/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from "ngx-toastr";
import { Http } from '@angular/http';
import { HttpErrorResponse } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Observable } from "rxjs/Observable";
import { Assay } from "app/assayviewrepo/assayviewrepo-models/assay";

@Injectable()
export class ParserService {
  public VERSION: number = 1000;
  private options = {
    attrPrefix: "@",
    textNodeName: "#text",
    ignoreNonTextNodeAttr: false,
    ignoreTextNodeAttr: true,
    ignoreNameSpace: true,
    ignoreRootElement: false,
    textNodeConversion: false,
    textAttrConversion: true
  };
  public assayList: Assay[];

  constructor(private _router: Router,
      private http: Http, 
      private toastrService: ToastrService, ) {
      this.assayList = [];
      this.update();
  }

  /** Returns the number of assay files loaded in the application. */
  getFileCount(): number {
    return this.assayList.length;
  }

  /** Returns all of the assay files loaded in the application. */
  getFiles(): Assay[] {
    return this.assayList;
  }

  /** Fetches a specific file from the list of assays. */ 
  getFile(index: number): Assay {
    if (index >= 0 && index < this.assayList.length) {
      return this.assayList[index];
    }
  }

  /** Removes an assay file from the assay list given an index value */
  removeElement(index: number): void {
    if (index > -1) {
      localStorage.removeItem('AV1000_' + this.assayList[index].fileName);
      this.assayList.splice(index, 1);
    }
  }

  addFile(file: File) {
    this.sendData(file.name, file).subscribe(res => {
      if(res == null)  {
        this.toastrService.warning('Unable to parse ' + file.name);
      }
      else {
        let assay: Assay = JSON.parse(res + '');
        assay.fileName = file.name;
        this.convertProcessStepMap(assay);


        if (!this.assayExists(assay)) {
          for(let i = 0; i < assay.reagentTypeDescription.reagents.length; i++) {
           this.convertTime(assay.reagentTypeDescription.reagents[i]);
          }
          this.saveAssay("AV1000_" + file.name, assay);
          this.assayList.push(assay);
        }
      }
    },
    error => {
      this.toastrService.warning('Unable to parse ' + file.name);
    });
  }
  
  sendData(text: string, file: File): Observable<Assay> {
    let formData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post('/api/Parsing/parseFile', formData)
        .map(res => res.json())
        .catch(this.handleError);
  }

  private handleError(err: HttpErrorResponse) {
    return Observable.throw(err.message);
  }

  /** Stores an assay object in the local storage memory. */
  saveAssay(key: string, assay: Assay): void {
    try {
      assay.date = new Date().getTime();
      localStorage.setItem(key, JSON.stringify(assay));
    } 
    catch(e) {
      this.toastrService.warning('Local Storage limit reached. File will not be saved'); 
    }
  }

  /** Loads saved files from local storage. */
  update(): void {
    let currentTime = new Date().getTime();
    let invalidFiles = [];

    for (let i = 0; i < localStorage.length; i++) {
      let key = localStorage.key(i);

      if(key.includes("AV1000_")) {
        let value: Assay = <Assay>JSON.parse(localStorage.getItem(key));

        if((currentTime - value.date) > 43200000) { 
          invalidFiles.push("AV1000_" + value.fileName);
          continue; 
        }
        this.assayList.push(value);
      }
      else if(key.includes("AV_")) {
        let value: Assay = <Assay>JSON.parse(localStorage.getItem(key));
        invalidFiles.push("AV_" + value.fileName);
        continue; 
      }
    }
    for(let i = 0; i < invalidFiles.length; i++)
      localStorage.removeItem(invalidFiles[i]);
  }

  /** Checks if an assay exists in the assayList instance. */
  assayExists(assay: Assay): boolean {
    for (let i = 0; i < this.assayList.length; i++) {
      if (assay.calculatedCrc === this.assayList[i].calculatedCrc) {
        this._router.navigate(['/assay', i]);
        window.scrollTo(0, 0);
        this.toastrService.info('Info: ' + assay.fileName + ' is already loaded');
        return true;
      }
    }
    return false;
  }

  /** Clears all the data stored in the local storage. */
  clear() {
    let length = this.assayList.length;
    for (let i = 0; i < length; i++) 
      localStorage.removeItem('AV1000_' + this.assayList[i].fileName);
    for (let i = 0; i < length; i++) 
      this.assayList.pop();
  }

  private convertTime(reagent: any) {
    let days: number = +reagent.openKitStability / 1440;
    let hours: number = +reagent.openKitStability % 1440 * 24;

    if(hours > 0) 
        reagent.openKitStability = days + ' days, ' + ('0' + hours).slice(-2) + ' hours';
    else 
        reagent.openKitStability = days + ' days'; 
    reagent.onBoardStability = ('0' + +reagent.onBoardStability / 60).slice(-2) + ' hours';
  }

  private convertProcessStepMap(assay: Assay) {
    let steps = [];

    let map = assay.processSteps;
    for (let key in map) {
      let properties = Object.getOwnPropertyNames(map[key]);
      let step = []; 
      for(let property in properties) {
        let prop = properties[property]; 
        let val = map[key][properties[property]];
        
        if(prop === 'StepList') {
          let steplist = [];
          for(let i = 0; i < val.length; i++) {
            let innerstep = []; 
            for(let p in val[i]) {
              let v = val[i][p];
              innerstep.push({property: p, value: v});
            }
            steplist.push(innerstep);
          }
          val = steplist;
        }
        else if(prop === 'StepList' || prop === 'Step') {
        }
        else if(prop.indexOf('Step') != -1 && prop.indexOf('StepName') == -1) {
          continue;
        }

        step.push({property: prop, value: val});
      }
      steps.push(step);
    }
    assay.processSteps = steps;
  }
}

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
import { Run } from "app/runexport/runexport-models/Run";

@Injectable()
export class RunService {
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
  public runList: Run[];

  constructor(private _router: Router,
      private http: Http, 
      private toastrService: ToastrService, ) {
      this.runList = [];
  }

  /** Returns the number of run files loaded in the application. */
  getFileCount(): number {
    return this.runList.length;
  }

  /** Returns all of the run files loaded in the application. */
  getFiles(): Run[] {
    return this.runList;
  }

  /** Fetches a specific file from the list of runs. */ 
  getFile(index: number){
    if (index >= 0 && index < this.runList.length) {
      return this.runList[index];
    }
  }

  /** Removes an run file from the run list given an index value */
  removeElement(index: number): void {
    if (index > -1) {
      this.runList.splice(index, 1);
    }
  }

  addFile(file: File) {
    this.sendData(file.name, file).subscribe(res => {
      if(res == null)  {
        this.toastrService.warning('Unable to parse ' + file.name);
      }
      else {
       var parsedObj = JSON.parse(res + '');
       let run: Run = parsedObj.run;
        run.FileName = parsedObj.FileName;
        
        this.runList.push(run);
      }
    },
    error => {
      this.toastrService.warning('Unable to parse ' + file.name);
    });
  }
  
  sendData(text: string, file: File): Observable<Run> {
    let formData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post('/api/Run/ProcessRun', formData)
        .map(res => res.json())
        .catch(this.handleError);
  }

  private handleError(err: HttpErrorResponse) {
    return Observable.throw(err.message);
  }

  /** Checks if an run exists in the runList instance. 
  runExists(run: Run): boolean {
    for (let i = 0; i < this.runList.length; i++) {
      if (run.calculatedCrc === this.runList[i].calculatedCrc) {
        this._router.navigate(['/run', i]);
        window.scrollTo(0, 0);
        this.toastrService.info('Info: ' + run.fileName + ' is already loaded');
        return true;
      }
    }
    return false;
  }*/

  /** Clears all the data stored in the local storage. */
  clear() {
    let length = this.runList.length;
    this.runList = [];
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

 /* private convertProcessStepMap(run: Run) {
    let steps = [];

    let map = run.processSteps;
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
    run.processSteps = steps;
  }*/
}

/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
import {CurveFile} from '../curverepo-models/CurveFile.component';
import { Injectable } from '@angular/core';
import {Http, Response} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import { RequestOptions, Request, RequestMethod, Headers } from '@angular/http';
import { BaseRequestOptions } from '@angular/http';
import { CurverepoUploadComponent } from '../curverepo-upload/curverepo-upload.component';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class CurverepoDatabaseService {

  constructor(private http: Http) { }
  
    getFile(IsGolden: boolean, TagField: string, AssayVersion: string, SoftwareVersion: string, AssayField: number):Observable<any> {
  
          return this.http.get("api/FileAccess" + "/" + IsGolden + "/" +
           TagField + "/" + AssayVersion + "/" + SoftwareVersion + "/" + AssayField)
          .map(this.extractData)
          .catch(this.handleError);
    }
  
    getAssay():Observable<any>{
        return this.http.get("api/FileAccess/getAssay")
        .map(this.extractData)
        .catch(this.handleError);
    }

    getAssayInDB():Observable<any>{
        return this.http.get("api/FileAccess/getAssayInDB")
        .map(this.extractData)
        .catch(this.handleError);
    }


    getVersion():Observable<any>{
        return this.http.get("api/FileAccess/getVersion")
        .map(this.extractData)
        .catch(this.handleError);
    }
  
    getStatForKnown():Observable<any>{
        return this.http.get("api/FileAccess/getKnown")
        .map(this.extractData)
        .catch(this.handleError);
    }
  
    login(ID: string, PassWord: string):Observable<any>{
      return this.http.get("api/Login/" + ID + "/" + PassWord)
      .map(this.extractData)
      .catch(this.handleError);
  }
  
  
    sendFile(files, curveObjects, assayName){
          
          let headers = new Headers({ 'Accept': 'application/json' }); // ... Set content type to JSON
          let options = new RequestOptions({ headers: headers }); // Create a request option
          let formData = new FormData();
          var count = 0;
          for (let file of files) {
              formData.append(`file-${count}`, file, file.name);
              formData.append(`file-metadata-${count}`, this.Jsonify(curveObjects[count]));
              formData.append(`file-assayname-${count}`, this.Jsonify(assayName[count]));
              count += 1;
          }
                  return this.http.post("api/FileAccess", formData)
                  .map(this.extractData)
                  .catch(this.handleError);
  
    }
  
    matchAssay(fileName:any, files: any){
  
      let headers = new Headers({ 'Accept': 'application/json' }); // ... Set content type to JSON
      let options = new RequestOptions({ headers: headers }); // Create a request option
      let formData = new FormData();
      var count = 0;
      for (let file of files) {
          formData.append(`file-${count}`, file, file.name);
          formData.append(`file-metadata-${count}`, this.Jsonify(fileName[count]));
          count += 1;
      }
  
      return this.http.post("api/FileAccess/SetAssay", formData)
      .map(this.extractData)
      .catch(this.handleError);
  
    }
  
    Jsonify(objectInput: any){
        let body = JSON.stringify(objectInput);
        return body;
    }
  
    private extractData (res: Response){
        let body = res.json();
        return body;
    }
  
    
    private handleError (error: Response | any) {
      let errMsg: string = "";
      if (error instanceof Response) {
        const body = error.json() || '';
        const err = body.error || JSON.stringify(body);
        errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
      } else {
        errMsg = error.message ? error.message : error.toString();
      }
      console.error(errMsg);
      if (errMsg != ""){
          let delim = errMsg.indexOf('[');
          errMsg = errMsg.substr(delim, errMsg.length-1);
          alert(errMsg + " Invalid Curve File(s)");
      }else{
          alert(errMsg + "Upload Successful");
      }
      
      return Observable.throw(errMsg);
    }
}

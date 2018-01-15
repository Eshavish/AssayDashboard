/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/

import { Injectable, OnChanges, Output, EventEmitter } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import { RequestOptions, Request, RequestMethod, Headers } from '@angular/http';
import { BaseRequestOptions } from '@angular/http';
import { UploadFiles } from '../reportrepo-upload/reportrepo-upload.component';
import { ToastrService } from 'ngx-toastr';
import 'rxjs/Rx';
import { Report } from "app/reportrepo/reportrepo-models/reportrepo-reportclass.component";

@Injectable()
export class DatabaseService {
    rem: string[];
    constructor(private http: Http, private toastService: ToastrService) {
    }

    //loads assay data from report file
    //NOTE: IE does not support URLSearchParams()
    loadAssayDataFromReportFile(): Observable<Response> {
        let myHeaders = new Headers();
        myHeaders.append('Content-Type', 'application/json');
        let options = new RequestOptions({ headers: myHeaders });
        return this.http.get('api/AssayNamesReportFile')
            .map(this.extractDataOfAssayType).catch(this.handleError);
    }

    //loads the assay name dropdown
    loadAssayData(): Observable<Response> {
        let myHeaders = new Headers();
        myHeaders.append('Content-Type', 'application/json');
        let options = new RequestOptions({ headers: myHeaders });
        return this.http.get('api/Assay')
            .map(this.extractDataOfAssayType).catch(this.handleError);
    }

    //Get assay and ssv
    GetFilesFromAssaySSVDatabase(typeIdToSend: number, majorVersion: number, minorVersion: number, servicePack: number, buildNumber: number): Observable<Response> {
        if (typeIdToSend == undefined || typeIdToSend == 0) {
            typeIdToSend = -1;
        } if (majorVersion == undefined) {
            majorVersion = -1;
        } if (minorVersion == undefined) {
            minorVersion = -1;
        } if (servicePack == undefined) {
            servicePack = -1;
        } if (buildNumber == undefined) {
            buildNumber = -1;
        }
        return this.http.get('api/AssaySSVQuery' + '/' + typeIdToSend + '/' + majorVersion + '/' + minorVersion + '/' + servicePack + '/' + buildNumber).map(res => res.json()).catch(this.handleError);
    }

    //Get report and ssv
    GetFilesFromReportSSVDatabase(reportType: string, majorVersion: number, minorVersion: number, servicePack: number, buildNumber: number): Observable<Response> {
        if (reportType == undefined || reportType.length <= 0 || reportType == ' ') {
            reportType = 'null';
        } if (majorVersion == undefined) {
            majorVersion = -1;
        } if (minorVersion == undefined) {
            minorVersion = -1;
        } if (servicePack == undefined) {
            servicePack = -1;
        } if (buildNumber == undefined) {
            buildNumber = -1;
        }
        return this.http.get('api/ReportSSVQuery' + '/' + reportType + '/' + majorVersion + '/' + minorVersion + '/' + servicePack + '/' + buildNumber).map(res => res.json()).catch(this.handleError);
    }

    // Get assay, report and ssv
    GetFilesFromAssayReportSSVDatabase(typeIdToSend: number, reportType: string, majorVersion: number, minorVersion: number, servicePack: number, buildNumber: number): Observable<Response> {
        if (typeIdToSend == undefined || typeIdToSend == 0) {
            typeIdToSend = -1;
        } if (reportType == undefined || reportType.length <= 0 || reportType == ' ') {
            reportType = 'null';
        } if (majorVersion == undefined) {
            majorVersion = -1;
        } if (minorVersion == undefined) {
            minorVersion = -1;
        } if (servicePack == undefined) {
            servicePack = -1;
        } if (buildNumber == undefined) {
            buildNumber = -1;
        }
        return this.http.get('api/ReportAssaySSVQuery' + '/' + typeIdToSend + '/' + reportType + '/' + majorVersion + '/' + minorVersion + '/' + servicePack + '/' + buildNumber).map(res => res.json()).catch(this.handleError);
    }

    //Get files from db
    GetAllFilesFromDatabase(typeIdToSend: number, reportType: string, majorVersion: number, minorVersion: number, servicePack: number, buildNumber: number): Observable<Response> {
        if (typeIdToSend == undefined || typeIdToSend == 0) {
            typeIdToSend = -1;
        } if (reportType == undefined || reportType.length <= 0 || reportType == ' ') {
            reportType = 'null';
        } if (majorVersion == undefined) {
            majorVersion = -1;
        } if (minorVersion == undefined) {
            minorVersion = -1;
        } if (servicePack == undefined) {
            servicePack = -1;
        } if (buildNumber == undefined) {
            buildNumber = -1;
        }
        return this.http.get('api/Values' + '/' + typeIdToSend + '/' + reportType + '/' + majorVersion + '/' + minorVersion + '/' + servicePack + '/' + buildNumber).map(res => res.json()).catch(this.handleError);
    }

    private dataFromSearch(res: Response) {
        let body = res.json();
        return body;
    }

    public extractDataOfAssayType(res: Response) {
        let body = res.json();
        if (body) {
            return body;
        } else {
            return "";
        }
    }

    //loads the reports dropdown menu
    loadReportTypes(): Observable<Response> {
        let myHeaders = new Headers();
        myHeaders.append('Content-Type', 'application/json');
        let options = new RequestOptions({ headers: myHeaders });
        return this.http.get('api/Report')
            .map(this.extractDataOfReportType).catch(this.handleError);
    }

    public extractDataOfReportType(res: Response) {
        let body = res.json();
        if (body) {
            return body;
        } else {
            return "";
        }
    }

    checkIfFileExists(files) {
        let formData = new FormData();
        var count = 0;
        var countloop: number = 0;
        for (let file of files) {
            let headers = new Headers({ 'Content': 'application/json' });
            let options = new RequestOptions({ headers: headers });
            formData.append(`file-${count}`, file, file.name);
        }

        return this.http.post("api/HashValues", formData)
            .map(this.bodyToJSON).catch(this.handleError);
    }

    storeFiles(reportObjectToStore: Report[]) {
    }

    extractPdfData(files) {
        let formData = new FormData();
        var count = 0;
        var countloop: number = 0;
        for (let file of files) {
            let headers = new Headers({ 'Content': 'application/json' });
            let options = new RequestOptions({ headers: headers });
            formData.append(`file-${count}`, file, file.name);
        }
        return this.http.post("api/ExtractReportType", formData)
            .map(this.bodyToJSON).catch(this.handleError);
    }

    extractPdfSSV(files) {
        let formData = new FormData();
        var count = 0;
        var countloop: number = 0;
        for (let file of files) {
            let headers = new Headers({ 'Content': 'application/json' });
            let options = new RequestOptions({ headers: headers });
            formData.append(`file-${count}`, file, file.name);
        }
        return this.http.post("api/ExtractSSV", formData)
            .map(this.bodyToJSON).catch(this.handleError);
    }

    extractPdfAssayName(files) {
        let formData = new FormData();
        var count = 0;
        var countloop: number = 0;
        for (let file of files) {
            let headers = new Headers({ 'Content': 'application/json' });
            let options = new RequestOptions({ headers: headers });
            formData.append(`file-${count}`, file, file.name);
        }
        return this.http.post("api/ExtractAssayName", formData)
            .map(this.bodyToJSON).catch(this.handleError);
    }

    public bodyToJSON(res: Response) {
        let body = res.json();
        if (body) {
            return body;
        } else {
            return "";
        }
    }

    //posts file to the serve
    sendFile(files, reportObject) {
        let formData = new FormData();
        var count = 0;
        var countloop: number = 0;
        for (let file of files) {
            let headers = new Headers({ 'Content': 'application/json' });
            let options = new RequestOptions({ headers: headers });
            formData.append(`file-${count}`, file, file.name);
            formData.append(`file-metadata-${count}`, this.Jsonify(reportObject[count++]));
        }
        this.http.post("api/UploadFile", formData)
            .subscribe(data => {
                this.toastService.success(count + " Files Uploaded Successfully");
            }, error => {
                this.toastService.error(error);
            });
    }

    Jsonify(objectInput: any) {
        let body = JSON.stringify(objectInput);
        return body;
    }

    private extractData(res: Response) {
        let body = res.json();
        return body;
    }

    private handleError(error: Response | any) {
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Observable.throw(errMsg);
    }
}

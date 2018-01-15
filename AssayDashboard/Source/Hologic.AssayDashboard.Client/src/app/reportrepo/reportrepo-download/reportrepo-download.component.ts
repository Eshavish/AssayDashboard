/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/

import { Component, OnInit, OnDestroy, ElementRef } from '@angular/core';
import { DatabaseService } from '../reportrepo-services/reportrepo-dbservice';
import { HttpModule } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { RequestOptions, Request, RequestMethod, Headers } from '@angular/http';
import 'rxjs/Rx';
import { ToastrService } from 'ngx-toastr';
import { IAssayItemsDownload } from './reportrepo-download';
import { saveAs } from 'file-saver';
import JSZip from 'jszip';

@Component({
    moduleId: module.id,
    templateUrl: './reportrepo-download.component.html',
    styleUrls: ['./reportrepo-download.component.scss'],
    providers: [DatabaseService]
})

export class DowloadFiles {
    minorVersionNumber: number;
    majorVersionNumber: number;
    servicePackNumber: number;
    buildNumber: number;
    resultFromAssayQuery: any;
    fileName: string[] = [];
    assayNames: string[] = [];
    reportType: string[] = [];
    fileContent: any[] = [];
    assayItems: IAssayItemsDownload[] = [];
    testAssayTodisplayDownload: string[] = [];
    saveChosenAssayDownload: string;
    reportOptionsDownload: string[] = [];
    saveChosenReportDownload: string;
    finalAssayDatabaseDownload: string[] = [];
    finalTypeId: number[] = [];
    assayToSend: string;
    showTable: boolean;
    inputFocused: boolean = false;
    countMajor: number = 0;
    countMinor: number = 0;
    countService: number = 0;
    reportSelectedCheck: boolean = true;
    assayNameSelectedCheck: boolean = true;
    ssvSelectedeCheck: boolean = true;
    typeIdToSend: number;
    setCheckOnReport: boolean = true;
    setCheckOnAssay: boolean = true;
    setCheckOnSSV: boolean = true;
    isValid: boolean = true;
    checkSSVInputs: boolean;
    majorVersionNumberwildentry: number;
    minorVersionNumberwildentry: number;
    assayNameToDisplay: string[] = [];
    reportTypeToDisplay: string[] = [];
    majorVersionNumberDisplay: number[] = [];
    minorVersionNumberDisplay: number[] = [];
    serviceNumberDisplay: number[] = [];
    buildNumberDisplay: number[] = [];
    flag: boolean;
    chosenReport: string;
    chosenAssay: string;
    chosenMajor: number;
    chosenMinor: number;
    chosenService: number;
    chosenBuild: number;

    private filesNameCallBackListener: Function = (payload: any) => {
        this.assayNames = [];
        this.fileContent = [];
        this.reportType = [];
        this.fileName = [];
        this.resultFromAssayQuery = payload;
        var jsonConverted = JSON.parse(this.resultFromAssayQuery);
        if (jsonConverted == null || jsonConverted == undefined || jsonConverted.length <= 0) {
            this.toastService.error("File not found");
            return;
        }
        jsonConverted.forEach(element => {
            this.fileName.push(element.File.FileName);
            this.reportTypeToDisplay.push(element.File.ReportType.ReportName);
            this.assayNameToDisplay.push(element.File.AssayType.AssayName);
            this.majorVersionNumberDisplay.push(element.File.MajorVersion);
            this.minorVersionNumberDisplay.push(element.File.MinorVersion);
            this.serviceNumberDisplay.push(element.File.ServicePackNumber);
            this.buildNumberDisplay.push(element.File.BuildNumber);
            this.fileContent.push(element.File.FileContent);
        });
        if (this.fileName != undefined || this.fileName != null)
            this.showTable = true;
        else
            this.showTable = false;
    };

    ngOnInit(): void {
        this.dataService.loadAssayDataFromReportFile();
        this.dataService.loadReportTypes();
    }

    reportTypeClicked() {
        if (this.reportSelectedCheck == false) {
            this.reportSelectedCheck = true;
        }
        else {
            this.reportSelectedCheck = false;
        }
    }

    assayNameClicked() {
        if (this.assayNameSelectedCheck == false) {
            this.assayNameSelectedCheck = true;
        }
        else {
            this.assayNameSelectedCheck = false;
        }
    }

    ssvClicked() {
        if (this.ssvSelectedeCheck == false) {
            this.ssvSelectedeCheck = true;
        }
        else {
            this.ssvSelectedeCheck = false;
        }
    }

    private assayNameCallbackListener: Function = (payload: any) => {
        this.testAssayTodisplayDownload.push(" ")
        this.assayItems = payload;
        let keys = Object.keys(this.assayItems);
        for (var i = 0; i < payload.length; i++) {
            this.testAssayTodisplayDownload.push(payload[i].AssayName + " " + "Type Id" + " " + payload[i].TypeId);
            this.finalAssayDatabaseDownload.push(payload[i].AssayName);
            this.finalTypeId.push(payload[i].TypeId);
        }
    };

    private reportTypeCallbackListener: Function = (payload: any) => {
        this.reportOptionsDownload.push(" ");
        for (var i = 0; i < payload.length; i++) {
            this.reportOptionsDownload.push(payload[i]);
        }
    };

    onChange() {
        if (this.saveChosenAssayDownload != null || this.saveChosenAssayDownload != undefined) {
            var index: number = this.testAssayTodisplayDownload.indexOf(this.saveChosenAssayDownload);
            this.typeIdToSend = this.finalTypeId[index - 1];
            this.assayToSend = this.finalAssayDatabaseDownload[index - 1];
        }
    }

    //Class constructor
    constructor(private dataService: DatabaseService, private toastService: ToastrService, private elementRef: ElementRef) {
        this.dataService.loadAssayDataFromReportFile().subscribe(
            result => this.assayNameCallbackListener(result)
        );
        this.dataService.loadReportTypes().subscribe(
            result => this.reportTypeCallbackListener(result)
        );
    }

    isValidForm() {
        if (this.reportSelectedCheck == false && this.assayNameSelectedCheck == false && this.ssvSelectedeCheck == false)
            return !this.isValid;
        else
            return this.isValid;
    }

    //Disabling Download All and Reset button
    filesPresent() {
        if (this.fileName.length<=0)
            return !this.isValid;
        else
            return this.isValid;
    }

    getFiles(): void {
        //Check on report type if selected
        if (this.setCheckOnReport == true) {
            if (this.saveChosenReportDownload == undefined) {
                this.toastService.error("Enter the report type");
                return;
            }
        }

        //Check on assay name if selected
        if (this.setCheckOnAssay == true) {
            if (this.typeIdToSend == undefined) {
                this.toastService.error("Enter the assay name");
                return;
            }
        }

        //Check on system software version if selected
        if (this.setCheckOnSSV == true) {
            if (this.majorVersionNumber == undefined && this.minorVersionNumber == undefined && this.buildNumber == undefined && this.servicePackNumber == undefined) {
                this.toastService.error("Enter the system software version number");
                return;
            }
        }
        this.setValue(this.saveChosenReportDownload, this.saveChosenAssayDownload, this.majorVersionNumber, this.minorVersionNumber, this.servicePackNumber, this.buildNumber);

        if (this.typeIdToSend == undefined && this.saveChosenReportDownload == undefined && this.majorVersionNumber == undefined && this.minorVersionNumber == undefined && this.servicePackNumber == undefined && this.buildNumber == undefined) {
            this.toastService.error("Enter atleast one Report Attributes");
            return;
        }
        else {
            //if report and some fields of SSV are selected
            if (this.majorVersionNumber == undefined || this.minorVersionNumber == undefined || this.servicePackNumber == undefined || this.buildNumber == undefined) {
                this.flag = true;
            }
            if (this.setCheckOnSSV == true && this.flag == true && this.saveChosenReportDownload && !this.typeIdToSend) {
                this.dataService.GetFilesFromReportSSVDatabase(this.saveChosenReportDownload,
                    this.majorVersionNumber,
                    this.minorVersionNumber,
                    this.servicePackNumber,
                    this.buildNumber).subscribe(r => this.filesNameCallBackListener(r));
                this.clearFieldsNotReload();
                return;
            }

            if (this.setCheckOnSSV == true && this.flag == true && this.typeIdToSend && !this.saveChosenReportDownload) {
                this.dataService.GetFilesFromAssaySSVDatabase(this.typeIdToSend,
                    this.majorVersionNumber,
                    this.minorVersionNumber,
                    this.servicePackNumber,
                    this.buildNumber).subscribe(r => this.filesNameCallBackListener(r));
                this.clearFieldsNotReload();
                return;
            }

            if (this.setCheckOnSSV == true && this.flag == true && this.saveChosenAssayDownload && this.typeIdToSend) {
                this.dataService.GetFilesFromAssayReportSSVDatabase(this.typeIdToSend,
                    this.saveChosenReportDownload,
                    this.majorVersionNumber,
                    this.minorVersionNumber,
                    this.servicePackNumber,
                    this.buildNumber).subscribe(r => this.filesNameCallBackListener(r));
                this.clearFieldsNotReload();
                return;
            }

            this.dataService.GetAllFilesFromDatabase
                (this.typeIdToSend,
                this.saveChosenReportDownload,
                this.majorVersionNumber,
                this.minorVersionNumber,
                this.servicePackNumber,
                this.buildNumber).subscribe(r => this.filesNameCallBackListener(r));
            this.clearFieldsNotReload();
        }
    }

    setValue(report: string, assay: string, major: number, minor: number, service: number, build: number) {
        this.chosenReport = report;
        this.chosenAssay = assay;
        this.chosenBuild = build;
        this.chosenMajor = major;
        this.chosenService = service;
        this.chosenMinor = minor;
    }

    //Clear fields
    clearFieldsNotReload() {
        this.assayToSend = undefined;
        this.saveChosenReportDownload = undefined;
        this.saveChosenAssayDownload = undefined;
        this.majorVersionNumber = undefined;
        this.minorVersionNumber = undefined;
        this.majorVersionNumberwildentry = undefined;
        this.minorVersionNumberwildentry = undefined;
        this.servicePackNumber = undefined;
        this.buildNumber = undefined;
        this.reportSelectedCheck = false;
        this.assayNameSelectedCheck = false;
        this.ssvSelectedeCheck = false;
        this.typeIdToSend = undefined;
        this.setCheckOnReport = false;
        this.setCheckOnAssay = false;
        this.setCheckOnSSV = false;
        this.reportTypeToDisplay = [];
        this.assayNameToDisplay = [];
        this.majorVersionNumberDisplay = [];
        this.minorVersionNumberDisplay = [];
        this.serviceNumberDisplay = [];
        this.buildNumberDisplay = [];
        this.flag = false;
    }

    //Clear all fields
    clearFields() {
        this.assayToSend = undefined;
        this.saveChosenReportDownload = undefined;
        this.saveChosenAssayDownload = undefined;
        this.majorVersionNumber = undefined;
        this.minorVersionNumber = undefined;
        this.servicePackNumber = undefined;
        this.buildNumber = undefined;
        this.fileContent = [];
        this.fileName = [];
        this.reportSelectedCheck = false;
        this.assayNameSelectedCheck = false;
        this.ssvSelectedeCheck = false;
        this.setCheckOnReport = false;
        this.setCheckOnAssay = false;
        this.setCheckOnSSV = false;
        this.chosenReport = "";
        this.chosenAssay = "";
        this.chosenMajor = undefined;
        this.chosenMinor = undefined;
        this.chosenService = undefined;
        this.chosenBuild = undefined;
    }

    //Check to allow only numeric values
    _keyPressMajor(event: any) {
        const pattern = /[0-9\+\-\ ]/;
        let inputChar = String.fromCharCode(event.charCode);

        if (!pattern.test(inputChar)) {
            event.preventDefault();
        }
        else {
            this.countMajor++;
        }
        if (this.countMajor == 3) {
            let majorVersion = (<HTMLElement>document.getElementsByClassName('majorrVersion')[0]);
            let minorVersion = (<HTMLElement>document.getElementsByClassName('minorVersion')[0]);
            minorVersion.focus();
        }
    }

    //Check to allow only numeric values
    _keyPressMinor(event: any) {
        const pattern = /[0-9\+\-\ ]/;
        let inputChar = String.fromCharCode(event.charCode);

        if (!pattern.test(inputChar)) {
            event.preventDefault();
        }
        else {
            this.countMinor++;
        }
        if (this.countMinor == 3) {
            let minorVersion = (<HTMLElement>document.getElementsByClassName('minorVersion')[0]);
            let servicePack = (<HTMLElement>document.getElementsByClassName('servicePackageNumber')[0]);
            servicePack.focus();
        }
    }

    //Check to allow only numeric values
    _keyPressService(event: any) {
        const pattern = /[0-9\+\-\ ]/;
        let inputChar = String.fromCharCode(event.charCode);

        if (!pattern.test(inputChar)) {
            event.preventDefault();
        }
        else {
            this.countService++;
        }
        if (this.countService == 3) {
            let servicePack = (<HTMLElement>document.getElementsByClassName('servicePackageNumber')[0]);
            let buildNumberFocus = (<HTMLElement>document.getElementsByClassName('buildNumber')[0]);
            buildNumberFocus.focus();
        }
    }

    //View files in a seperate window
    viewFiles(index: number) {
        var byteCharacters = atob(this.fileContent[index]);
        var byteNumbers = new Array(byteCharacters.length);
        for (var j = 0; j < byteCharacters.length; j++) {
            byteNumbers[j] = byteCharacters.charCodeAt(j);
        }
        var byteArray = new Uint8Array(byteNumbers);
        var blob = new Blob([byteArray], { type: 'application/pdf' });
        var url = window.URL.createObjectURL(blob);
        window.open(url);
    }

    // Download all files as zip folder
    downloadAll() {
        var zip = new JSZip();
        var files = zip.folder("Reports");
        //fileContent has the pdf file content
        for (var i = 0; i < this.fileContent.length; i++) {
            var byteCharacters = atob(this.fileContent[i]);
            var byteNumbers = new Array(byteCharacters.length);
            for (var j = 0; j < byteCharacters.length; j++) {
                byteNumbers[j] = byteCharacters.charCodeAt(j);
            }
            //making blob using the byte array
            var byteArray = new Uint8Array(byteNumbers);
            var blob = new Blob([byteArray], { type: 'application/pdf' });
            files.file(this.fileName[i], blob);
        }
        zip.generateAsync({ type: "blob" })
            .then(function (content) {
                saveAs(content, "reports.zip");
            });
    }

    //Download files one by one
    downloadFiles(fileName: string, index: number) {
        var byteCharacters = atob(this.fileContent[index]);
        var byteNumbers = new Array(byteCharacters.length);
        for (var j = 0; j < byteCharacters.length; j++) {
            byteNumbers[j] = byteCharacters.charCodeAt(j);
        }
        //making blob using the byte array
        var byteArray = new Uint8Array(byteNumbers);
        var blob = new Blob([byteArray], { type: 'application/pdf' });
        saveAs(blob, fileName, false);
    }
}

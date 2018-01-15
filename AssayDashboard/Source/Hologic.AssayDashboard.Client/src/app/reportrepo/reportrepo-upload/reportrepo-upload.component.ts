/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpModule } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { RequestOptions, Request, RequestMethod, Headers } from '@angular/http';
import 'rxjs/Rx';
import { DatabaseService } from '../reportrepo-services/reportrepo-dbservice';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Report } from "app/reportrepo/reportrepo-models/reportrepo-reportclass.component";
import { IAssayItems } from './reportrepo-upload';
import { NgModel } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrService } from 'ngx-toastr';
import { Ng2Bs3ModalModule } from 'ng2-bs3-modal/ng2-bs3-modal';
import { PopoverModule } from "ngx-popover";
import { IssvItems } from './reportrepo-ssvclass';

@Component({
    moduleId: module.id,
    templateUrl: './reportrepo-upload.component.html',
    styleUrls: ['./reportrepo-upload.component.scss'],
    providers: [DatabaseService]
})

export class UploadFiles implements OnInit {
    filesToExtract: string[] = [];
    filesHolder: string[] = [];
    filesToPost: string[] = [];
    minorVersionNumber: number[] = [];
    majorVersionNumber: number[] = [];
    servicePackNumber: number[] = [];
    buildNumber: number[] = [];
    mode = 'Observable';
    files: string;
    assayOptions: string[] = [];
    reportOptions: string[] = [];
    selectedAssay: number[] = [];
    selectedReport: number[] = [];
    reportObject: string[] = [];
    finalAssayOptions: string[] = [];
    finalAssayToDatabase: number[] = [];
    assayNameArrayHolder: string[] = [];
    saveChosenAssay: string[] = [];
    saveChosenReport: string[] = [];
    assayItems: IAssayItems[] = [];
    ssvItems: IssvItems[] = [];
    testIdToDatabase: number[] = [];
    testAssayTodisplay: string[] = [];
    yesClicked: boolean = false;
    noClicked: boolean = false;
    globalIndex: number;
    tempFileHolder: any[] = [];
    tempMajorVersionHolder: string[] = [];
    majorNumberEmpty: number[] = [];
    objectToStore: Report[];
    filePresent: boolean;
    countMinorFocus: number = 0;
    countMajorFocus: number = 0;
    countServiceFocus: number = 0;

    //Getting assay names for displaying on UI
    private assayNameCallbackListener: Function = (payload: any) => {
        this.testAssayTodisplay.push("Choose AssayName");
        this.assayItems = payload;
        let keys = Object.keys(this.assayItems);
        for (var i = 0; i < payload.length; i++) {
            this.testIdToDatabase.push(this.assayItems[i].Id);
        }
        for (var i = 0; i < payload.length; i++) {
            this.testAssayTodisplay.push(payload[i].AssayName + " " + " " + "(" + payload[i].TypeId + ")");
        }
    }

    //Setting reports for report dropdown menu on UI
    private reportTypeCallbackListener: Function = (payload: any) => {
        this.reportOptions.push("Choose ReportType");
        for (var i = 0; i < payload.length; i++) {
            this.reportOptions.push(payload[i]);
        }
    }

    //Setting the System Software Version number on the UI
    private extractSSVFromPdf: Function = (ssvList: any) => {
        this.ssvItems = ssvList;
        for (var i = 0; i < ssvList.length; i++) {
            this.majorVersionNumber[i] = this.ssvItems[i].MajorVersion;
            this.minorVersionNumber[i] = this.ssvItems[i].MinorVersion;
            this.servicePackNumber[i] = this.ssvItems[i].ServiceBuild;
            this.buildNumber[i] = this.ssvItems[i].BuildNumber;
        }
    }

    //Setting the report type on the UI
    private extractDataFromPdf: Function = (reportData: any) => {
        for (let i = 0; i < reportData.length; i++) {
            this.saveChosenReport[i] = reportData[i];
            this.onReportSelect(this.saveChosenReport[i], i)
        }
    }
    // Setting Assay Name on the UI
    private extractAssayNameFromPdf: Function = (assayList: any) => {
        for (let i = 0; i < assayList.length; i++) {
            this.saveChosenAssay[i] = assayList[i];
            this.onChange(this.saveChosenAssay[i], i)
        }
    }

    // Class constructor
    constructor(private dataService: DatabaseService, private toastService: ToastrService) {
        this.dataService.loadAssayData().subscribe(
            result => this.assayNameCallbackListener(result)
        );
        this.dataService.loadReportTypes().subscribe(
            result => this.reportTypeCallbackListener(result)
        );
    }

    ngOnDestroy() {
    }

    ngOnInit(): void {
        this.dataService.loadAssayData();
        this.dataService.loadReportTypes();
    }

    //Onclick of assay type dropdown
    onChange(assayReturned: any, toPutIndex: any) {
        if (assayReturned == "Choose AssayName") {
            this.selectedAssay[toPutIndex] = null;
            return;
        }
        var index = this.findIndexOfAssayOptionsToDisplay(this.testAssayTodisplay, assayReturned)
        this.selectedAssay[toPutIndex] = this.testIdToDatabase[index];
    }

    // Onclick of report selected from dropdown
    onReportSelect(returnReport: any, toPutIndex: any) {
        if (returnReport == "Choose ReportType") {
            this.selectedReport[toPutIndex] = null;
            return;
        }
        var reportIndex = this.findIndexOfReport(this.reportOptions, returnReport);
        this.selectedReport[toPutIndex] = reportIndex;
    }

    findIndexOfReport(reportOptions, val) {
        var index: number = 0;
        var size = reportOptions.length;
        while (index < size && reportOptions[index].trim() != val.trim())++index;
        return index;
    }

    findIndexOfAssayOptionsToDisplay(assayWithIndex, val) {
        var index: number = 0;
        var size = assayWithIndex.length;
        while (index < size && assayWithIndex[index].trim() != val.trim())++index;
        return index;
    }

    yesClickedFunction() {
        this.yesClicked = true;
        this.noClicked = false;
        this.finalDelete()
        this.globalIndex = -1;
    }

    noClickedFunction() {
        this.noClicked = true;
    }

    deleteFile(index) {
        this.globalIndex = index;
    }

    finalDelete() {
        if (this.yesClicked == true) {
            this.filesHolder.splice(this.globalIndex, 1);
            for (var i = 0; i < this.filesHolder.length; i++) {
            }
        }
        if (!this.noClicked) {

        }
    }

    majorVersionInput(majorInput, i) {
    }

    minorVersionInput(minorVersion, i) {
    }

    servicePackInput(servicePack, i) {
    }

    buildVersionInput(buildVersion, i) {
    }

    //checking for duplicate files
    private extractHashValues: Function = (hashData: any) => {
        for (let i = 0; i < hashData.length; i++) {
            if (hashData[i] != 1) {
                this.tempFileHolder.push(this.filesToExtract[i]);
            }
            else {
                this.toastService.error(this.filesToExtract[i]['name'] + " already exists in database");
            }
        }
        this.tempFileHolder.forEach(element => {
            console.log("element printing here ");
            console.log(element.name);
            if (this.checkIfExists(element.name) == false) {
                console.log("inserting element");
                this.filesHolder.push(element);
                this.assayNameArrayHolder.push(element.name);
            }
        });
        this.dataService.extractPdfData(this.filesHolder).subscribe(r => this.extractDataFromPdf(r));
        this.dataService.extractPdfAssayName(this.filesHolder).subscribe(r => this.extractAssayNameFromPdf(r));
        this.dataService.extractPdfSSV(this.filesHolder).subscribe(r => this.extractSSVFromPdf(r));
        if (this.filesHolder.length >= 0) {
            this.filePresent = true;
        }
        this.filesToExtract = []
    }

    holdFiles(event: any) {
        for (let eachValue of event.target.files) {
            this.filesToExtract.push(eachValue);
        }
        this.dataService.checkIfFileExists(this.filesToExtract).subscribe(r => this.extractHashValues(r));
    }

    checkIfExists(fileNameToCheck) {
        if (this.assayNameArrayHolder.indexOf(fileNameToCheck) != -1)
            return true;
        else
            return false;
    }

    //To check for only numeric values
    _keyPress(event: any) {
        const pattern = /[0-9\+\-\ ]/;
        let inputChar = String.fromCharCode(event.charCode);

        if (!pattern.test(inputChar)) {
            event.preventDefault();
        }
    }

    Post() {
        //To check if no file is selected
        if (this.filesHolder.length <= 0) {
            this.toastService.error("Please select a file");
            return;
        }

        //validate for all empty values
        if (this.selectedAssay.length <= 0 && this.selectedReport.length <= 0 && this.majorVersionNumber.length <= 0 && this.minorVersionNumber.length <= 0 && this.servicePackNumber.length <= 0 && this.buildNumber.length <= 0) {
            this.toastService.error("Please enter the file attributes");
            return;
        }

        //validate for empty report type
        if (this.saveChosenReport.length <= 0) {
            this.toastService.error("Please enter the report type");
            return;
        }

        //Checking for undefined report types
        for (let i = 0; i < this.filesHolder.length; i++) {
            if (this.saveChosenReport[i] == null || this.saveChosenReport[i] == undefined) {
                var p = i + 1;
                this.toastService.error("Please enter the report type for file " + p);
                return;
            }
        }

        //Checking for undefined report types
        for (let i = 0; i < this.filesHolder.length; i++) {
            if (this.selectedReport[i] == null) {
                var p = i + 1;
                this.toastService.error("Please enter the report type for file " + p);
                return;
            }
        }

        //validate for empty assay name
        if (this.saveChosenAssay.length <= 0) {
            this.toastService.error("Please enter the assay name type");
            return;
        }
        for (let i = 0; i < this.filesHolder.length; i++) {
            if (this.saveChosenAssay[i] == null || this.saveChosenAssay[i] == undefined || this.saveChosenAssay[i] == "Choose AssayName") {
                var q = i + 1;
                this.toastService.error("Please enter the assay name for file " + q);
                return;
            }
        }

        //checking for negative SSV
        for (let i = 0; i < this.filesHolder.length; i++) {
            if (this.majorVersionNumber[i] < 0 || this.minorVersionNumber[i] < 0 || this.servicePackNumber[i] < 0
                || this.buildNumber[i] < 0) {
                var fileNumber = i + 1;
                this.toastService.error("Please enter positive values for system software version number for file " + fileNumber);
                return;
            }
        }

        //checking for an empty system software number
        for (let i = 0; i < this.filesHolder.length; i++) {
            if (this.majorVersionNumber[i] == undefined && this.minorVersionNumber[i] == undefined && this.servicePackNumber[i] == undefined && this.buildNumber[i] == undefined) {
                var o = i + 1;
                this.toastService.error("Please enter the system software version number for file " + o);
                return;
            }

            //check for 0.0.0.0
            if (this.majorVersionNumber[i] == 0 && this.minorVersionNumber[i] == 0 && this.servicePackNumber[i] == 0 && this.buildNumber[i] == 0) {
                var o = i + 1;
                this.toastService.error("Please enter a valid system software version number for file " + o);
                return;
            }
            if (this.majorVersionNumber[i] == undefined || this.majorVersionInput == null)
                this.majorVersionNumber[i] = 0;
            if (this.minorVersionNumber[i] == undefined || this.minorVersionNumber == null)
                this.minorVersionNumber[i] = 0;
            if (this.servicePackNumber[i] == undefined || this.servicePackNumber == null)
                this.servicePackNumber[i] = 0;
            if (this.buildNumber[i] == undefined || this.buildNumber == null)
                this.buildNumber[i] = 0;
        }
        try {
            this.filesToPost = this.filesHolder;
            if (this.filesToPost.length <= 0) {
                this.toastService.error("Please  choose a file");
                return;
            }
            var count: number = 0;
            for (let i = 0; i < this.filesToPost.length; i++) {
                this.createObject(this.filesToPost[i], i);
                count++;
            }

            for (let i = 0; i < this.filesToPost.length; i++) {
                this.createObject(this.filesToPost[i], i);
                count++;
            }
            this.dataService.sendFile(this.filesToPost, this.reportObject);
            this.clearAllFields();
        } catch (Exception) {
            this.toastService.error(Exception);
        }
    }

    //clearing all fields
    clearAllFields() {
        this.filesHolder = [];
        this.assayNameArrayHolder = [];
        this.reportObject = [];
        this.saveChosenAssay = [];
        this.saveChosenReport = [];
        this.majorVersionNumber = [];
        this.minorVersionNumber = [];
        this.servicePackNumber = [];
        this.buildNumber = [];
        this.filesToExtract = [];
    }

    createObject(file: any, i: any) {
        let fileToSend = new Report(file.name, this.selectedAssay[i],
            this.selectedReport[i], this.majorVersionNumber[i], this.minorVersionNumber[i], this.servicePackNumber[i], this.buildNumber[i]);
        let jsonObject = this.Jsonify(fileToSend);
        this.reportObject.push(jsonObject);
    }

    Jsonify(objectInput: any) {
        let body = JSON.stringify(objectInput);
        return body;
    }

    clearFileHolder() {
        this.filesHolder = [];
    }

    //To check for only numeric values
    _keyPressMajor(event: any) {
        const pattern = /[0-9\+\-\ ]/;
        let inputChar = String.fromCharCode(event.charCode);

        if (!pattern.test(inputChar)) {
            event.preventDefault();
        }
        else {
            this.countMajorFocus++;
        }
        if (this.countMajorFocus == 3) {
            let majorVersion = (<HTMLElement>document.getElementsByClassName('majorrVersion')[0]);
            let minorVersion = (<HTMLElement>document.getElementsByClassName('minorVersion')[0]);
            minorVersion.focus();
        }
    }

    //To check for only numeric values
    _keyPressMinor(event: any) {
        const pattern = /[0-9\+\-\ ]/;
        let inputChar = String.fromCharCode(event.charCode);

        if (!pattern.test(inputChar)) {
            event.preventDefault();
        }
        else {
            this.countMinorFocus++;
        }
        if (this.countMinorFocus == 3) {
            let minorVersion = (<HTMLElement>document.getElementsByClassName('minorVersion')[0]);
            let servicePack = (<HTMLElement>document.getElementsByClassName('servicePackageNumber')[0]);
            servicePack.focus();
        }
    }

    //To check for only numeric values
    _keyPressService(event: any) {
        const pattern = /[0-9\+\-\ ]/;
        let inputChar = String.fromCharCode(event.charCode);

        if (!pattern.test(inputChar)) {
            event.preventDefault();
        }
        else {
            this.countServiceFocus++;
        }
        if (this.countServiceFocus == 3) {
            let servicePack = (<HTMLElement>document.getElementsByClassName('servicePackageNumber')[0]);
            let buildNumberFocus = (<HTMLElement>document.getElementsByClassName('buildNumber')[0]);
            buildNumberFocus.focus();
        }
    }
}

/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/

import { Component, OnInit, OnDestroy, ViewContainerRef } from '@angular/core';
import { Observable } from 'rxjs/Observable'
import { CurveFile } from '../curverepo-models/CurveFile.component';
import { AssayType } from '../curverepo-models/AssayType.component';

import 'rxjs/Rx'

import { CurverepoStorageService } from '../curverepo-services/curverepo-storage.service';
import { CurverepoDatabaseService } from '../curverepo-services/curverepo-database.service'
import { $ } from "protractor/built";

import { ToastsManager } from 'ng2-toastr/ng2-toastr';

@Component({
  selector: 'app-curverepo-upload',
  templateUrl: './curverepo-upload.component.html',
  styleUrls: ['./curverepo-upload.component.scss']
})
export class CurverepoUploadComponent implements OnInit, OnDestroy {
  title = 'Hologic';
  upload = 'Upload';
  view = 'View';

  cbKnownStatus: boolean[] = [];

  TagField: string[] = [];
  InvalidTag: boolean[] = [];
  AssayField: string[] = [];
  AssayToSend: string[] = [];

  errorContainer: string[] = [];
  InvalidIndex: number[] = [];
  InvalidFile: string[] = [];
  errorMsg: string[] = [];
  curveType: string = null;
  testArray: object[];
  tempFileHolder: any[] = [];
  errorMessage;
  finalFiles: string[] = [];
  fileNameArray: string[] = [];
  curveObjects: string[] = [];
  IsKnown: boolean;

  InvalidStageFile: string[] = [];

  status: string;
  message: string;

  Indicator: boolean[] = [];

  assays: AssayType[] = [];

  localStorageHolder: any[] = [];

  constructor(private databaseService: CurverepoDatabaseService, public storageService: CurverepoStorageService,
    public toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }


  ngOnInit() {
    if (this.storageService.assays.length < 1) {
      this.databaseService.getAssay().subscribe(
        result => this.setUpAssay(JSON.parse(result)),
        error => this.errorMessage = <any>error
      )
    } else {
      this.assays = this.storageService.assays;
    }

    if (this.storageService.upload_containers != (null || undefined)) {
      var localStorageFiles = JSON.parse(this.storageService.upload_containers);
      for (let i = 0; i < localStorageFiles.length; i++) {

        let value = localStorageFiles[i];
        this.fileNameArray[i] = value.FullFileName;
        this.cbKnownStatus[i] = value.IsKnown;
        this.AssayField[i] = value.Assay;
        this.tempFileHolder[i] = atob(value.Data);
        this.TagField[i] = value.Tag;
        this.InvalidTag[i] = false;
      }
    }
  }

  ngOnDestroy() {
    for (let i = 0; i < this.fileNameArray.length; i++) {
      let localCurve = new CurveFile(this.TagField[i], this.cbKnownStatus[i], this.fileNameArray[i]);
      localCurve.Assay = this.AssayField[i];
      localCurve.Data = btoa(this.tempFileHolder[i]);

      this.localStorageHolder.push(localCurve);
    }
    this.storageService.upload_containers = JSON.stringify(this.localStorageHolder);
  }

  setUpAssay(input: any) {
    for (let i = 0; i < input.length; i++) {
      this.storageService.assays.push(new AssayType(input[i].assay.ID, input[i].assay.AssayName));
      this.assays.push(new AssayType(input[i].assay.ID, input[i].assay.AssayName));
    }

  }

  stageFile(fileInput: any) {
    for (let eachFile of fileInput.target.files) {
      if (this.fileNameArray.indexOf(eachFile.name) == -1) {
        this.tempFileHolder.push(eachFile);
        this.fileNameArray.push(eachFile.name);
        this.cbKnownStatus.push(false);
        this.AssayField.push("wait...");
        this.TagField.push("");
        this.InvalidTag.push(false);
      }
    }

    this.databaseService.matchAssay(this.fileNameArray, this.tempFileHolder).subscribe(
      result => this.setAssay(JSON.parse(result)),
      error => this.errorMessage = <any>error
    )

  }

  setAssay(result: any) {
    var offset = 0;
    for (let i = 0; i < result.length; i++) {

      if (result[i].Assay != "Invalid" && result[i].Assay != "Invalid-Outdated") {
        let index = this.fileNameArray.indexOf(result[i].FileName);

        for (let j = 0; j < this.assays.length; j++) {
          if (this.assays[j].name == result[i].Assay) {
            this.AssayField[i - offset] = this.assays[j].name;
          }
        }

      }
      else {

        this.tempFileHolder.splice(i - offset, 1);
        this.fileNameArray.splice(i - offset, 1);
        this.cbKnownStatus.splice(i - offset, 1);
        this.AssayField.splice(i - offset, 1);
        this.TagField.splice(i - offset, 1);
        this.InvalidTag.splice(i - offset, 1);

        offset++;
        //outdated assay
        if (result[i].Assay == "Invalid-Outdated") {
          this.InvalidStageFile.push(result[i].FileName+ " has outdated Assay");
        } else {
          this.InvalidStageFile.push(result[i].FileName);
        }
      }
    }

    //toaster
    if (this.InvalidStageFile.length != 0) {
      var FileStr = this.InvalidStageFile.toString();
      //var errorMsg = FileStr.split(',').join('<br/>');
      var errorMsg = FileStr.split(',').join('\r\n');

      this.toastr.error(errorMsg, 'Invalid File(s)!', { 
        dismiss: 'click',
        showCloseButton: true,
        animate: "fade"
      });
    }
    this.InvalidStageFile = [];

    //If all the files were invalid clear table
    if (this.fileNameArray.length == 0){
      this.clearTableAndTag();
    }
  }

  createObject(fileName: any, i: any) {
    let fileToSend = new CurveFile(this.TagField[i], this.cbKnownStatus[i],
      fileName.name);
    this.AssayToSend.push(this.AssayField[i]);
    let jsonObject = this.Jsonify(fileToSend);
    this.curveObjects.push(jsonObject);
  }

  Jsonify(objectInput: any) {
    let body = JSON.stringify(objectInput);
    return body;
  }

  uploadFile() {

    if (this.fileNameArray.length == 0) {
      this.status = "Error";
      this.message = "Please choose a file";
      this.toastr.error(this.message, 'Error!', { 
        dismiss: 'click',
        showCloseButton: true,
        animate: "fade"
      });
      return;
    }

    if (this.InvalidTag.indexOf(true) != -1) {
      this.status = "Error";
      this.message = "There is duplicate Tag";
      this.toastr.error(this.message, 'Error!', { 
        dismiss: 'click',
        showCloseButton: true,
        animate: "fade"
      });
      return;
    }

    for (let i = 0; i < this.tempFileHolder.length; i++) {
      this.ValidateTag(i);
      if (this.finalFiles.indexOf(this.tempFileHolder[i]) == -1) {
        this.createObject(this.tempFileHolder[i], i);
        this.finalFiles.push(this.tempFileHolder[i]);
        //updating fields of already staged files
      } else {
        this.AssayToSend[i] = this.AssayField[i];

        if (this.TagField[i] == ';') {
          this.TagField[i] = '';
        }
        //update tag
        let fileUpdatedForTag = new CurveFile(this.TagField[i], this.cbKnownStatus[i], this.fileNameArray[i]);
        let jsonObject = this.Jsonify(fileUpdatedForTag);
        this.curveObjects[i] = jsonObject;
      }
    }

    this.databaseService.sendFile(this.finalFiles, this.curveObjects, this.AssayToSend).subscribe(
      result => this.displayError(result),
      error => this.errorMessage = <any>error
    );
  }

  displayError(msg: any) {
    msg = JSON.parse(msg);
    if (msg == "Successful") {
      this.status = "Upload Successful!"
      this.message = "All files were uploaded!";
      this.toastr.success(this.message, this.status, { 
        showCloseButton: true,
        animate: "fade"
      });
      //after upload clear the table
      this.clearTableAndTag();
      return;
    }

    //reset if called more than once
    if (this.InvalidFile.length != 0) {
      this.InvalidFile = [];
      this.InvalidIndex = [];
      this.errorContainer = [];
    }

    for (let i = 0; i < msg.length; i++) {
      let WholeMsg = msg[i].split('^', 2);
      let fileName = WholeMsg[0];
      let errMsg = WholeMsg[1];

      this.InvalidFile.push(fileName);
      this.errorContainer.push(errMsg);
    }
    let numOfSuccessful = this.fileNameArray.length;
    let j = 0;
    for (let i = 0; i < this.fileNameArray.length; i++) {
      //remove valid files
      if (this.InvalidFile.indexOf(this.fileNameArray[i]) == -1) {
        this.removeItem(i);
        i--;
        j++;
      }
      //keep track of invalid file index
      else {
        this.InvalidIndex.push(i + j);
      }
    }

    for (let i = 0; i < msg.length; i++) {
      this.errorMsg[i] = this.errorContainer[i];
      document.getElementById("error" + i).style.display = 'block';
    }
  }

  ValidateTag(i) {
    //validating tag fields
    if (this.TagField[i].charAt(this.TagField[i].length - 1) != ';') {
      if (this.TagField[i].length != 0) {
        this.TagField[i] += ';';
      }
    }
  }

  hideError(i: any) {
    let doc = document as any;
    let element = doc.getElementById("error" + i);
    element.style.display = "none";
  }

  removeItem(index: any) {
    this.tempFileHolder.splice(index, 1);
    this.fileNameArray.splice(index, 1);
    this.finalFiles.splice(index, 1);
    this.TagField.splice(index, 1);
    this.AssayField.splice(index, 1);
    this.cbKnownStatus.splice(index, 1);

    if (this.fileNameArray.length == 0) {
      let doc = document as any;
      doc.getElementById("fileUpload").value = "";
    }
  }

  clearTableAndTag() {
    this.tempFileHolder = [];
    this.fileNameArray = [];
    this.finalFiles = [];
    this.errorMsg = [];
    this.curveObjects = [];
    this.cbKnownStatus = [];
    this.AssayField = [];
    this.TagField = [];
    this.AssayToSend = [];
    this.storageService.upload_containers = undefined;
    let doc = document as any;
    doc.getElementById("input-upload").value = "";
  }


  valueChange(newValue, i) {

    let el = (<HTMLInputElement>document.getElementById('Tag' + i));
    let currentCursorPos = this.doGetCaretPosition(el);
    let tagLength = this.TagField[i].length;

    if (tagLength !== 0) {
      if (currentCursorPos === tagLength) {
        el.value += ';';
        el.focus();
        el.setSelectionRange(el.value.length - 1, el.value.length - 1);
      }
      else if (el.value.charAt(el.value.length - 1) == ';' &&
        el.value.charAt(currentCursorPos - 1) == ';') {
        el.value = el.value.substr(0, el.value.length - 1);
        el.focus();
        el.setSelectionRange(el.value.length, el.value.length);
      }
    }
    if (el.value == ';') {
      el.value = "";
    }

    if (this.checkForDuplicate(el.value)) {
      el.style.backgroundColor = "#d9534f";
      this.InvalidTag[i] = true;
    } else {
      el.style.backgroundColor = "white";
      this.InvalidTag[i] = false;
    }
  }

  checkForDuplicate(value: string) {
    let dividedString: string[] = [];
    let tempStringHolder: string = "";

    for (let i = 0; i < value.length; i++) {
      if (value[i] != ';') {
        tempStringHolder += value[i];
      } else {
        dividedString.push(tempStringHolder);
        tempStringHolder = "";
      }
    }

    for (let j = 0; j < dividedString.length; j++) {
      for (let k = 0; k < dividedString.length; k++) {
        if (dividedString[j] == dividedString[k] && (j != k)) {
          return true;
        }
      }
    }
    return false;
  }

  doGetCaretPosition(oField: any) {
    // Initialize
    var iCaretPos = 0;

    const doc = document as any;

    // IE Support
    if (doc.selection) {

      // Set focus on the element
      oField.focus();

      // To get cursor position, get empty selection range
      var oSel = doc.selection.createRange();

      // Move selection start to 0 position
      oSel.moveStart('character', -oField.value.length);

      // The caret position is selection length
      iCaretPos = oSel.text.length;
    }
    else if (oField.selectionStart || oField.selectionStart == '0') {
      iCaretPos = oField.selectionStart;
    }
    // Return results
    return iCaretPos;
  }
}

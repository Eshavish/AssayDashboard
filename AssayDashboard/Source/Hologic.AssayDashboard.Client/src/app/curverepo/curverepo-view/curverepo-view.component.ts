/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/

import { Component, OnInit, OnDestroy, ViewContainerRef } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { CurverepoDatabaseService } from '../curverepo-services/curverepo-database.service'
import { CurverepoStorageService } from '../curverepo-services/curverepo-storage.service';
import { CurveFile } from '../curverepo-models/CurveFile.component';
import { AssayType } from '../curverepo-models/AssayType.component';
import { Combined } from '../curverepo-models/Combined.component';

import { saveAs } from 'file-saver';

import { ChartReadyEvent } from 'ng2-google-charts';
import { ChartErrorEvent } from 'ng2-google-charts';
import { ChartSelectEvent } from 'ng2-google-charts';
import { ChartMouseOverEvent, ChartMouseOutEvent } from 'ng2-google-charts';


import { ToastsManager } from 'ng2-toastr/ng2-toastr';

@Component({
  selector: 'app-curverepo-view',
  templateUrl: './curverepo-view.component.html',
  styleUrls: ['./curverepo-view.component.scss']
})

export class CurverepoViewComponent implements OnInit, OnDestroy {
  containers: Combined[] = [];
  errorMessage;
  ModalErrorMessage;

  cbFileStatus: boolean[] = [];
  selectAllIndicator: boolean = false;

  IsKnown: boolean = false;
  TagField: string;

  AssayField: string = "";
  AssayName: number;

  assayVersionField: string = "";
  softwareVersionField: string = "";

  assayVersion: any = [];
  softwareVersion: any = [];
  assayInDB: any = []

  constructor(private databaseService: CurverepoDatabaseService, public storageService: CurverepoStorageService,
    public toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.databaseService.getVersion().subscribe(
      result => this.setUpVersion(JSON.parse(result)),
      error => this.errorMessage = <any>error)

    this.databaseService.getAssayInDB().subscribe(
      result => this.setUpAssay(JSON.parse(result)),
      error => this.errorMessage = <any>error
    )

    let indicator = this.storageService.view_IsKnown;
    if (indicator == "true") {
      this.IsKnown = true;
    } else {
      this.IsKnown = false;
    }

    let tempTag = this.storageService.view_Tag;

    if (tempTag != "undefined") {
      this.TagField = tempTag;
    }
    if (tempTag == (null || undefined)) {
      this.TagField = undefined;
    }

    this.AssayField = this.storageService.view_Assay;
    this.assayVersionField = this.storageService.view_assayVersion;
    this.softwareVersionField = this.storageService.view_softwareVersion;

    if (this.containers != (undefined || null)) {
      this.containers = this.storageService.view_containers;
      this.cbFileStatus = this.storageService.cbFileStatus;
      this.selectAllIndicator = this.storageService.selectAll;
    }
  }

  ngOnDestroy() {
    //if user changed the field
    this.storageService.view_IsKnown = this.IsKnown.toString();
    this.storageService.view_Assay = this.AssayField;
    this.storageService.view_Tag = this.TagField;
    this.storageService.view_assayVersion = this.assayVersionField;
    this.storageService.view_softwareVersion = this.softwareVersionField;

    if (this.containers != (undefined || null)) {
      this.storageService.view_containers = this.containers;
      this.storageService.cbFileStatus = this.cbFileStatus;
      this.storageService.selectAll = this.selectAllIndicator;
    }
  }

  setUpAssay(input: any) {
    for (let i = 0; i < input.length; i++) {
      this.assayInDB.push(new AssayType(input[i].ID, input[i].AssayName));
    }
  }

  setUpVersion(input: any) {

      this.assayVersion = input.AssayVersion;
      this.softwareVersion = input.SoftwareVersion;

    if (this.assayVersion.length == 0) {
      document.getElementById("assayVersion").setAttribute("disabled", "disabled");
    }
    if (this.softwareVersion.length == 0) {
      document.getElementById("softwareVersion").setAttribute("disabled", "disabled");
    }
  }

  getFile() {

    if (this.TagField != null) {
      this.verifyTag();
    }

    if (this.AssayField == "") {
      this.AssayField = undefined;
    }

    let AssayToPass = 0;
    for (let i = 0; i < this.assayInDB.length; i++) {
      if (this.assayInDB[i].id == this.AssayField) {
        AssayToPass = this.assayInDB[i].id;
      }
    }
    if (AssayToPass == 0) {
      AssayToPass = -1;
    }
    //error msg when search requirements are not met
    if ((this.TagField == null || this.TagField == "") && (this.AssayField == undefined || this.AssayField == "")
      && AssayToPass == -1 && (this.softwareVersionField == undefined || this.softwareVersionField == "") &&
      (this.assayVersionField == undefined || this.assayVersionField == "")) {

      this.ModalErrorMessage = "Please specify Assay name, Tag, Assay Version or Software Version";
      this.toastr.error(this.ModalErrorMessage, 'Error!', {
        dismiss: 'click',
        showCloseButton: true,
        animate: "fade"
      });
      return;
    }

    if (this.TagField == "") {
      this.TagField = undefined;
    }
    else if (this.TagField != undefined) {
      if (this.checkForDuplicate(this.TagField)) {
        this.ModalErrorMessage = "Duplicate Tag is Invalid";
        this.toastr.error(this.ModalErrorMessage, 'Error!', {
          dismiss: 'click',
          showCloseButton: true,
          animate: "fade"
        });
        return;
      }
    }

    if (this.assayVersionField == "") {
      this.assayVersionField = undefined;
    }
    if (this.softwareVersionField == "") {
      this.softwareVersionField = undefined;
    }

    //console.log("when getting the file " + this.IsKnown + " " + this.TagField + " " + this.assayVersionField + " " + this.softwareVersionField + " " + AssayToPass);

    this.databaseService.getFile(this.IsKnown, this.TagField, this.assayVersionField, this.softwareVersionField, AssayToPass).subscribe(
      result => this.Objectify(JSON.parse(result), true),
      error => this.errorMessage = <any>error
    )

    if (this.TagField == "undefined") {
      this.TagField = "";
    }
  }

  Objectify(JSONstring: any, Indicator: boolean) {
    if (Indicator) {
      if (JSONstring == "No Matching Curve File Found") {
        this.ModalErrorMessage = "No Matching Curve File Found";
        this.toastr.error(this.ModalErrorMessage, 'Error!', {
          dismiss: 'click',
          showCloseButton: true,
          animate: "fade"
        });
        return;
      }
    }
    else {
      if (JSONstring == "No Matching Curve File Found") {
        return;
      }
    }

    this.containers = JSONstring;

    for (let i = 0; i < JSONstring.length; i++) {
        this.containers[i].dataSet = JSONstring[i].data;
    }

    this.SetUpTable(this.containers.length);

  }

  SetUpTable(size: number) {
    for (let i = 0; i < size; i++) {
      this.cbFileStatus.push(false);
      this.containers[i].curveFile.FormatCreationDate = this.formatDate(this.containers[i].curveFile.CreationDate);
    }
  }

  localStorageCurve(i: number) {
    this.storageService.curverepo_graph = JSON.stringify(this.containers[i].dataSet);
    this.storageService.curverepo_graphAssay = this.containers[i].AssayName;
    this.storageService.curverepo_fileName = this.containers[i].curveFile.FullFileName;
    this.storageService.curverepo_fileType = this.containers[i].FileType;
  }

  formatDate(date: Date) {
    let dateString = date.toString();
    let year = +dateString.substr(0, 4);
    let month = +dateString.substr(5, 2);
    let day = +dateString.substr(8, 2);

    let wholeDate = month + '/' + day + '/' + year;

    return wholeDate;
  }

  verifyTag() {

    if (this.TagField.charAt(this.TagField.length - 1) != ';') {
      this.TagField += ';';
    }
    if (this.TagField == ';' && this.TagField.length == 1) {
      this.TagField = '';
      return;
    }
  }


  DownloadAllFile(Indicator: boolean, index: number) {

    if (Indicator == true) {
      for (let i = 0; i < this.containers.length; i++) {
        if (this.cbFileStatus.indexOf(true) == -1) {
          this.ModalErrorMessage = "Please Select a file to Download";
          this.toastr.error(this.ModalErrorMessage, 'Error!', {
            dismiss: 'click',
            showCloseButton: true,
            animate: "fade"
          });
          return;
        }
        if (this.cbFileStatus[i] == true) {
          let data = this.containers[i].curveFile.Data;

          //byte array
          var byteCharacters = atob(data);

          var byteNumbers = new Array(byteCharacters.length);
          for (var j = 0; j < byteCharacters.length; j++) {
            byteNumbers[j] = byteCharacters.charCodeAt(j);
          }
          var byteArray = new Uint8Array(byteNumbers);

          var blob = new Blob([byteArray], { type: 'application/octet-stream' });
          saveAs(blob, this.containers[i].curveFile.FullFileName, false);
        }
      }
    }
    else {
      let data = this.containers[index].curveFile.Data;

      //byte array
      var byteCharacters = atob(data);

      var byteNumbers = new Array(byteCharacters.length);
      for (var j = 0; j < byteCharacters.length; j++) {
        byteNumbers[j] = byteCharacters.charCodeAt(j);
      }
      var byteArray = new Uint8Array(byteNumbers);

      var blob = new Blob([byteArray], { type: 'application/octet-stream' });
      saveAs(blob, this.containers[index].curveFile.FullFileName, false);
    }
  }

  SelectAll() {
    if (this.selectAllIndicator) {
      for (let i = 0; i < this.cbFileStatus.length; i++) {
        this.cbFileStatus[i] = true;
      }
    }
    else {
      for (let i = 0; i < this.cbFileStatus.length; i++) {
        this.cbFileStatus[i] = false;
      }
    }
  }

  clearAll() {
    this.containers = [];
    this.cbFileStatus = [];
    this.selectAllIndicator = false;
    this.IsKnown = false;
    this.AssayField = "";
    this.assayVersionField = "";
    this.softwareVersionField = "";
    this.TagField = undefined;
    this.storageService.view_Assay = undefined;
    this.storageService.view_containers = [];
    this.storageService.view_IsKnown = undefined;
    this.storageService.view_Tag = undefined;
    this.storageService.curverepo_fileName = undefined;
  }


  removeItem(index: any) {
    this.containers.splice(index, 1);
    this.cbFileStatus.splice(index, 1);

    if (this.containers.length == 0) {
      this.clearAll();
    }
  }

  valueChange(newValue) {
    let el = (<HTMLInputElement>document.getElementById('Tag'));
    let currentCursorPos = this.doGetCaretPosition(el);
    let tagLength = this.TagField.length;

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
    } else {
      el.style.backgroundColor = "white";
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

  changeFileStatus(index: number) {
    this.cbFileStatus[index] = !this.cbFileStatus[index];
    if (this.selectAllIndicator && this.cbFileStatus.indexOf(false) != -1) {
      this.selectAllIndicator = !this.selectAllIndicator;
    }
    if (!this.selectAllIndicator && this.cbFileStatus.indexOf(false) == -1) {
      this.selectAllIndicator = !this.selectAllIndicator;
    }
  }
}

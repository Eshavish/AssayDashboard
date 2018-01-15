/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { RequestOptions, Request, RequestMethod, Headers } from '@angular/http';
import 'rxjs/Rx';
import { FormsModule } from '@angular/forms';
import { Report } from "app/reportrepo/reportrepo-models/reportrepo-reportclass.component";
import * as $ from "jquery";
import { Router } from '@angular/router';

@Component({
  moduleId: module.id,
  templateUrl: './reportrepo-frontpage.component.html',
  styleUrls: ['./reportrepo-frontpage.component.scss'],
  providers: [],
})

export class FrontPage {
  uploadVisible: boolean = true;
  downloadVisible: boolean = false;
  helpVisible: boolean = false;
  width: number;

  //Upload checkbox selected
  uploadSelected() {
    this.uploadVisible = true;
    this.downloadVisible = false;
    this.helpVisible = false;
  }

  //Download checkbox selected
  downloadSelected() {
    this.uploadVisible = false;
    this.downloadVisible = true;
    this.helpVisible = false;
  }

  checkBeforeSwitching() {
  }

  openNav() {
    this.width = 180;
  }

  closeNav() {
    this.width = 0;
  }
}



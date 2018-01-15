/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { DatabaseService } from '../reportrepo-services/reportrepo-dbservice';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { DashboardComponent } from '../../dashboard/dashboard.component';
import { DowloadFiles } from "app/reportrepo/reportrepo-download/reportrepo-download.component";
import { UploadFiles } from "app/reportrepo/reportrepo-upload/reportrepo-upload.component";
import { FrontPage } from "app/reportrepo/reportrepo-frontpage/reportrepo-frontpage.component";

@NgModule({
  declarations: [
    DowloadFiles,
    UploadFiles,
    FrontPage,
  ],
  
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule,
    RouterModule.forRoot(
      [
      ]
    ),
    ToastrModule.forRoot(
      {
      }
    ),
  ],
  exports: [
    DowloadFiles,
    FrontPage,
    UploadFiles
  ],
  providers: [DatabaseService],
})

export class ReportModule { }

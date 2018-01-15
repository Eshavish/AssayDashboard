/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/

import { UploadFiles } from '../reportrepo-upload/reportrepo-upload.component';
import { TestBed, async, inject } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { DebugElement } from "@angular/core/src/debug/debug_node";
import { ComponentFixture } from "@angular/core/testing";
import { By } from "@angular/platform-browser/src/dom/debug/by";
import { ToastrService } from 'ngx-toastr';
import {
  HttpModule, Http,
  Response,
  ResponseOptions,
  XHRBackend
} from '@angular/http';
import { DatabaseService } from '../reportrepo-services/reportrepo-dbservice';

describe('UploadFiles', () => {
  let comp: UploadFiles;
  let fixture: ComponentFixture<UploadFiles>;
  let de: DebugElement;
  let el: HTMLElement;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UploadFiles],
      providers: [DatabaseService],
    });
    fixture = TestBed.createComponent(UploadFiles);
    comp = fixture.componentInstance;
    this.dataService = fixture.debugElement.injector.get(DatabaseService);
  });
});


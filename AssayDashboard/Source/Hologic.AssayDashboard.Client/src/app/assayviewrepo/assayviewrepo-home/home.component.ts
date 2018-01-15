/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
import { Component, HostBinding, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { Assay } from "app/assayviewrepo/assayviewrepo-models/assay";
import { ParserService } from "app/assayviewrepo/parser.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  @HostBinding('style.background-color') private background = '#ffffff';
  @HostBinding('style.color') private color = '#969696';
  public loading: boolean = false;
  public assay: Assay;

  constructor(private _router: Router, private parserServer: ParserService) {
    this.assay = this.parserServer.getFile(2);
   }

  onManager(): void {
    this._router.navigate(['/manager']);
  }
}

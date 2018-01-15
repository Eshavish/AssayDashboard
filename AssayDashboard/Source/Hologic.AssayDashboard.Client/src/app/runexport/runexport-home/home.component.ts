/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
import { Component, HostBinding, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { Run } from "app/runexport/runexport-models/run";
import { RunService } from "app/runexport/run.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class RunHomeComponent {
  @HostBinding('style.background-color') private background = '#ffffff';
  @HostBinding('style.color') private color = '#969696';
  public loading: boolean = false;
  public run: Run;

  constructor(private _router: Router, private parserServer: RunService) {
    this.run = this.parserServer.getFile(2);
  }

  onManager(): void {
    this._router.navigate(['/RunManager']);
  }
}

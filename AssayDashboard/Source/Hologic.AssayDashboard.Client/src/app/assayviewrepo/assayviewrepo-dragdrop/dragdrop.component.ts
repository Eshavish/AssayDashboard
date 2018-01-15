/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
import { Component, HostListener, HostBinding } from '@angular/core';
import * as xmldeserializer from 'xmldeserializer';
import { Router } from '@angular/router';
import { ParserService } from "app/assayviewrepo/parser.service";

@Component({
  selector: 'app-dragdrop',
  templateUrl: './dragdrop.component.html',
  styleUrls: ['./dragdrop.component.scss']
})
export class DragdropComponent {
  @HostBinding('style.background') private background = '#ffffff';
  @HostBinding('style.color') private color = '#969696';
  public loading: boolean = false;

  constructor(private _parserService: ParserService,
    private _router: Router) { }

changeListener(event: any): void {
    let files = event.target.files;
    let count = this._parserService.getFileCount();
    if (files.length > 0) {
      this.loading = true;
      for (let i = 0; i < files.length; i++) {
        this._parserService.addFile(files[i]);
      }
      setTimeout(() => {
        if (this._parserService.getFileCount() > count) 
          this._router.navigate(['/assay', count]);
        
        this.loading = false;
      }, 1000);
    }
  }

  @HostListener('dragover', ['$event']) public onDragOver(evt) {
    evt.preventDefault();
    evt.stopPropagation();
    this.background = 'rgba(0, 153, 255, .07)'
    this.color = '#337ab7';
  }

  @HostListener('dragleave', ['$event']) public onDragLeave(evt) {
    evt.preventDefault();
    evt.stopPropagation();
    this.background = '#ffffff'
    this.color = "#969696";
  }

  @HostListener('drop', ['$event']) public onDrop(evt) {
    evt.preventDefault();
    evt.stopPropagation();
    let files = evt.dataTransfer.files;
    let count = this._parserService.getFileCount();
    if (files.length > 0) {
      this.background = '#ffffff'
      this.color = '#969696';
      this.loading = true;

      for (let i = 0; i < files.length; i++)
        this._parserService.addFile(files[i]);

      setTimeout(() => {
        if (this._parserService.getFileCount() > count)
          this._router.navigate(['/assay', count]);
        this.loading = false;
      }, 1000);
    }
  }
}

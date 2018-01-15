/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
import { Component, OnInit, HostListener, ChangeDetectionStrategy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { saveAs } from "file-saver";
import { loginScreenAnimation } from "app/assayviewrepo/assayviewrepo-animations/login.animation";
import { routerTransition } from "app/assayviewrepo/assayviewrepo-animations/slide_animation";
import { Assay } from "app/assayviewrepo/assayviewrepo-models/assay";
import { ParserService } from "app/assayviewrepo/parser.service";

@Component({
  selector: 'app-details',
  templateUrl: './assaydetail.component.html',
  styleUrls: ['./assaydetail.component.scss'],
  //changeDetection: ChangeDetectionStrategy.OnPush
  //animations: [loginScreenAnimation(), routerTransition()],
  //host: { '[@handleTransition]': '' },
})

export class AssayDetailComponent implements OnInit {
  public pageTitle: string = 'Assay Details';
  public index: number;
  public totalFiles: number;
  public assay: Assay;
  public isClicked: number;
  public loading: boolean = false;

  constructor(private _router: Router,
    private _activatedRoute: ActivatedRoute,
    private _parserService: ParserService) { }

  ngOnInit() {
    if(this._parserService.getFileCount() === 0) {
        this._router.navigate(['/home']);
        return;
    }
    this._activatedRoute.paramMap.subscribe(
      params => {
        this.index = +params.get('id');
        this.totalFiles = this._parserService.getFileCount();
        this.assay = this._parserService.getFile(this.index);
        
        if(this.assay === undefined)
          this._router.navigate(['/home']);
      }
    );
  }

  onBack(): void {
    this._router.navigate(['/manager']);
  }

  onNext(): void {
    this.reduceRows();
    let newIndex = this.index + 1;
    if (this.index >= this._parserService.getFileCount() - 1) 
      newIndex = 0;
    
        //this._router.navigate(['/assay', newIndex]);
        //window.scrollTo(0, 0)
    this._router.navigateByUrl(`/manager`, { skipLocationChange: true }).then(
      () => {
        this._router.navigate(['/assay', newIndex]);
        //window.scrollTo(0, 0)
      });
  }

  // Load next assay file and refresh the component to display the data
  onPrevious(): void {
    this.reduceRows();
    let newIndex = this.index - 1;
    if (this.index <= 0) 
      newIndex = this._parserService.getFileCount() - 1;
    
        //this._router.navigate(['/assay', newIndex]);
        //window.scrollTo(0, 0)
        //
    this._router.navigateByUrl(`/manager`, { skipLocationChange: true }).then(
      () => {
        this._router.navigate(['/assay', newIndex]);
        //window.scrollTo(0, 0)
      });
  }

  fixCrc() {
    let xmlText = this.assay.xmlText;
    let crc = ' ObjectCRC="' + this.assay.objectCrc + '"';
    this.assay.xmlText = xmlText.replace(crc, ' ObjectCRC="'+ this.assay.calculatedCrc + '"');
    this.assay.objectCrc = this.assay.calculatedCrc;
    var file = new File([this.assay.xmlText], this.assay.fileName, {type:"application/xml"});
    saveAs(file); 
    this.assay.xmlText = '';
    localStorage.setItem('AV_' + this.assay.fileName, JSON.stringify(this.assay));
  }

  reduceRows() {
    let totalCategories = this.assay.dataReduction.resultCategories.length;
    for(let i = 0; i < totalCategories; i++) {
      this.assay.dataReduction.resultCategories[i].active = false;
    }
  }

  /** Blocks default behavior when something is dragged over the component. */
  @HostListener('dragover', ['$event']) public onDragOver(evt) {
    evt.preventDefault();
    evt.stopPropagation();
  }

  /** Blocks default behavior when something is dragged over the component. */
  @HostListener('dragleave', ['$event']) public onDragLeave(evt) {
    evt.preventDefault();
    evt.stopPropagation();
  }

  /** Blocks default behavior when something is dropped on the component. */
  @HostListener('drop', ['$event']) public onDrop(evt) {
    evt.preventDefault();
    evt.stopPropagation();
    let files = evt.dataTransfer.files;
    let count = this._parserService.getFileCount();
    if (files.length > 0) {
      this.loading = true;

      for (let i = 0; i < files.length; i++)
        this._parserService.addFile(files[i]);

      setTimeout(() => {
        this.loading = false;

        if (this._parserService.getFileCount() > count) {
          this._router.navigate(['/assay', count]);
          window.scrollTo(0, 0);
        }
      }, 1000);
    }
  }
}

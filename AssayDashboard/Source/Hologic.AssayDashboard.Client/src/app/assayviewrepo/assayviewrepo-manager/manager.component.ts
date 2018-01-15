/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
import { Component, OnInit, HostBinding, HostListener } from '@angular/core';
import { Assay } from "app/assayviewrepo/assayviewrepo-models/assay";
import { Router } from '@angular/router';
import { Http } from '@angular/http';
import { HttpErrorResponse } from '@angular/common/http';
import { ParserService } from "app/assayviewrepo/parser.service";

@Component({
  selector: 'app-manager',
  templateUrl: './manager.component.html',
  styleUrls: ['./manager.component.scss'],
})

export class ManagerComponent implements OnInit {
  public files: Assay[];
  public filteredFiles: Assay[];
  public loading: boolean = false;

  constructor(private _parserService: ParserService,
      private http: Http, 
      private _router: Router) { 
        this.filteredFiles = this.files;
      }

  _listFilter: string;
  get listFilter(): string {
      return this._listFilter;
  }
  set listFilter(value: string) {
      this._listFilter = value;
      this.filteredFiles = this.listFilter ? this.performFilter(this.listFilter) : this.files;
  }

  ngOnInit() {
    this.files = this._parserService.getFiles();
    this.listFilter = '';
  }

  performFilter(filterBy: string): Assay[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.files.filter((assay: Assay) => 
      assay.assayName.toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

  /** Removes a file from the assay list. */
  deleteItem(index: number) {
    this._parserService.removeElement(index);
  }

  changeListener(event: any): void {
    let files = event.target.files;
    let count = this._parserService.getFileCount();
    if (files.length > 0) {
      this.loading = true;
      for (let i = 0; i < files.length; i++) {
        this._parserService.addFile(files[i]);
      }
      setTimeout(() => {
        this.loading = false;

        if (this._parserService.getFileCount() > count)
          this._router.navigate(['/assay', count]);
      }, 1000);
    }
  }

  /** Navigates to assay details page of selected assay. */
  viewAssay(index: number) {
    this._router.navigate(['/assay', index]);
    window.scrollTo(0, 0)
  }

  /** Deletes all assay files stored in the application. */
  deleteAllFiles() {
    this._parserService.clear();
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

        if (this._parserService.getFileCount() > count)
          this._router.navigate(['/assay', count]);

      }, 1000);
    }
  }
}

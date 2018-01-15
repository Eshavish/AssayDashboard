/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
import { Component, OnInit, HostBinding, HostListener } from '@angular/core';
import { Run } from "app/runexport/runexport-models/run";
import { Router } from '@angular/router';
import { Http } from '@angular/http';
import { HttpErrorResponse } from '@angular/common/http';
import { RunService } from "app/runexport/run.service";

@Component({
  selector: 'app-manager',
  templateUrl: './manager.component.html',
  styleUrls: ['./manager.component.scss'],
})

export class ManagerComponent implements OnInit {
  public files: Run[];
  public filteredFiles: Run[];
  public loading: boolean = false;

  constructor(private _parserService: RunService,
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
    this.filteredFiles = this.files;
    console.log("in manager " + this.files);
    this.listFilter = '';
  }

  performFilter(filterBy: string): Run[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.files.filter((run: Run) => 
      run.AssayName.toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

  /** Removes a file from the run list. */
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
          this._router.navigate(['/run', count]);
      }, 1000);
    }
  }

  /** Navigates to run details page of selected run. */
  viewRun(index: number) {
    this._router.navigate(['/run', index]);
    window.scrollTo(0, 0)
  }

  /** Deletes all run files stored in the application. */
  deleteAllFiles() {
    this._parserService.clear();
    this.filteredFiles = [];
    this.files = [];
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
          this._router.navigate(['/run', count]);

      }, 1000);
    }
  }
}

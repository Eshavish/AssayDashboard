/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
import { Component, OnInit, HostListener, ChangeDetectionStrategy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { saveAs } from "file-saver";
import { loginScreenAnimation } from "app/runexport/runexport-animations/login.animation";
import { routerTransition } from "app/runexport/runexport-animations/slide_animation";
import { Run } from "app/runexport/runexport-models/run";
import { RunService } from "app/runexport/run.service";

@Component({
  selector: 'app-details',
  templateUrl: './rundetail.component.html',
  styleUrls: ['./rundetail.component.scss'],
  //changeDetection: ChangeDetectionStrategy.OnPush
  //animations: [loginScreenAnimation(), routerTransition()],
  //host: { '[@handleTransition]': '' },
})

export class RunDetailComponent implements OnInit {
  public pageTitle: string = 'Run Details';
  public index: number;
  public totalFiles: number;
  public run: Run;
  public isClicked: number;
  public loading: boolean = false;
  public rawDataReduction: any[] = [];
  public specialDataReduction: any[] = [];

  constructor(private _router: Router,
    private _activatedRoute: ActivatedRoute,
    private _parserService: RunService) { }

  ngOnInit() {
    if (this._parserService.getFileCount() === 0) {
      this._router.navigate(['/home']);
      return;
    }
    this._activatedRoute.paramMap.subscribe(
      params => {
        this.index = +params.get('id');
        this.totalFiles = this._parserService.getFileCount();
        this.run = this._parserService.getFile(this.index);

        let reSpace = / /gi;
        let reComma = /,/gi;

        if (this.run.TestOrders.TestOrder[0].Result.DetailedDataReductionRes.DetailedDataReductionResString != null) {
          for (let index = 0; index < this.run.TestOrders.TestOrder.length; index++) {
            //special case for channels
            if (this.run.TestOrders.TestOrder[index].Result.DetailedDataReductionRes.DetailedDataReductionResString.substring(0, 3) == "RTF") {
              let toInsert = this.run.TestOrders.TestOrder[index].Result.DetailedDataReductionRes.DetailedDataReductionResString.split(",");
              let tempArray = [];
              let testOrderHolder = [];
              for (let index = 0; index < toInsert.length; index++) {
                if (toInsert[index].includes("RTF") && index != 0) {
                  testOrderHolder.push(tempArray);
                  tempArray = [];
                }
                tempArray.push(toInsert[index]);
              }
              testOrderHolder.push(tempArray);

              this.specialDataReduction.push(testOrderHolder);
            }
            else {
              let spaceRemoved = this.run.TestOrders.TestOrder[index].Result.DetailedDataReductionRes.DetailedDataReductionResString.replace(reSpace, "*");
              let commaRemoved = spaceRemoved.replace(reComma, "");
              let newLineRemoved = commaRemoved.replace(/\n/g, "");
              let splitArray = newLineRemoved.split("*");
              let toInsert = this.combineKeyAndValue(splitArray);
              this.rawDataReduction.push(toInsert);
            }
          }
        }
        console.log(this.run);
        
        if (this.run === undefined)
          this._router.navigate(['/home']);
      }
    );
  }

  onBack(): void {
    this._router.navigate(['/RunManager']);
  }

  onNext(): void {
    // this.reduceRows();
    let newIndex = this.index + 1;
    if (this.index >= this._parserService.getFileCount() - 1)
      newIndex = 0;

    this._router.navigateByUrl(`/RunManager`, { skipLocationChange: true }).then(
      () => {
        this._router.navigate(['/run', newIndex]);

      });
  }

  // Load next assay file and refresh the component to display the data
  onPrevious(): void {
    let newIndex = this.index - 1;
    if (this.index <= 0)
      newIndex = this._parserService.getFileCount() - 1;

    this._router.navigateByUrl(`/RunManager`, { skipLocationChange: true }).then(
      () => {
        this._router.navigate(['/run', newIndex]);
        window.scrollTo(0, 0)
      });
  }

  //combines first and second (key and value) as individual array
  combineKeyAndValue(input: string[]) {
    let toReturn: any[] = [];
    let add: string[] = [];
    let offset = 0;

    for (let index = 0; index + offset < input.length - offset; index++) {

      //special case for --- tube --- case
      if (input[index] == "---" && index == 0) {
        add.push(input[1]);
        add.push(input[2]);
        toReturn.push(add);
        add = [];
        add.push(input[3].substring(3, input[3].length - 1));
        add.push(input[4]);
        toReturn.push(add);
        add = [];
        add.push(input[5]);
        add.push(input[6]);
        toReturn.push(add);
        add = [];
        add.push(input[7]);
        add.push(input[8]);
        toReturn.push(add);
        add = [];
        add.push(input[9]);
        add.push(input[10].substring(0, input[10].length - 5));
        toReturn.push(add);
        add = [];
        add.push(input[10].substring(input[10].length - 5, input[10].length));
        add.push(input[11]);
        toReturn.push(add);
        index = 11;
        continue;
      }

      if (index % 2 == 0) {
        if (!input[index].includes(":")) {
          offset++;
          continue;
        }
        add = [];
        add.push(input[index]);
      }
      else {
        if (input[index].includes(":")) {
          add.push("---");
          continue;
        }
        add.push(input[index]);
        toReturn.push(add);
      }
    }
    return toReturn;
  }

  toggle(type: string, index: number) {
    let button = document.getElementById(type + index.toString());
    if (button.style.display === "none") {
      button.style.display = "block";
    } else {
      button.style.display = "none";
    }
  }

  toggleDataReduc(testorderIndex: number, dataReducIndex: number){
    let button = document.getElementById("DataReduction" + testorderIndex.toString() + dataReducIndex.toString());
    if (button.style.display === "none") {
      button.style.display = "block";
    } else {
      button.style.display = "none";
    }
  }

  showChannel(resultIndex: number, channelIndex: number) {
    for (let count = 0; count < channelIndex; count++) {
      let button = document.getElementById("OuterChannel" + resultIndex.toString() + count.toString());
      if (button.style.display === "none") {
        button.style.display = "block";
      } else {
        button.style.display = "none";
        document.getElementById("channel" + resultIndex.toString() + count.toString()).style.display = 'none';
      }
    }
  }

  toggleChannel(testOrderIndex: number, channelIndex: number) {
    let button = document.getElementById("channel" + testOrderIndex.toString() + channelIndex.toString());
    if (button.style.display === "none") {
      button.style.display = "block";
    } else {
      button.style.display = "none";
    }
  }

  toggleResult(index: number) {
    let button = document.getElementById("resultCategory" + index.toString());
    if (button.style.display === "none") {
      button.style.display = "block";
    } else {
      button.style.display = "none";
    }
  }

  collapseAll() {
    for (let index = 0; index < this.run.TestOrders.TestOrder.length; index++) {
      document.getElementById("TestOrder" + index.toString()).style.display = "none";
      document.getElementById("resultCategory" + index.toString()).style.display = "none";
      document.getElementById("dataReduc" + index.toString()).style.display = "none";
     
      if (this.rawDataReduction.length == null){
        for (let dataIndex = 0; dataIndex < this.specialDataReduction[index].length; dataIndex++){
          document.getElementById("DataReduction" + index.toString() + dataIndex.toString()).style.display = "none";
        }
      }

      if (this.run.TestOrders.TestOrder[index].Result.DetailedDataReductionRes.Tube != null) {
        for (let channelIndex = 0; channelIndex < this.run.TestOrders.TestOrder[index].Result.DetailedDataReductionRes.Tube.Channel.length; channelIndex++) {
          document.getElementById("channel" + index.toString() + channelIndex.toString()).style.display = "none";
          document.getElementById("OuterChannel" + index.toString() + channelIndex.toString()).style.display = "none";
        }
      }
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
          this._router.navigate(['/run', count]);
          window.scrollTo(0, 0);
        }
      }, 1000);
    }
  }
}

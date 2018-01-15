/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
import { Component, OnInit, ViewChild } from '@angular/core';

import { ChartReadyEvent } from 'ng2-google-charts';
import { ChartErrorEvent } from 'ng2-google-charts';
import { ChartSelectEvent } from 'ng2-google-charts';
import { ChartMouseOverEvent, ChartMouseOutEvent } from 'ng2-google-charts';

import { CurverepoStorageService } from '../curverepo-services/curverepo-storage.service';

@Component({
  selector: 'app-curverepo-graph',
  templateUrl: './curverepo-graph.component.html',
  styleUrls: ['./curverepo-graph.component.scss']
})
export class CurverepoGraphComponent implements OnInit {

  graphInfo: any;
  graphAssay: string;
  lineChartData: any;
  fileName: string;
  fileType: string;

  dataSet: any;
  finalDataSet: Channel[] = [];
  finalSampleID: any[] = [];


  checkBox: boolean[] = [];
  sampleCheckBox: boolean[] = [];

  sampleIDHolder: any[] = [];

  constructor(public storageService: CurverepoStorageService) { }

  ngOnInit() {
    if (this.storageService.curverepo_fileName == (null || undefined)) {
      document.getElementById("controller").style.display = 'none';
      document.getElementById("noGraph").style.display = 'block';
      document.getElementById('helper').style.display = 'none';
      return;
    }
    document.getElementById("noGraph").style.display = 'none';

    this.graphInfo = JSON.parse(this.storageService.curverepo_graph);
    this.graphAssay = this.storageService.curverepo_graphAssay;
    this.fileName = this.storageService.curverepo_fileName;
    this.fileType = this.storageService.curverepo_fileType;

    if (this.graphInfo == null) {
      return;
    }
    this.dataSet = this.graphInfo.channels;
    for (let data in this.dataSet) {
      this.checkBox.push(false);
    }

    for (let key in this.dataSet[0].dict) {
      this.sampleIDHolder.push(key);
      this.sampleCheckBox.push(false);
    }

    this.populate();
  }

  populate() {
    document.getElementById('helper').style.display = 'none';
    var yTitle;
    var xTitle;

    //determine x and y axis title
    if (this.fileType == "TCYC"){
      xTitle = "Cycle Number";
      yTitle = "Fluorescence";
    }
    else if (this.fileType == "Fluo")
    {
      xTitle = "TimeStamp";
      yTitle = "Measurement";
    }

    //no graph is selected
    if (this.checkBox.indexOf(true)  == -1 && this.sampleCheckBox.indexOf(true) == -1) {
      this.lineChartData = undefined;
      var element = document.getElementById('chart');
      element.style.display = 'none';
      element = document.getElementById('helper');
      element.style.display = 'block';
      return;
    }

    var element = document.getElementById('chart');
    element.style.display = 'block';

    this.finalDataSet = [];
    let lineChart = new Array<Array<any>>();
    let topRow = new Array<any>();
    topRow.push("CycleNum");

    //getting length of the data
    var keyDictLength;
    //getting first key 
    for (let key in this.dataSet[0].dict) {
      keyDictLength = this.dataSet[0].dict[key].xAxis.length;
    }

    if (this.sampleCheckBox.indexOf(true) == -1) {
      //adding header 
      for (let i = 0; i < this.dataSet.length; i++) {
        if (this.checkBox[i]) {
          this.finalDataSet.push(this.dataSet[i]);
          //only one channel is selected
          for (let key in this.dataSet[i].dict) {
            topRow.push(this.dataSet[i].channel + '-' + key);
          }
        }
      }
      lineChart.push(topRow);

      var indicator = true;

      //iterate through the number of columns
      for (let i = 0; i < keyDictLength; i++) {
        var row = new Array<any>();
        //iterate through the dictionary
        for (let j = 0, indicator = true; j < this.finalDataSet.length; j++) {
          for (let key in this.finalDataSet[j].dict) {
            //first column
            if (indicator) {
              row.push(this.finalDataSet[j].dict[key].xAxis[i]);
              row.push(this.finalDataSet[j].dict[key].yAxis[i]);
              indicator = false;
            }
            else {
              row.push(this.finalDataSet[j].dict[key].yAxis[i]);
            }
          }
        }
        lineChart.push(row);
      }

      var customColor = [];
      if (this.finalDataSet.length > 1) {
        for (let i = 0; i < this.finalDataSet.length; i++) {
          var color = '#' + (0x1000000 + (Math.random()) * 0xffffff).toString(16).substr(1, 6);
          for (let j = 0; j < Object.keys(this.finalDataSet[i].dict).length; j++) {
            customColor.push(color);
          }
        }
      } else {
        for (let j = 0; j < Object.keys(this.finalDataSet[0].dict).length; j++) {
          var color = '#' + (0x1000000 + (Math.random()) * 0xffffff).toString(16).substr(1, 6);
          customColor.push(color);
        }
      }
    }
    //when only sample id is checked
    else if (this.checkBox.indexOf(true) == -1) {
      this.finalDataSet = [];
      this.finalSampleID = [];

      //setting up the header
      for (let i = 0; i < this.sampleCheckBox.length; i++) {
        if (this.sampleCheckBox[i]) {
          //push in sampleID of each datasets
          for (let j = 0; j < this.dataSet.length; j++) {
            for (let key in this.dataSet[j].dict) {
              if (key == this.sampleIDHolder[i]) {
                this.finalSampleID.push(key);
                topRow.push(this.dataSet[j].channel + '-' + key);
              }
            }
          }
        }
      }
      lineChart.push(topRow);
      let indicator = true;

      for (let rowIndex = 0; rowIndex < keyDictLength; rowIndex++) {
        let row = new Array<any>();
        for (let i = 0; i < this.dataSet.length; i++) {
          for (let key in this.dataSet[i].dict) {
            if (this.finalSampleID.indexOf(key) != -1) {
              if (indicator) {
                row.push(this.dataSet[i].dict[key].xAxis[rowIndex]);
                indicator = false;
              }
              row.push(this.dataSet[i].dict[key].yAxis[rowIndex]);
            }
          }
        }
        indicator = true;
        lineChart.push(row);
      }

      var customColor = [];
      var colorCount = 0;
      //determine how many are checked
      for (let index = 0; index < this.sampleCheckBox.length; index++) {
        if (this.sampleCheckBox[index] == true) {
          colorCount++;
        }
      }
      if (colorCount > 1) {
        for (let i = 0; i < colorCount; i++) {
          var color = '#' + (0x1000000 + (Math.random()) * 0xffffff).toString(16).substr(1, 6);
          for (let j = 0; j < this.dataSet.length; j++) {
            customColor.push(color);
          }
        }
      } else {
        for (let j = 0; j < this.dataSet.length; j++) {
          var color = '#' + (0x1000000 + (Math.random()) * 0xffffff).toString(16).substr(1, 6);
          customColor.push(color);
        }
      }

    }
    //when both channel and sampleID are checked
    else {
      this.finalDataSet = [];
      var sampleID = [];

      for (let index = 0; index < this.checkBox.length; index++) {
        if (this.checkBox[index]) {
          this.finalDataSet.push(this.dataSet[index]);
        }
      }

      for (let sampleIndex = 0; sampleIndex < this.sampleCheckBox.length; sampleIndex++) {
        if (this.sampleCheckBox[sampleIndex]) {
          sampleID.push(this.sampleIDHolder[sampleIndex]);
        }
      }

      //setting up the headerFF
      for (let index = 0; index < this.finalDataSet.length; index++) {
        for (let sampleIndex = 0; sampleIndex < sampleID.length; sampleIndex++) {
          topRow.push(this.finalDataSet[index].channel + '-' + sampleID[sampleIndex]);
        }
      }
      lineChart.push(topRow);

      var indicator = true;

      for (let rowIndex = 0; rowIndex < keyDictLength; rowIndex++) {
        let row = new Array<any>();
        for (let index = 0; index < this.finalDataSet.length; index++) {
          for (let sampleIndex = 0; sampleIndex < sampleID.length; sampleIndex++) {
            if (indicator) {
              row.push(this.finalDataSet[index].dict[sampleID[sampleIndex]].xAxis[rowIndex]);
              indicator = false;
            }
            row.push(this.finalDataSet[index].dict[sampleID[sampleIndex]].yAxis[rowIndex]);
          }
        }
        indicator = true;
        lineChart.push(row);
      }

      customColor = [];
      if (this.finalDataSet.length > 1) {
        for (let index = 0; index < this.finalDataSet.length; index++) {
          var color = '#' + (0x1000000 + (Math.random()) * 0xffffff).toString(16).substr(1, 6);
          for (let sampleIndex = 0; sampleIndex < sampleID.length; sampleIndex++) {
            customColor.push(color);
          }
        }
      } else {
        for (let index = 0; index < sampleID.length; index++) {
          var color = '#' + (0x1000000 + (Math.random()) * 0xffffff).toString(16).substr(1, 6);
          customColor.push(color);
        }
      }
    }

    //get screen resolution
    var graphWidth = screen.width * 0.7;
    var graphHeight = screen.height * 0.9; 

    this.lineChartData = {
      chartType: 'LineChart',
      dataTable: lineChart,
      options: {
        title: 'Curve Graph',
        width: graphWidth, height: graphHeight,
        vAxis: { title: yTitle },
        hAxis: { title: xTitle },
        explorer: {
          axis: 'horizontal',
          keepInBounds: true,
          maxZoomIn: 4.0
        },
        colors: customColor
      }
    }
  }

  clearAll() {
    for (let index = 0; index < this.checkBox.length || index < this.sampleCheckBox.length; index++) {
      this.checkBox[index] = false;
      this.sampleCheckBox[index] = false;
    }
    var element = document.getElementById('chart');
    element.style.display = 'none';
    document.getElementById('helper').style.display = 'block';
  }
}

class DataSet {
  channels: any[] = [];
}
class Channel {
  channel: string;
  dict: { [sampleID: string]: DataPoint }
}
class DataPoint {
  xAxis: number[] = [];
  yAxis: number[] = [];
}
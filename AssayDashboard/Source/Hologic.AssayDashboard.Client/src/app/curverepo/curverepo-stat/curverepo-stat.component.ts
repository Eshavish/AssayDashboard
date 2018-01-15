/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
import { Component, OnInit } from '@angular/core';
import { ChartReadyEvent } from 'ng2-google-charts';
import { ChartErrorEvent } from 'ng2-google-charts';
import { ChartSelectEvent } from 'ng2-google-charts';
import { ChartMouseOverEvent, ChartMouseOutEvent } from 'ng2-google-charts';


import { CurverepoDatabaseService } from '../curverepo-services/curverepo-database.service'



@Component({
  selector: 'app-curverepo-stat',
  templateUrl: './curverepo-stat.component.html',
  styleUrls: ['./curverepo-stat.component.scss']
})

export class CurverepoStatComponent implements OnInit {
  AssayData: any;

  totalNumOfCurve: number;
  totalNumOfKnown: number;
  tableChartAssayData: any;
  pieChartKnownData: any;
  result: any;
  pieChartData: any;

  type: string;
  xAxis: string;
  yAxis: string;

  lineChartData: any;

  constructor(private databaseService: CurverepoDatabaseService) {
  }


  ngOnInit() {
    document.getElementById("stat").style.display = 'none';

    this.databaseService.getStatForKnown().subscribe(
      result => this.prepareDataForKnown(result)
    );

    this.databaseService.getAssay().subscribe(
      result => this.assayTable(JSON.parse(result))
    );
  }


  assayTable(data: any){
    console.log(data);
    this.AssayData = data;
   /* let chartData = new Array<Array<String>>();
    chartData.push(['Assay Name', 'Type ID', 'Number of File(s)']);

    for (let i = 0; i < data.length; i++){
      var row = [
        data[i].assay.AssayName, 
        data[i].assay.TypeId,
        data[i].fileCount
      ];
      chartData.push(row);
    }

    this.tableChartAssayData =  {
      chartType: 'Table',
      dataTable: chartData,
      options: {title: 'Assays', allowHtml: true}
    };*/

    document.getElementById("loader").style.display = 'none';
    document.getElementById("stat").style.display = 'block';
  }


  prepareDataForKnown(data: string){
    let chartData = new Array<Array<any>>();
    chartData.push(['Known', 'Number of File(s)'])

    let indexOfComma = data.indexOf(',', 0);

    this.totalNumOfCurve = +data.substr(0, indexOfComma);
    this.totalNumOfKnown = +data.substr(indexOfComma+1, data.length-1);
    this.result = data;

    // add data
    chartData.push(['Known', this.totalNumOfKnown]);
    chartData.push(['Unknown', this.totalNumOfCurve-this.totalNumOfKnown]);
    
    this.pieChartKnownData =  {
      chartType: 'PieChart',
      dataTable: chartData,
      options: {'title': 'Known Curves'},
    }; 
  }
}

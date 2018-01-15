import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { Ng2GoogleChartsModule } from 'ng2-google-charts';


import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CurverepoDatabaseService } from '../curverepo-services/curverepo-database.service';
import { CurverepoUploadComponent } from '../curverepo-upload/curverepo-upload.component';
import { CurverepoViewComponent } from '../curverepo-view/curverepo-view.component';
import { CurverepoStatComponent } from '../curverepo-stat/curverepo-stat.component';
import { DashboardComponent} from '../../dashboard/dashboard.component';
import { CurverepoGraphComponent } from '../curverepo-graph/curverepo-graph.component';

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    HttpModule,
    BrowserAnimationsModule,
    Ng2GoogleChartsModule,
    RouterModule.forRoot([ 
      { path: 'dashboard', component: DashboardComponent },
      { path: 'upload', component: CurverepoUploadComponent },
      { path: 'download', component: CurverepoViewComponent },
      { path: 'view', component: CurverepoGraphComponent },
      { path: 'stat', component: CurverepoStatComponent}
    ])
  ],
  exports: [
    CurverepoUploadComponent,
    CurverepoViewComponent,
    CurverepoStatComponent,
    CurverepoGraphComponent
  ],
  declarations: [
    CurverepoUploadComponent,
    CurverepoViewComponent,
    CurverepoStatComponent,
    CurverepoGraphComponent
  ],
  providers: [CurverepoDatabaseService]
})

export class CurverepoModule { }

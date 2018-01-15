import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RunDetailComponent } from "app/runexport/runexport-rundetails/rundetail.component";
import { ManagerComponent } from "app/runexport/runexport-manager/manager.component";
import { RunHomeComponent } from "app/runexport/runexport-home/home.component";
import { ToastrModule } from 'ngx-toastr';
import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';
import { HttpModule } from "@angular/http";
import { RunService } from "app/runexport/run.service";
import { DragdropComponent } from "app/runexport/runexport-dragdrop/dragdrop.component";

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    HttpModule,
    BrowserAnimationsModule, 
    LoadingModule,
    LoadingModule.forRoot({
      animationType: ANIMATION_TYPES.rectangleBounce,
      backdropBackgroundColour: 'rgba(0,0,0,0.1)', 
      backdropBorderRadius: '4px',
      primaryColour: '#ffffff', 
      secondaryColour: '#ffffff', 
      tertiaryColour: '#ffffff'
    }),
    ToastrModule.forRoot(),
    RouterModule.forRoot([ 
      { path: 'RunManager', component: ManagerComponent },
      { path: 'run/:id', component: RunDetailComponent },
      { path: 'home', component: RunHomeComponent },
    ]),
  ],
  exports: [
    RunHomeComponent,
    ManagerComponent,
    RunDetailComponent,
    DragdropComponent
  ],
  declarations: [
    RunHomeComponent,
    ManagerComponent,
    RunDetailComponent,
    DragdropComponent
  ],
  providers: [RunService] 
})
export class RunexportModule { }

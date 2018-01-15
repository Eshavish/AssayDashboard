import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AssayDetailComponent } from "app/assayviewrepo/assayviewrepo-assaydetails/assaydetail.component";
import { ManagerComponent } from "app/assayviewrepo/assayviewrepo-manager/manager.component";
import { HomeComponent } from "app/assayviewrepo/assayviewrepo-home/home.component";
import { ToastrModule } from 'ngx-toastr';
import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';
import { HttpModule } from "@angular/http";
import { ParserService } from "app/assayviewrepo/parser.service";
import { DragdropComponent } from "app/assayviewrepo/assayviewrepo-dragdrop/dragdrop.component";

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
      { path: 'manager', component: ManagerComponent },
      { path: 'assay/:id', component: AssayDetailComponent },
      { path: 'home', component: HomeComponent },
    ]),
  ],
  exports: [
    HomeComponent,
    ManagerComponent,
    AssayDetailComponent,
    DragdropComponent
  ],
  declarations: [
    HomeComponent,
    ManagerComponent,
    AssayDetailComponent,
    DragdropComponent
  ],
  providers: [ParserService] 
})
export class AssayViewerModule { }

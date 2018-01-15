import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Route } from '@angular/router';
import { AppComponent } from './app.component';
import { CurverepoModule } from './curverepo/curverepoModule/curverepo.module';
import { RunexportModule} from './runexport/runexportModule/runexport.module';
import { CurverepoUploadComponent } from './curverepo/curverepo-upload/curverepo-upload.component';
import { AssayViewerModule } from "app/assayviewrepo/assayviewrepoModule/assayviewrepo.module";
import { DashboardComponent } from "app/dashboard/dashboard.component";
import { AboutComponent } from "app/about/about.component";
import { HomeComponent } from "app/assayviewrepo/assayviewrepo-home/home.component";
import { RunHomeComponent } from "app/runexport/runexport-home/home.component";
import { ReportModule } from "./reportrepo/reportrepoModule/reportrepo-app.module";
import { UploadFiles } from "./reportrepo/reportrepo-upload/reportrepo-upload.component";
import { DowloadFiles } from "app/reportrepo/reportrepo-download/reportrepo-download.component";
import { FrontPage } from "app/reportrepo/reportrepo-frontpage/reportrepo-frontpage.component";
import { CurverepoStorageService } from './curverepo/curverepo-services/curverepo-storage.service';
import { ToastModule } from 'ng2-toastr/ng2-toastr';
//import {ReactiveFormsModule} from '@angular/forms';
import { HttpModule } from '@angular/http';



@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    AboutComponent
    //ReactiveFormsModule
  ],
  imports: [
    BrowserModule,
    HttpModule,
    ReportModule,
    FormsModule,
    AssayViewerModule,
    CurverepoModule,
    RunexportModule,
    ToastModule.forRoot(),
    RouterModule.forRoot([
      { path: 'dashboard', component: DashboardComponent },
      { path: 'about', component: AboutComponent},
      {
        path: 'reportrepo-frontpage', component: FrontPage
        , children: [
          { path: '', component: UploadFiles },
          { path: 'upload', component: UploadFiles},
          { path: 'download', component: DowloadFiles}
        ]
      },
      { path: 'curverepo-upload', component: CurverepoUploadComponent },
      { path: 'runexport-home', component: RunHomeComponent},
      { path: 'assayview-home', component: HomeComponent },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
    ])
  ],
  providers: [CurverepoStorageService],
  bootstrap: [AppComponent]
})
export class AppModule { }

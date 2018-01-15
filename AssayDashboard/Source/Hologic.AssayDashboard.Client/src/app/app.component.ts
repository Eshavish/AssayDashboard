import { Component} from '@angular/core';
import { CurverepoStorageService } from './curverepo/curverepo-services/curverepo-storage.service';
import { Router, NavigationEnd } from '@angular/router';
 
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent{
  title = "";
  
  constructor(private router: Router){  
    router.events.subscribe(event =>{
      if (event instanceof NavigationEnd) {
        this.setTitle(event.url);
      }
    })
  }

  setTitle (input: string){
    if (input.includes("dashboard")){
      this.title = "";
    }else if (input.includes("curverepo") || input == "/download" || input == "/view"|| input == "/stat" || input =="/upload"){
      this.title = "Curve Repository";
    }else if (input.includes("reportrepo")){
      this.title = "Report Repository";
    }else if (input.includes("assayview") || input.includes("manager") || input.includes("assay") || (input.includes("home") && !input.includes("runexport-home"))){
      this.title = "Assay Viewer";
    }else if (input.includes("runexport") || input.includes("RunManager") || input.includes("run")){
      this.title = "Run Export Viewer";
    }else if (input.includes("about")){
      this.title = "About";
    }else{
      this.title = "";
    }
  }
  
}

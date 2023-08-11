import { Component, EventEmitter, Output } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Blog';



  constructor(public translate:TranslateService) {

    translate.setDefaultLang('sr');
    translate.use('sr');
  }


  onLanguageSelected(e){
    if(e=='sr'){
    }
    if(e=='us'){
    }
  }

  
}

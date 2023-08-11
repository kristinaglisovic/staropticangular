import { HttpParams } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { PostComponent } from 'src/app/home/home/post/post.component';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';
@Component({
  selector: 'app-sticky-lang-change',
  templateUrl: './sticky-lang-change.component.html',
  styleUrls: ['./sticky-lang-change.component.css']
})
export class StickyLangChangeComponent implements OnInit {

  constructor(private translate:TranslateService,private tE:TranslateToEngService) { }




  switchTo(l:string){
    if(l=="sr"){
      localStorage.setItem('selectedLanguage', l);
      this.tE.TranslateToEng(false);
    }
    if(l=='us'){
      localStorage.setItem('selectedLanguage', l);
      this.tE.TranslateToEng(true);
    }
    // this.translateClicked.emit();
    this.translate.use(l);
  }


  ngOnInit(): void {
    localStorage.setItem('selectedLanguage', 'sr');
  }

}

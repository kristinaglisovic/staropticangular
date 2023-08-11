import { HttpParams } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Tag } from 'src/app/interfaces/Tag/ITag';
import { TagService } from 'src/app/services/Tag/tag.service';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';

@Component({
  selector: 'app-tags',
  templateUrl: './tags.component.html',
  styleUrls: ['./tags.component.css']
})
export class TagsComponent implements OnInit {

  constructor(private ts:TagService,private trs:TranslateToEngService) { }

  tags:Tag[]=[];

translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;

  


  getTags(){
    this.ts.getPostTags(this.translate)
      .subscribe((response:any)=>{
        console.log(response)
        this.tags=response.data;
      }
      );
  } 


  

  translateChng(){
    this.trs.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
      this.getTags()
    })
  }


  onlyActive(items: any[]): any[] {
    return items.filter(p => p.isActive==true && p.activePostsCount>0);
  }

  tsel: Array<any> = [];
  @Output() tagsSelected=new EventEmitter<any[]>();

  
  changeTags(event){
    var index = this.tsel.indexOf(event.source.value);
      if(index === -1){
        this.tsel.push(event.source.value);
      }else{
        this.tsel.splice(index,1);
      }
     this.tagsSelected.emit(this.tsel)
  }


  ngOnInit(): void {
    this.translateChng();
    this.getTags()

  }
}

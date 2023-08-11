import { HttpParams } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Category } from 'src/app/interfaces/Category/ICategory';
import { CategoryService } from 'src/app/services/Category/category.service';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {

  constructor(private cs:CategoryService, private ts:TranslateToEngService) { }

  cas:Category[]=[];


  translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;


  translateChng(){
    this.ts.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
      this.getCas()
    })
  }


  csel:Array<any>=[];
  @Output() catSelected=new EventEmitter<any[]>();

  changeCategories(event){
    var index = this.csel.indexOf(event.source.value);
      if(index === -1){
        this.csel.push(event.source.value);
      }else{
        this.csel.splice(index,1);
      }
     this.catSelected.emit(this.csel)
  }

  onlyActive(items: any[]): any[] {
    return items.filter(p => p.isActive==true && p.postsCount>0);
  }

  getCas(){
    this.cs.getAllPostCategories(this.translate)
      .subscribe((response:any)=>{
        // console.log(response)
        this.cas=response.data;
      }
      );
  } 
  ngOnInit(): void {
    this.translateChng()
    this.getCas()
  }
}

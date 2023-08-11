import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Tag } from 'src/app/interfaces/Tag/ITag';
import { TagService } from 'src/app/services/Tag/tag.service';
import { MatDialog } from '@angular/material/dialog';
import { AddTagComponent } from './add-tag/add-tag.component';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';
import { HttpParams } from '@angular/common/http';
import { AdminViewTagComponent } from '../admin-view-tag/admin-view-tag.component';

@Component({
  selector: 'app-admin-tags',
  templateUrl: './admin-tags.component.html',
  styleUrls: ['./admin-tags.component.css']
})
export class AdminTagsComponent implements OnInit {
  constructor(private tagsService:TagService,private dialogRef:MatDialog,private ts:TranslateToEngService) { }

  openDialog(dialog:string,data?:any){
    if(dialog=='add'){
      this.dialogRef.open(AddTagComponent).afterClosed().subscribe(d=>{
        console.log(d)
        if(d){
         this.getTags()
        }
      })
    }
    if(dialog=='editTag' && data){
    
      this.dialogRef.open(AdminViewTagComponent,{
        width      : '100%',
        maxWidth   : '400px',
        height     : 'auto',
        hasBackdrop: true,
        maxHeight  : '700px',
        data:data
      }).afterClosed().subscribe(d=>{
        if(d){
          this.getTags()
        }
      });
    }
   
  }


  tags:Tag[]=[];
  /*pagination*/
  currentPage:number=0;
  total: number = 0;
  itemsPerPage:number=10;
  totalPages:number;
  dataSource:any;
  translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;

  params=new HttpParams();
  

  translateChng(){
    this.ts.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
      this.getTags()
    })
  }


  getTags(){
    this.tagsService.getAllTags(this.currentPage+1,this.itemsPerPage,this.params,this.translate)
      .subscribe((response:any)=>{
        console.log(response)
        this.tags=response.data;
        this.dataSource=new MatTableDataSource(this.tags)
        this.total = response.totalCount;
        this.itemsPerPage=response.itemsPerPage
      }
      );
  }

  displayedColumns:string[]=['id','name','postsCount','activePostsCount','active','createdAt','updatedAt','delete','activate','edit']
  pageSizeOptions: number[] = [10, 25, 100];
 


  ngOnInit(): void {
    this.translateChng()
    this.getTags();
  }


  pageChangeEvent(event: PageEvent){
    this.currentPage = event.pageIndex;
    this.itemsPerPage = event.pageSize;
    this.getTags()
  }


  deleteTag(id){

  this.tagsService.deleteTag(id).subscribe((resp=>{

    this.getTags()
  }))
}


changeStatus(id:number):void{
  this.tagsService.changeStatus(id).subscribe(resp=>{
    this.getTags()
   }
    );
}

 
}

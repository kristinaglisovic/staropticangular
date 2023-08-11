import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Category } from 'src/app/interfaces/Category/ICategory';
import { CategoryService } from 'src/app/services/Category/category.service';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';
import { AdminViewCategoryComponent } from '../admin-view-category/admin-view-category.component';
import { AddCategoryComponent } from './add-category/add-category.component';

@Component({
  selector: 'app-admin-categories',
  templateUrl: './admin-categories.component.html',
  styleUrls: ['./admin-categories.component.css']
})
export class AdminCategoriesComponent implements OnInit {

  constructor(private CategoryService:CategoryService,private ts:TranslateToEngService,private dialogRef:MatDialog,) { }


  openDialog(dialog:string,data?:any){
    if(dialog=='add'){
      this.dialogRef.open(AddCategoryComponent).afterClosed().subscribe(d=>{
        console.log(d)
        if(d){
         this.getCategories()
        }
      })
    }
    if(dialog=='edit' && data){
      this.dialogRef.open(AdminViewCategoryComponent,{
        width      : '100%',
        maxWidth   : '400px',
        height     : 'auto',
        hasBackdrop: true,
        maxHeight  : '700px',
        data:data
      }).afterClosed().subscribe(d=>{
        if(d){
          this.getCategories()
        }
      });
    }
   
  }

  categories:Category[]=[];
    /*pagination*/
  currentPage:number=0;
  total: number = 0;
  itemsPerPage:number=5;
  totalPages:number;
  dataSource:any;


  translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;

  
  translateChng(){
    this.ts.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
      this.getCategories()
    })
  }


  getCategories(){
    this.CategoryService.getAllCategories(this.currentPage+1,this.itemsPerPage,this.translate)
      .subscribe((response:any)=>{
        // console.log(response)
        this.categories=response.data;
        this.dataSource=new MatTableDataSource(this.categories)
        this.total = response.totalCount;
        this.itemsPerPage=response.itemsPerPage
      }
      );
  }

  
  displayedColumns:string[]=['id','name','description','postsCount','activePostsCount','active','createdAt','updatedAt','delete','activate','edit']
  pageSizeOptions: number[] = [5, 10, 25, 100];


  ngOnInit(): void {
    this.translateChng()
    this.getCategories();
  }


  pageChangeEvent(event: PageEvent){
    this.currentPage = event.pageIndex;
    this.itemsPerPage = event.pageSize;
    this.getCategories()
  }

  changeStatus(id:number):void{
    this.CategoryService.changeStatus(id).subscribe(resp=>{
    this.getCategories()
     }
    );
  }

  deleteCategory(id){
    //console.log(id)

    this.CategoryService.deleteCategory(id).subscribe((resp=>{

    this.getCategories()
  }))
  }
  
}

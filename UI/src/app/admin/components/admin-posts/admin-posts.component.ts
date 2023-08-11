import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Post } from 'src/app/interfaces/Post/IPost';
import { PostService } from 'src/app/services/Post/post.service';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';
import { AdminViewPostComponent } from '../admin-view-post/admin-view-post.component';
import { AddPostComponent } from './add-post/add-post.component';
@Component({
  selector: 'app-admin-posts',
  templateUrl: './admin-posts.component.html',
  styleUrls: ['./admin-posts.component.css']
})
export class AdminPostsComponent implements OnInit {

  constructor(private PostService:PostService,private ts:TranslateToEngService,private dialogRef:MatDialog) { }


  posts:Post[]=[];


  
  currentPage:number=0;
  total: number = 0;
  itemsPerPage:number=4;
  totalPages:number;
  dataSource:any;

  translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;


  displayedColumns:string[]=['id','title','description','author','images','likes','comments','active','createdAt','updatedAt','look','delete','activate','edit']
  pageSizeOptions: number[] = [4,5, 10, 25, 100];


  openDialog(dialog:string,data?:any){
    if(dialog=='add'){
      this.dialogRef.open(AddPostComponent,{
        width      : '100%',
        maxWidth   : '400px',
        height     : 'auto',
        hasBackdrop: true,
        maxHeight  : '700px',
        data:data
      }).afterClosed().subscribe(d=>{
        console.log(d)
        if(d){
         this.getPosts()
        }
      })
    }
    if(dialog=='edit' && data){
      this.dialogRef.open(AdminViewPostComponent,{
        width      : '100%',
        maxWidth   : '400px',
        height     : 'auto',
        hasBackdrop: true,
        maxHeight  : '700px',
        data:data
      }).afterClosed().subscribe(d=>{
        if(d){
          this.getPosts()
        }
      });
    }
   
  }







  getPosts(){
   this.PostService.getAllAdminPosts(this.currentPage+1,this.itemsPerPage,this.translate)
      .subscribe((response:any)=>{
       console.log(response.data)
        this.posts=response.data;
        this.dataSource=new MatTableDataSource(this.posts)
        this.total = response.totalCount;
        this.itemsPerPage=response.itemsPerPage
        
        
      }
      
      );
  }

  ngOnInit(): void {

    this.translateChng()
    this.getPosts();
  }



  deletePost(id:number):void{
    this.PostService.deletePost(id).subscribe(resp=>{
      this.getPosts()
     }
      );
  }


  changeStatus(id:number):void{
    this.PostService.changeStatus(id).subscribe(resp=>{
      this.getPosts()
     }
      );
  }

  pageChangeEvent(event: PageEvent){
    this.currentPage = event.pageIndex;
    this.itemsPerPage = event.pageSize;
    this.getPosts()
  }


  translateChng(){
    this.ts.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
      this.getPosts()
    })
  }



}

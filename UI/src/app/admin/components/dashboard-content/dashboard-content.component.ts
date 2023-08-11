import {MatPaginator, PageEvent} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { CategoryService } from 'src/app/services/Category/category.service';
import { CommentService } from 'src/app/services/Comment/comment.service';
import { ImagesService } from 'src/app/services/Images/images.service';
import { PostService } from 'src/app/services/Post/post.service';
import { TagService } from 'src/app/services/Tag/tag.service';
import { UserService } from 'src/app/services/User/user.service';
import { IImage } from 'src/app/interfaces/Image/IImage';
import { DomSanitizer } from '@angular/platform-browser';
import { MatDialog } from '@angular/material/dialog';
import { AddImageComponent } from './add-image/add-image.component';

@Component({
  selector: 'app-dashboard-content',
  templateUrl: './dashboard-content.component.html',
  styleUrls: ['./dashboard-content.component.css']
})
export class DashboardContentComponent implements OnInit {

  constructor(private us:UserService,private comS:CommentService,private ts:TagService,private categS:CategoryService,private ps:PostService,private is:ImagesService, private dialogRef:MatDialog) { }

  images:any;

  /*pagination*/
  currentPage:number=0;
  total: number = 0;
  itemsPerPage:number=4;
  totalPages:number;
  dataSource:any;

  users:number;
  comments:number;
  tags:number;
  categories:number;
  posts:number;


  openDialog(){
    this.dialogRef.open(AddImageComponent).afterClosed().subscribe(d=>{
        if(d){
         this.getImages()
        }
    })
  }




  
  getImages(){
    this.is.getAllImages(this.currentPage+1,this.itemsPerPage)
      .subscribe((response:any)=>{
        this.images=response.data;
        this.dataSource=new MatTableDataSource(this.images)
        this.total = response.totalCount;
        this.itemsPerPage=response.itemsPerPage
      }
      );
  }

  getUsers(){
    this.us.getAllActiveUsers().subscribe((e:any)=>{
      this.users=e.totalCount;
    })
  }

  getPosts(){
    this.ps.getAllActivePosts().subscribe((e:any)=>{
      this.posts=e.totalCount;
    })
  }

  getComments(){
    this.comS.getAllActiveComments().subscribe((e:any)=>{
      this.comments=e.totalCount;
    })
  }
  getCategories(){
    this.categS.getAllActiveCategories().subscribe((e:any)=>{
      this.categories=e.totalCount;
    })
  }

  getTags(){
    this.ts.getAllActiveTags().subscribe((e:any)=>{
      this.tags=e.totalCount;
    })
  }


 displayedColumns:string[]=['id','path','user','posts','createdAt','delete']
 pageSizeOptions: number[] = [4,5, 10, 25, 100];



  user:any=JSON.parse(localStorage.getItem('user'));

  ngOnInit(): void {
    this.getComments()
    this.getPosts()
    this.getTags()
    this.getCategories()
    this.getImages()
    this.getUsers()
  }



  pageChangeEvent(event: PageEvent){
    this.currentPage = event.pageIndex;
    this.itemsPerPage = event.pageSize;
    this.getImages()
  }


  deletePhoto(id){
    //console.log(id)

  this.is.deleteImage(id).subscribe((resp=>{

    this.getImages()
  }))
       
}


  
}

import { Component, OnInit, ViewChild } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { IComment } from 'src/app/interfaces/Comment/IComment';
import { IUpdateCommentRequest } from 'src/app/interfaces/Comment/IUpdateCommentRequest';
import { CommentService } from 'src/app/services/Comment/comment.service';

@Component({
  selector: 'app-admin-comments',
  templateUrl: './admin-comments.component.html',
  styleUrls: ['./admin-comments.component.css']
})
export class AdminCommentsComponent implements OnInit {

  constructor(private CommService:CommentService) { }


  comments:IComment[]=[];


  currentPage:number=0;
  total: number = 0;
  itemsPerPage:number=10;
  totalPages:number;
  dataSource:any;

  displayedColumns:string[]=['id','author','comment','look','active','createdAt','updatedAt','delete','activate']
  pageSizeOptions: number[] = [10, 25, 100];
 

  getComments(){
    this.CommService.getAllComments(this.currentPage+1,this.itemsPerPage)
    .subscribe((response:any)=>{
      console.log(response)
      this.comments=response.data;

      this.dataSource=new MatTableDataSource(this.comments)
      this.total = response.totalCount;
        this.itemsPerPage=response.itemsPerPage
      }
      );
  }


 

  ngOnInit(): void {
    this.getComments();
  }


  deleteComment(id:number):void{
    this.CommService.deleteComment(id).subscribe(resp=>{
        this.getComments();
     }
   );
  }
  
 changeStatus(id:number):void{
        this.CommService.changeStatus(id).subscribe(resp=>{
          this.getComments();
        }
      );
  }


 pageChangeEvent(event: PageEvent){
    this.currentPage = event.pageIndex;
    this.itemsPerPage = event.pageSize;
    this.getComments()
  }
}

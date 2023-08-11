import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { User } from 'src/app/interfaces/User/IUser';
import { AuthService } from 'src/app/services/Auth/auth.service';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';

import { UserService } from 'src/app/services/User/user.service';
import { AdminViewUserComponent } from '../admin-view-user/admin-view-user.component';
import { AddUserComponent } from './add-user/add-user.component';

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html',
  styleUrls: ['./admin-users.component.css']
})
export class AdminUsersComponent implements OnInit {

  constructor(private UserService:UserService,public auth:AuthService,private ts:TranslateToEngService,private dialogRef:MatDialog) { }

  translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;


  openDialog(dialog:string,data?:any){
    if(dialog=='add'){
      this.dialogRef.open(AddUserComponent,{
        width      : '100%',
        maxWidth   : '400px',
        height     : 'auto',
        hasBackdrop: true,
        maxHeight  : '700px',
        data:data
      }).afterClosed().subscribe(d=>{
        console.log(d)
        if(d){
         this.getUsers()
        }
      })
    }
    if(dialog=='edit' && data){
      this.dialogRef.open(AdminViewUserComponent,{
        width      : '100%',
        maxWidth   : '400px',
        height     : 'auto',
        hasBackdrop: true,
        maxHeight  : '700px',
        data:data
      }).afterClosed().subscribe(d=>{
        if(d){
          this.getUsers()
        }
      });
    }
   
  }



  users:User[]=[];

  currentPage:number=0;
  total: number = 0;
  itemsPerPage:number=4;
  totalPages:number;
  dataSource:any;

  displayedColumns:string[]=['id','fname','lname','username','picture', 'email','role','postsCount','activePostsCount','likes','comments','active','registered','updatedAt','delete','activate','edit']
  pageSizeOptions: number[] = [4,5, 10, 25, 100];

 
  

  deleteUser(id){
    this.UserService.deleteUser(id).subscribe(resp=>{
      this.getUsers()
    }
   );
  }

  getUsers(){
    this.UserService.getAllUsers(this.currentPage+1,this.itemsPerPage)
      .subscribe((response:any)=>{
        console.log(response)
        this.users=response.data;
        this.dataSource=new MatTableDataSource(this.users)
        this.total = response.totalCount;
        this.itemsPerPage=response.itemsPerPage
      }
      );
  }
  

  deactivateUserAndItsPosts(id){
    this.UserService.deactivateUserAndItsPosts(id).subscribe(resp=>{
      this.getUsers()
    }
   );
  }

   translateChng(){
    this.ts.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
    })
  }


  ngOnInit(): void {
    this.translateChng()
    this.getUsers();
  }



  changeStatus(id:number):void{
    this.UserService.changeStatus(id).subscribe(resp=>{
      this.getUsers()
     }
      );
  }


 pageChangeEvent(event: PageEvent){
    this.currentPage = event.pageIndex;
    this.itemsPerPage = event.pageSize;
    this.getUsers()
  }
}

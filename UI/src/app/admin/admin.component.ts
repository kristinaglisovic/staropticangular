import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  constructor(private router:Router) { }
  user:any=JSON.parse(localStorage.getItem('user'));
  navigate(){
    this.router.navigate(['/home'])
    .then(() => {
      window.location.reload();
    });
  }
  
  checkUser(){
   if(this.user==null || this.user.role!="Administrator"){
     this.navigate()
    }
 }
 
 ngOnInit(): void {
    this.checkUser()
  }

 

}

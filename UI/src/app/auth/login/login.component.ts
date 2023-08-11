import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/Auth/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserService } from 'src/app/services/User/user.service';
import { User } from 'src/app/interfaces/User/IUser';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  constructor(private auth:AuthService,private router:Router,private us:UserService,private ts:TranslateToEngService) { }
  loginForm: FormGroup;
  user:User;
  eng:boolean=false;

  navigate(){
    this.router.navigate(['home'])
  }

  checkUser(){
    if(JSON.parse(localStorage.getItem('user'))){
       this.navigate()
    }
  }
  
  ngOnInit(): void {
    this.translateChng()
    this.checkUser()
    this.loginForm=new FormGroup({
      email:new FormControl("", [Validators.required,Validators.email]),
      password:new FormControl("",Validators.required)
   });
  }

  get f(){return this.loginForm.controls;}


  error:string=''
  login(){
    if(this.loginForm.valid){
      this.auth.login(this.loginForm.value).subscribe(result=>{

        localStorage.setItem('token',result.token) 
        
        const helper = new JwtHelperService();
        const decodedToken = helper.decodeToken(result.token);
        
        // console.log(decodedToken)
        const userId=decodedToken.UserId;
        
         this.us.getOneUserById(userId).subscribe(result=>{
           localStorage.setItem('user',JSON.stringify(result))
           this.auth.user = result;
         },

         )
       
        this.navigate()
      },
      (error)=>{
     //   console.log(error)
        if(this.error && this.eng){
          this.error="There is no user with the entered email and password."
        }
        else{
          this.error=error;
        }
     })
    }
  }
  translateChng(){
    this.ts.TranslateTo().subscribe((d=>{
      this.eng=d;
    }))
  }


}

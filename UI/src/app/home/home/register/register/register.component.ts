import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';
import { UserService } from 'src/app/services/User/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  constructor(private userService:UserService,private router:Router,private ts:TranslateToEngService) { }

  showPassword: boolean = false;
  resp:string='';
  resp2:string='';
  backMsgEmail:string='';
  backMsgUsername:string='';
  addUser:FormGroup;
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
    this.addUser=new FormGroup({
      FirstName:new FormControl("", [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(40),
        Validators.pattern('[A-ZŠĐČĆŽa-zšđčćž]+((\s)?((\'|\-|\.)?([A-ZŠĐČĆŽa-zšđčćž])+))*')]),
        LastName:new FormControl("", [Validators.required,Validators.minLength(3),Validators.maxLength(40),Validators.pattern('^[A-ZŠĐČĆŽa-zšđčćž]+((\s)?((\'|\-|\.)?([A-ZŠĐČĆŽa-zšđčćž])+))*$')]),
        Username:new FormControl("", [Validators.required,Validators.pattern('(?=.{4,30}$)(?![_.])(?!.*[_.]{2})[a-zšđčćžA-ZŠĐČĆŽ0-9._]+(?<![_.])')]),
        Email:new FormControl("", [Validators.required,Validators.email]),
        Password:new FormControl("", [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(15),
          Validators.pattern('(?=.*[a-zšđčćž])(?=.*[A-ZŠĐČĆŽ])(?=.*\\d)(?=.*[@$!%*?&])[A-ZŠĐČĆŽa-zšđčžć\\d@$!%*?&]{8,15}')]),
          // Validators.pattern('^(?=.*[a-zšđčćž])(?=.*[A-ZŠĐČĆŽ])(?=.*\d)(?=.*[@$!%*?&])[A-ZŠĐČĆŽa-zšđčžć\d@$!%*?&]{8,15}$')]),
        Image:new FormControl(''),
        fileSource: new FormControl('')
    });
   
  }
  get f(){return this.addUser.controls;}



  showHidePassword() {
    this.showPassword = !this.showPassword;
  }

  onFileChange(event:any) {
  
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.addUser.patchValue({
        fileSource: file
      });
    }
  } 

  

  onSubmit():void{
    var formData: any = new FormData();
    
    formData.append('FirstName', this.addUser.get('FirstName').value);
    formData.append('LastName', this.addUser.get('LastName').value);
    formData.append('Username', this.addUser.get('Username').value);
    formData.append('Email', this.addUser.get('Email').value);
    formData.append('Password', this.addUser.get('Password').value);
    formData.append('RoleId', 2);
    formData.append('Image', this.addUser.get('fileSource')?.value);

   
    this.userService.addUser(formData).subscribe
    (resp=>{
      this.backMsgEmail='';
      this.backMsgUsername='';
      if(this.eng){
        this.resp="You have successfully registered";
      }
      else{
        this.resp='Uspešno ste se registrovali'
      }
    
      setTimeout(() => {
        this.router.navigate(['home/login'])
      }, 2000);
     
    },
    (error)=>{
      // console.log(error)
      if(error.status==422){
        this.addUser.setErrors({ 'invalid': true });
        if(error.error.errors.find(o=>o.property=="Username") && !this.eng){
              this.backMsgUsername=error.error.errors.find(o=>o.property=="Username").error;
        }
        else if(error.error.errors.find(o=>o.property=="Username") && this.eng){
          this.backMsgUsername="Username is taken."
        }
        else{
          this.backMsgUsername=''
        }
        if(error.error.errors.find(o=>o.property=="Email") && !this.eng){
          this.backMsgEmail=error.error.errors.find(o=>o.property=="Email").error;
        }
        else if(error.error.errors.find(o=>o.property=="Email") && this.eng){
          this.backMsgEmail="Email is taken."
        }
        else{
          this.backMsgEmail=''
        }
        
      }
     
      if(error.status==500){
        if(error && this.eng){
          this.resp2="The allowed file extensions are: .jpg, .png, .jpeg, .gif, .JPG, .PNG, .JPEG, .GIF";
        }
        else{
          this.resp2="Dozvoljene ekstenzije su: .jpg, .png, .jpeg, .gif,.JPG, .PNG,.JPEG, .GIF";
        }
       }
    });
    
  }
  translateChng(){
    this.ts.TranslateTo().subscribe((d=>{
      this.eng=d;
    }))
  }

}

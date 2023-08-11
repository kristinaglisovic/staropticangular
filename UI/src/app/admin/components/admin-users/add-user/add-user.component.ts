import { Component, OnInit } from '@angular/core';

import { UserService } from 'src/app/services/User/user.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { MatDialogRef } from '@angular/material/dialog';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';


@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {

  constructor(private userService:UserService,
  private ts:TranslateToEngService,private dialogRef: MatDialogRef<AddUserComponent>) { }

  translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;
  addUser:FormGroup;

  hide = true;


  errorMsg:string;
  resp:string;
  roleId:number=1;
  empty:boolean=false;

  
  onFileChange(event:any) {
   
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      if(file.type==""){
        this.empty=true;
      }
      else{
        this.empty=false;
      }
      this.addUser.patchValue({
        fileSource: file,
        imageType:file.type,
      });
    }
  } 




  ngOnInit(): void {
    this.translateChng();
    this.addUser=new FormGroup({
      FirstName:new FormControl("", [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(40),
        Validators.pattern('[A-ZŠĐČĆŽa-zšđčćž]+((\s)?((\'|\-|\.)?([A-ZŠĐČĆŽa-zšđčćž])+))*')]),

        LastName:new FormControl("", [Validators.required,Validators.minLength(3),Validators.maxLength(40),Validators.pattern('^[A-ZŠĐČĆŽa-zšđčćž]+((\s)?((\'|\-|\.)?([A-ZŠĐČĆŽa-zšđčćž])+))*$')]),

        Username:new FormControl("", [Validators.required,Validators.pattern('(?=.{4,30}$)(?![_.])(?!.*[_.]{2})[a-zšđčćžA-ZŠĐČĆŽ0-9._]+(?<![_.])')]),

        Email:new FormControl("", [Validators.required,Validators.email]),
        fileSource: new FormControl(''),
        Password:new FormControl("", [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(15),
          Validators.pattern('(?=.*[a-zšđčćž])(?=.*[A-ZŠĐČĆŽ])(?=.*\\d)(?=.*[@$!%*?&])[A-ZŠĐČĆŽa-zšđčžć\\d@$!%*?&]{8,15}')]),
        //.jpg, .png, .jpeg, .gif, .JPG, .PNG, .JPEG, .GIF.
        imageType: new FormControl('',[Validators.pattern('(image\/(jpe?g|JPE?G|png|PNG|gif|GIF))')])
    });
  }

  onSubmit():void{
    var formData: any = new FormData();

 
    var formData: any = new FormData();
    
    formData.append('FirstName', this.addUser.get('FirstName').value);
    formData.append('LastName', this.addUser.get('LastName').value);
    formData.append('Username', this.addUser.get('Username').value);
    formData.append('Email', this.addUser.get('Email').value);
    formData.append('Password', this.addUser.get('Password').value);
    formData.append('RoleId', this.roleId);
    formData.append('Image', this.addUser.get('fileSource')?.value);


    this.userService.addUser(formData).subscribe({
      next: data => {
        this.errorMsg='';
        if(!this.translate){
          this.resp='Korisnik je uspešno dodat'
        }
        else{
          this.resp='User successfully added'
        }
        setTimeout(() => {
          this.dialogRef.close(true);
        }, 2000);
      },
      error: err => {
        console.log(err)
        if(!this.translate){
          this.errorMsg=err.error.errors[0].error;
        }
        else{
          if(err.error.errors[0].property=='Email'){
            this.errorMsg='Email is already registered!';
          }
          if(err.error.errors[0].property=='Username'){
            this.errorMsg='Username already exists!'
          }
        }
      }
    })
  }




  translateChng(){
    this.ts.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
    })
  }

  setRole(id){
    // console.log(id)
    this.roleId=id;
  }

}

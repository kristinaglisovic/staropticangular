import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { User } from 'src/app/interfaces/User/IUser';
import { ImagesService } from 'src/app/services/Images/images.service';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';
import { UserService } from 'src/app/services/User/user.service';

@Component({
  selector: 'app-admin-view-user',
  templateUrl: './admin-view-user.component.html',
  styleUrls: ['./admin-view-user.component.css']
})
export class AdminViewUserComponent implements OnInit {

  constructor(private userService:UserService, @Inject(MAT_DIALOG_DATA) public userdata: User,
  private ts:TranslateToEngService,private dialogRef: MatDialogRef<AdminViewUserComponent>,private is:ImagesService) { }
  


  translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;
  editUser:FormGroup;

  errorMsg:string;
  resp:string;
  roleId:number;
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
      this.editUser.patchValue({
        fileSource: file,
        imageType:file.type,
      });
    }
  } 




  ngOnInit(): void {
    this.translateChng();
    this.editUser=new FormGroup({
      FirstName:new FormControl(this.userdata.firstName, [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(40),
        Validators.pattern('[A-ZŠĐČĆŽa-zšđčćž]+((\s)?((\'|\-|\.)?([A-ZŠĐČĆŽa-zšđčćž])+))*')]),

        LastName:new FormControl(this.userdata.lastName, [Validators.required,Validators.minLength(3),Validators.maxLength(40),Validators.pattern('^[A-ZŠĐČĆŽa-zšđčćž]+((\s)?((\'|\-|\.)?([A-ZŠĐČĆŽa-zšđčćž])+))*$')]),

        Username:new FormControl(this.userdata.username, [Validators.required,Validators.pattern('(?=.{4,30}$)(?![_.])(?!.*[_.]{2})[a-zšđčćžA-ZŠĐČĆŽ0-9._]+(?<![_.])')]),

        Email:new FormControl(this.userdata.email, [Validators.required,Validators.email]),
        fileSource: new FormControl(''),
        
        //.jpg, .png, .jpeg, .gif, .JPG, .PNG, .JPEG, .GIF.
        imageType: new FormControl('',[Validators.pattern('(image\/(jpe?g|JPE?G|png|PNG|gif|GIF))')])
    });
  }

  onSubmit():void{
    var formData: any = new FormData();

 
    formData.append('UserId', this.userdata.id);
    formData.append('FirstName', this.editUser.get('FirstName').value);
    formData.append('LastName', this.editUser.get('LastName').value);
    formData.append('Username', this.editUser.get('Username').value);
    formData.append('Email', this.editUser.get('Email').value);
    formData.append('RoleId', this.userdata.roleId);
    formData.append('Image', this.editUser.get('fileSource')?.value);


    this.userService.updateUser(formData).subscribe({
      next: data => {
        this.errorMsg='';
        if(!this.translate){
          this.resp='Korisnik je uspešno ažuriran'
        }
        else{
          this.resp='User successfully edited'
        }
        setTimeout(() => {
          this.dialogRef.close(true);
        }, 2000);
      },
      error: err => {
        console.log(err)
        if(!this.translate){
          this.errorMsg=err.error.message;
        }
        else{
          if(err.error.property=='email'){

            this.errorMsg='Email already exists!';
          }
          if(err.error.property=='username'){
            this.errorMsg='Username already exists!'
          }
        }
      }
    })
  }

  deletePhoto(id){
    //console.log(id)

      this.is.deleteImage(id).subscribe((resp=>{
        this.userdata.image='No image'
        this.ngOnInit()
      }))
  }


  translateChng(){
    this.ts.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
    })
  }

  setRole(id){
    // console.log(id)
    this.userdata.roleId=id;
  }


}

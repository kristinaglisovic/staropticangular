import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { IAddTagRequest } from 'src/app/interfaces/Tag/IAddTagRequest';
import { TagService } from 'src/app/services/Tag/tag.service';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';

@Component({
  selector: 'app-add-tag',
  templateUrl: './add-tag.component.html',
  styleUrls: ['./add-tag.component.css']
})
export class AddTagComponent implements OnInit {

  addTag:FormGroup;
  constructor(private tagService:TagService, private dialogRef: MatDialogRef<AddTagComponent>,private ts:TranslateToEngService) { }



  translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;

  errorMsg:string;
  resp:string;




  translateChng(){
    this.ts.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
    })
  }


  onSubmit():void{
    
    const addtagRequest:IAddTagRequest={
       name:this.addTag.value.nameSr,
       nameUs:this.addTag.value.nameUs
    }

    

    this.tagService.addTag(addtagRequest).subscribe({
      next: data => {
        this.errorMsg='';
        if(!this.translate){
          this.resp='Tag je uspešno kreiran'
        }
        else{
          this.resp='Tag successfully created'
        }
        setTimeout(() => {
          this.dialogRef.close(true);
        }, 1800);
      },
      error: err => {
        // console.log(err.error);
        if(!this.translate){
          this.errorMsg=err.error.errors[0].error;
        }
        else{
          if(err.error.errors[0].property=='Name'){

            this.errorMsg='NameSr already exists!';
          }
          if(err.error.errors[0].property=='NameUs'){
            this.errorMsg='NameUs already exists!'
          }
        }
      }
    })

  }

  ngOnInit(): void {
    this.translateChng()
    this.addTag=new FormGroup({
      nameSr:new FormControl("", [Validators.required,Validators.minLength(3),Validators.maxLength(20)]),
      nameUs:new FormControl("", [Validators.required,Validators.minLength(3),Validators.maxLength(20)]),
    });
  }

}

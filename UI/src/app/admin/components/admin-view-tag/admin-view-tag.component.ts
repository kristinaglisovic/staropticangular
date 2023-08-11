import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Tag } from 'src/app/interfaces/Tag/ITag';
import { IUpdateTagRequest } from 'src/app/interfaces/Tag/IUpdateTagRequest';
import { TagService } from 'src/app/services/Tag/tag.service';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';

@Component({
  selector: 'app-admin-view-tag',
  templateUrl: './admin-view-tag.component.html',
  styleUrls: ['./admin-view-tag.component.css']
})
export class AdminViewTagComponent implements OnInit {

  constructor(private ts:TranslateToEngService,private tagService:TagService, @Inject(MAT_DIALOG_DATA) private tag: Tag, private dialogRef: MatDialogRef<AdminViewTagComponent>) { }

  editTag:FormGroup;

  translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;


  errorMsg:string;
  resp:string;

  translateChng(){
    this.ts.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
    })
  }


  
  onSubmit():void{
    const updateTagRequest:IUpdateTagRequest={
      tagId:this.tag.id,
      name:this.editTag.value.nameSr,
      nameUs:this.editTag.value.nameUs
      
    }

    this.tagService.updateTag(updateTagRequest).subscribe({
      next: data => {
        this.errorMsg='';
        if(!this.translate){
          this.resp='Tag je uspešno ažuriran'
        }
        else{
          this.resp='Tag successfully edited'
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
    this.editTag=new FormGroup({
      nameSr:new FormControl(this.tag.nameSr, [Validators.required,Validators.minLength(3),Validators.maxLength(20)]),
      nameUs:new FormControl(this.tag.nameUs, [Validators.required,Validators.minLength(3),Validators.maxLength(20)]),
    });
  }
}

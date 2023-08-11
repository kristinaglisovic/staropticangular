import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { IAddCategoryRequest } from 'src/app/interfaces/Category/IAddCategoryRequest';
import { CategoryService } from 'src/app/services/Category/category.service';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnInit {

  constructor(private categoryService:CategoryService,private dialogRef: MatDialogRef<AddCategoryComponent>,private ts:TranslateToEngService) { }
  
  
  category:any;

  translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;

  errorMsg:string;
  resp:string;


  addCategory:FormGroup;


    translateChng(){
    this.ts.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
    })
  }




  ngOnInit(): void {
    this.addCategory=new FormGroup({
      nameSr:new FormControl("", [Validators.required,Validators.minLength(3),Validators.maxLength(20)]),
      nameUs:new FormControl("", [Validators.required,Validators.minLength(3),Validators.maxLength(20)]),
      descriptionSr:new FormControl("", [Validators.required,Validators.minLength(3),Validators.maxLength(50)]),
      descriptionUs:new FormControl("", [Validators.required,Validators.minLength(3),Validators.maxLength(50)])
    });

  }



  onSubmit():void{
    
    const addCategoryRequest:IAddCategoryRequest={
      name:this.addCategory.value.nameSr,
      description:this.addCategory.value.descriptionSr,
      nameEng:this.addCategory.value.nameUs,
      descriptionEng:this.addCategory.value.descriptionUs
    }
    this.categoryService.addCategory(addCategoryRequest).subscribe({
          next: data => {
            this.errorMsg='';
            if(!this.translate){
              this.resp='Kategorija je uspešno kreirana'
            }
            else{
              this.resp='Category successfully created'
            }
            setTimeout(() => {
              this.dialogRef.close(true);
            }, 1800);
          },
          error: err => {
            if(!this.translate){
              this.errorMsg=err.error.errors[0].error;
            }
            else{
              if(err.error.errors[0].property=='Name'){
    
                this.errorMsg='NameSr already exists!';
              }
              if(err.error.errors[0].property=='NameEng'){
                this.errorMsg='NameUs already exists!'
              }
            }
          }
      })
      
  }

}

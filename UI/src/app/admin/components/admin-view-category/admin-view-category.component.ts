import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Category } from 'src/app/interfaces/Category/ICategory';
import { IUpdateCategoryRequest } from 'src/app/interfaces/Category/IUpdateCategoryRequest';
import { CategoryService } from 'src/app/services/Category/category.service';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';

@Component({
  selector: 'app-admin-view-category',
  templateUrl: './admin-view-category.component.html',
  styleUrls: ['./admin-view-category.component.css']
})
export class AdminViewCategoryComponent implements OnInit {

  constructor(private ts:TranslateToEngService,private catS:CategoryService, @Inject(MAT_DIALOG_DATA) private cat: Category, private dialogRef: MatDialogRef<AdminViewCategoryComponent>) {
    
  }
  editCategory:FormGroup;

  translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;


  errorMsg:string;
  resp:string;

  translateChng(){
    this.ts.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
    })
  }


  
  onSubmit():void{
    const updateCategoryRequest:IUpdateCategoryRequest={
      name: this.editCategory.value.nameSr,
      nameEng: this.editCategory.value.nameUs,
      description:this.editCategory.value.descriptionSr,
      descriptionEng:this.editCategory.value.descriptionUs,
      categoryId:this.cat.id
    }

    this.catS.updateCategory(updateCategoryRequest).subscribe({
      next: data => {
        this.errorMsg='';
        if(!this.translate){
          this.resp='Kategorija je uspešno ažurirana'
        }
        else{
          this.resp='Category successfully edited'
        }
        setTimeout(() => {
          this.dialogRef.close(true);
        }, 1800);
      },
      error: err => {
        if(!this.translate){
          this.errorMsg=err.error.message;
        }
        else{
          if(err.error.property=='nameSr'){

            this.errorMsg='NameSr already exists!';
          }
          if(err.error.property=='nameUs'){
            this.errorMsg='NameUs already exists!'
          }
        }
      }
    })
  }

  ngOnInit(): void {
    this.translateChng()
    this.editCategory=new FormGroup({
      nameSr:new FormControl(this.cat.nameSr, [Validators.required,Validators.minLength(3),Validators.maxLength(20)]),
      nameUs:new FormControl(this.cat.nameUs, [Validators.required,Validators.minLength(3),Validators.maxLength(20)]),
      descriptionSr:new FormControl(this.cat.descriptionSr, [Validators.required,Validators.minLength(3),Validators.maxLength(50)]),
      descriptionUs:new FormControl(this.cat.descriptionUs, [Validators.required,Validators.minLength(3),Validators.maxLength(50)])
    });
  }
}


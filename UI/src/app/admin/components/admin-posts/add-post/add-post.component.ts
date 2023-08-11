import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Category } from 'src/app/interfaces/Category/ICategory';
import { Tag } from 'src/app/interfaces/Tag/ITag';
import { CategoryService } from 'src/app/services/Category/category.service';
import { PostService } from 'src/app/services/Post/post.service';
import { TagService } from 'src/app/services/Tag/tag.service';
import { HttpErrorResponse, HttpParams } from '@angular/common/http';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';
import { ImagesService } from 'src/app/services/Images/images.service';
import { AuthService } from 'src/app/services/Auth/auth.service';
import { IAddPostRequest } from 'src/app/interfaces/Post/IAddPostRequest';
import { MatDialogRef } from '@angular/material/dialog';
@Component({
  selector: 'app-add-post',
  templateUrl: './add-post.component.html',
  styleUrls: ['./add-post.component.css']
})
export class AddPostComponent implements OnInit {


  addPost:FormGroup;
  errorMsg:string;
  resp:string;
  translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;

  
  translateChng(){
    this.tss.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
    })
  }


  constructor(private pService:PostService,private auth:AuthService,private cS:CategoryService,private fb:FormBuilder,private ts:TagService,private tss:TranslateToEngService,private is:ImagesService,private dialogRef: MatDialogRef<AddPostComponent>) {
     
   }

   get f(){return this.addPost.controls;}


  categories:Category[]=[];
  
  selectedCategoriesIds=[];
  selectedTagsIds=[];
  selectedImagesIds=[];
  

  tags:Tag[]=[];
  images:any;

  


  selectedCategories(e){
    if (e.source.selected) this.selectedCategoriesIds.push(e.source.value);
    else {
      const index = this.selectedCategoriesIds.indexOf(e.source.value);
      if (index > -1) {
        this.selectedCategoriesIds.splice(index, 1);
      }
    }
  }

  selectedImages(e){
    if (e.source.selected) this.selectedImagesIds.push(e.source.value);
    else {
      const index = this.selectedImagesIds.indexOf(e.source.value);
      if (index > -1) {
        this.selectedImagesIds.splice(index, 1);
      }
    }
  }

  selectedTags(e){
    if (e.source.selected) this.selectedTagsIds.push(e.source.value);
    else {
      const index = this.  selectedTagsIds.indexOf(e.source.value);
      if (index > -1) {
        this.selectedTagsIds.splice(index, 1);
      }
    }
  }


  onlyActive(items: any[]): any[] {
    return items.filter(p => p.isActive==true);
  }

  getTags(){
    this.ts.getPostTags(this.translate)
      .subscribe((response:any)=>{
        console.log(response)
        this.tags=response.data;
      }
      );
  }

  getCategories(){
    this.cS.getAllPostCategories(this.translate)
      .subscribe((response:any)=>{
        this.categories=response.data;
      }
      );
  } 

  
  getImages(){
    this.is.postsOnlyImages()
      .subscribe((response:any)=>{
        this.images=response.data;
      }
      );
  }

  ngOnInit(): void {
    this.translateChng();
    this.getCategories();
    this.getTags();
    this.getImages();
    this.addPost=this.fb.group({
      CategoriesIds:this.fb.control('',[Validators.required]),
      TagsIds:this.fb.control('',[Validators.required]),
      ImagesIds:this.fb.control('',[Validators.required]),
      Title:this.fb.control('',[Validators.required,Validators.minLength(3)]),
      Description:this.fb.control('',[Validators.required,Validators.minLength(5)]),
      Heading1:this.fb.control('',[Validators.required,Validators.minLength(3),Validators.maxLength(40)]),
      Heading2:this.fb.control('',[Validators.minLength(3),Validators.maxLength(40)]),
      Text1:this.fb.control('',[Validators.required,Validators.minLength(3)]),
      Text2:this.fb.control('',[Validators.minLength(3)]),
      TitleEng:this.fb.control('',[Validators.required,Validators.minLength(3)]),
      DescriptionEng:this.fb.control('',[Validators.required,Validators.minLength(5)]),
      Heading1Eng:this.fb.control('',[Validators.required,Validators.minLength(3),Validators.maxLength(40)]),
      Heading2Eng:this.fb.control('',[Validators.minLength(3),Validators.maxLength(40)]),
      Text1Eng:this.fb.control('',[Validators.required,Validators.minLength(3)]),
      Text2Eng:this.fb.control('',[Validators.minLength(3)]),
    })
  }


  onSubmit():void{

    const addPostRequest:IAddPostRequest={
      Title:this.addPost.get('Title').value,
      TitleEng:this.addPost.get('TitleEng').value,
      Description:this.addPost.get('Description').value,
      DescriptionEng:this.addPost.get('DescriptionEng').value,
      Heading1:this.addPost.get('Heading1').value,
      Heading1Eng:this.addPost.get('Heading1Eng').value,
      Heading2:this.addPost.get('Heading2').value,
      Heading2Eng:this.addPost.get('Heading2Eng').value,
      Text1:this.addPost.get('Text1').value,
      Text1Eng:this.addPost.get('Text1Eng').value,
      Text2:this.addPost.get('Text2').value,
      Text2Eng:this.addPost.get('Text2Eng').value,
      TagsIds:this.selectedTagsIds,
      CategoriesIds:this.selectedCategoriesIds,
      ImagesIds:this.selectedImagesIds,
      UserId:this.auth.user.id,
    }

    this.pService.addPost(addPostRequest).subscribe({
      next: data => {
        this.errorMsg='';
         if(!this.translate){
              this.resp='Post je uspešno kreiran'
            }
            else{
              this.resp='Post successfully created'
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
          if(err.error.errors[0].property=='Title'){

            this.errorMsg='Title sr already exists!';
          }
          if(err.error.errors[0].property=='TitleEng'){
            this.errorMsg='Title eng already exists!'
          }
        }
      }
  })
    
   }

}

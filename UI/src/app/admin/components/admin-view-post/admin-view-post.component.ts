import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Category } from 'src/app/interfaces/Category/ICategory';
import { IUpdatePostRequest } from 'src/app/interfaces/Post/IUpdatePostRequest';
import { Tag } from 'src/app/interfaces/Tag/ITag';
import { AuthService } from 'src/app/services/Auth/auth.service';
import { CategoryService } from 'src/app/services/Category/category.service';
import { ImagesService } from 'src/app/services/Images/images.service';
import { PostService } from 'src/app/services/Post/post.service';
import { TagService } from 'src/app/services/Tag/tag.service';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';

@Component({
  selector: 'app-admin-view-post',
  templateUrl: './admin-view-post.component.html',
  styleUrls: ['./admin-view-post.component.css']
})
export class AdminViewPostComponent implements OnInit {

  constructor(private postService:PostService,private fb:FormBuilder, @Inject(MAT_DIALOG_DATA) private postdata:any,private ts:TranslateToEngService,private dialogRef:MatDialogRef<AdminViewPostComponent>, 
  private auth:AuthService,private cS:CategoryService,private tagService:TagService,private is:ImagesService) { }
  

  translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;
 
  editPost:FormGroup;
  errorMsg:string;
  resp:string;
  selectedCategoriesIds=[];
  selectedTagsIds=[];
  selectedImagesIds=[];
  categories:Category[]=[];
  tags:Tag[]=[];
  images:any;


  translateChng(){
    this.ts.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
    })
  }


  selectedCategories(e){
    if (e.source.selected) this.selectedCategoriesIds.push(e.source.value);
    else {
      const index = this.selectedCategoriesIds.indexOf(e.source.value);
      if (index > -1) {
        this.selectedCategoriesIds.splice(index, 1);
      }
    }
    console.log(this.selectedCategoriesIds)

  }

  selectedImages(e){
    if (e.source.selected) this.selectedImagesIds.push(e.source.value);
    else {
      const index = this.selectedImagesIds.indexOf(e.source.value);
      if (index > -1) {
        this.selectedImagesIds.splice(index, 1);
      }
    }
    console.log(this.selectedImagesIds)

  }

  selectedTags(e){
    if (e.source.selected) this.selectedTagsIds.push(e.source.value);
    else {
      const index = this.selectedTagsIds.indexOf(e.source.value);
      if (index > -1) {
        this.selectedTagsIds.splice(index, 1);
      }
    }
    console.log(this.selectedTagsIds)
  }

  onlyActive(items: any[]): any[] {
    return items.filter(p => p.isActive==true);
  }


  getTags(){
    this.tagService.getPostTags(this.translate)
      .subscribe((response:any)=>{
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
    this.getImages();
    this.getCategories();
    this.getTags();
    this.editPost=this.fb.group({
      CategoriesIds:this.fb.control(this.postdata.categoriesIds,[Validators.required]),
      TagsIds:this.fb.control(this.postdata.tagsIds,[Validators.required]),
      ImagesIds:this.fb.control(this.postdata.imagesIds,[Validators.required]),
      Title:this.fb.control(this.postdata.titleSr,[Validators.required,Validators.minLength(3)]),
      Description:this.fb.control(this.postdata.descriptionSr,[Validators.required,Validators.minLength(5)]),
      Heading1:this.fb.control(this.postdata.heading1Sr,[Validators.required,Validators.minLength(3),Validators.maxLength(40)]),
      Heading2:this.fb.control(this.postdata.heading2Sr,[Validators.minLength(3),Validators.maxLength(40)]),
      Text1:this.fb.control(this.postdata.text1Sr,[Validators.required,Validators.minLength(3)]),
      Text2:this.fb.control(this.postdata.text2Sr,[Validators.minLength(3)]),
      TitleEng:this.fb.control(this.postdata.titleEng,[Validators.required,Validators.minLength(3)]),
      DescriptionEng:this.fb.control(this.postdata.descriptionEng,[Validators.required,Validators.minLength(5)]),
      Heading1Eng:this.fb.control(this.postdata.heading1Eng,[Validators.required,Validators.minLength(3),Validators.maxLength(40)]),
      Heading2Eng:this.fb.control(this.postdata.heading2Eng,[Validators.minLength(3),Validators.maxLength(40)]),
      Text1Eng:this.fb.control(this.postdata.text1Eng,[Validators.required,Validators.minLength(3)]),
      Text2Eng:this.fb.control(this.postdata.text2Eng,[Validators.minLength(3)]),
    })
  }

  
  onSubmit():void{

    const  updatePostRequest:IUpdatePostRequest={
      Title:this.editPost.get('Title').value,
      TitleEng:this.editPost.get('TitleEng').value,
      Description:this.editPost.get('Description').value,
      DescriptionEng:this.editPost.get('DescriptionEng').value,
      Heading1:this.editPost.get('Heading1').value,
      Heading1Eng:this.editPost.get('Heading1Eng').value,
      Heading2:this.editPost.get('Heading2').value,
      Heading2Eng:this.editPost.get('Heading2Eng').value,
      Text1:this.editPost.get('Text1').value,
      Text1Eng:this.editPost.get('Text1Eng').value,
      Text2:this.editPost.get('Text2').value,
      Text2Eng:this.editPost.get('Text2Eng').value,
      TagsIds:this.selectedTagsIds,
      CategoriesIds:this.selectedCategoriesIds,
      ImagesIds:this.selectedImagesIds,
      UserId:this.auth.user.id,
      PostId:this.postdata.id,
    }

    this.postService.updatePost(updatePostRequest).subscribe({
      next: data => {
        this.errorMsg='';
         if(!this.translate){
              this.resp='Post je uspešno ažuriran'
            }
            else{
              this.resp='Post successfully edited'
            }
            setTimeout(() => {
              this.dialogRef.close(true);
            }, 1800);
      },
      error: err => {
        if(!this.translate){
          if(err.error.message){
              this.errorMsg=err.error.message;
         }
        }
        else{
          if(err.error.property=='TitleSr'){
            this.errorMsg='Title sr already exists!';
          }
          if(err.error.property=='TitleEng'){
            this.errorMsg='Title eng already exists!'
          }
        }
      }
    })
  }

  
}

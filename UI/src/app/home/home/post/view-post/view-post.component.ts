import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { ILikeRequest } from 'src/app/interfaces/Post/ILikeRequest';
import { AuthService } from 'src/app/services/Auth/auth.service';
import { CommentService } from 'src/app/services/Comment/comment.service';
import { PostService } from 'src/app/services/Post/post.service';
import { ReloadPostService } from 'src/app/services/reload/reload-post.service';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';


@Component({
  selector: 'app-view-post',
  templateUrl: './view-post.component.html',
  styleUrls: ['./view-post.component.css']
})
export class ViewPostComponent implements OnInit {

  constructor(private route:ActivatedRoute,private postService:PostService,private ts:TranslateToEngService, public auth:AuthService,private CommService:CommentService,private rs:ReloadPostService) { 
  }

  errorMessage:any;
  post:any;
  isReady: boolean=false;
  translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;
  images:any=[];



  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[]=[];


  ToReload(){
    this.rs.Reload().subscribe((d=>{
      if(d==true){
        this.getPost()
      }
      
    }))

  }

  deleteComment(id:number):void{
    this.CommService.deleteComment(id).subscribe(resp=>{
       this.getPost()
       }
      );
  }
  
  translateChng(){
    this.ts.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
      this.getPost()
    })
  }


  isLiked(post:any):boolean{
    return post.likesInfo.find(l=>l.userId==this.auth.user.id);
  }


  likePost(id):void{
    const likeRequest:ILikeRequest={
      UserId:this.auth.user.id,
      PostId:id
    }
    this.postService.likePost(likeRequest).subscribe(resp=>{
         this.getPost()
         this.rs.ReloadPostPage(true);
         }
    );
}



  getPost(){
    this.route.paramMap.subscribe(
      params=>{
      const id=params.get('id');
        if(id){
        this.postService.getOnePostById(id,this.translate).subscribe(response=>{
          console.log(response)
          this.post=response;
          this.images=response.images;
          if(this.galleryImages.length>0){
            this.galleryImages=[];
         }
          this.images.forEach(e=>{
             this.galleryImages.push({medium:'http://localhost:5000/Images/'+e,small:'http://localhost:5000/Images/'+e,big:'http://localhost:5000/Images/'+e})
          })
          this.isReady=true;
          },(error)=>{
            console.log(error);
            this.errorMessage=error;
          }
          );
        }
      }
    )
  }

  
  ngOnInit(): void {
    

    this.translateChng()
    this.getPost()
    this.ToReload()



    this.galleryOptions = [
      {
        width: '600px',
        height: '400px',
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide
      },
      // max-width 800
      {
        breakpoint: 800,
        width: '100%',
        height: '600px',
        imagePercent: 80,
        thumbnailsPercent: 20,
        thumbnailsMargin: 20,
        thumbnailMargin: 20
      },
      // max-width 400
      {
        breakpoint: 400,
        preview: false
      }
    ];
  }

}

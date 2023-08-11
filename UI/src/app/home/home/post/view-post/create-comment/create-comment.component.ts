import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IAddCommentRequest } from 'src/app/interfaces/Comment/IAddCommentRequest';
import { AuthService } from 'src/app/services/Auth/auth.service';
import { CommentService } from 'src/app/services/Comment/comment.service';
import { ReloadPostService } from 'src/app/services/reload/reload-post.service';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';

@Component({
  selector: 'app-create-comment',
  templateUrl: './create-comment.component.html',
  styleUrls: ['./create-comment.component.css']
})
export class CreateCommentComponent implements OnInit {

  @Input() PostId:number

  addComment:FormGroup;

  translate:boolean=false;
  
  constructor(
    private CommentService:CommentService,
    public auth: AuthService,
    private rp:ReloadPostService, private ts:TranslateToEngService) { }

  ngOnInit(): void {
    this.addComment=new FormGroup({
      Text:new FormControl("", [Validators.required,
                                Validators.minLength(1),Validators.maxLength(100)]),
    });
    this.translateChng()
  }

  get f(){return this.addComment.controls;}

  resp:string=''
  resp2:string=''

  translateChng(){
    this.ts.TranslateTo().subscribe((d=>{
      this.translate=d;
    }))
    
  }

  onSubmit():void{
    const addCommentRequest:IAddCommentRequest={
      UserId:this.auth.user.id,
      Text:this.addComment.value.Text,
      PostId:this.PostId
    }

    this.CommentService.addComment(addCommentRequest).subscribe
    (r=>{
        this.resp2=''
      if(this.translate==false){
        this.resp='Komentar je uspešno dodat!'
      }
      else{
        this.resp='Comment added successfully!'
      }
      setTimeout(() => {
        this.resp=''
        this.rp.ReloadPostPage(true);
        this.addComment.reset();
      }, 1000);
    },
    (error:HttpErrorResponse)=>{
      if(error.status==409){
        
        if(this.translate==false){
            this.resp2="Već ste ostavili isti komentar na ovoj objavi.";
        }
        else{
          this.resp2="You have already left the same comment on this post.";
        }
      }
    });
    
  }

}

import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/interfaces/Post/IPost';
import { PostService } from 'src/app/services/Post/post.service';
import { ReloadPostService } from 'src/app/services/reload/reload-post.service';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';

@Component({
  selector: 'app-recent-p',
  templateUrl: './recent-p.component.html',
  styleUrls: ['./recent-p.component.css']
})
export class RecentPComponent implements OnInit {

  constructor(private PostService:PostService, private ts:TranslateToEngService,private rs:ReloadPostService) { }


  posts:Post[]=[];


  translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;

  params=new HttpParams();

  ToReload(){
    this.rs.Reload().subscribe((d=>{

      if(d==true){

        this.getPosts()
      }
      
    }))

  }

  getPosts(){
    this.PostService.getRecentPosts(this.params,this.translate)
      .subscribe((response:any)=>{
        this.posts=response.data;
      }
      );
  }

  onlyActive(items: any[]): any[] {
    return items.filter(p => p.isActive==true);
  }

  translateChng(){
    this.ts.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
      this.getPosts()
    })
  }

  
  ngOnInit(): void {
    this.getPosts();
    this.ToReload()
    this.translateChng();

  }

 
}

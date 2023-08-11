import { HttpParams } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { ILikeRequest } from 'src/app/interfaces/Post/ILikeRequest';
import { Post } from 'src/app/interfaces/Post/IPost';
import { AuthService } from 'src/app/services/Auth/auth.service';
import { PostService } from 'src/app/services/Post/post.service';
import { ReloadPostService } from 'src/app/services/reload/reload-post.service';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  constructor(private PostService:PostService, private ts:TranslateToEngService,public auth:AuthService,private rs:ReloadPostService) { 
  }
  posts:Post[]=[];

  currentPage: number = 1;
  pagesCount: number = 0;
  itemsPerPage:number;

  translate:boolean=localStorage.getItem('selectedLanguage')=='sr'? false:true;
  

  total: number = 0;
  params=new HttpParams();

  fetchPosts(){
      this.PostService.getAllPosts(this.currentPage,this.params,this.translate).subscribe((response:any)=>{
        console.log(response)
        this.posts=response.data;

        this.total=response.totalCount;
        
        this.itemsPerPage=response.itemsPerPage;
        this.currentPage=response.currentPage;
        this.pagesCount=response.pagesCount;
      })
      
  }

  isLiked(post:any):boolean{
    return post.likesInfo.find(l=>l.userId==this.auth.user.id);
  }

  translateCng(){
    this.ts.TranslateTo().subscribe(()=>{
      this.translate= localStorage.getItem('selectedLanguage')=='sr'? false:true;
      this.fetchPosts()
    })
  }

 
  likePost(id):void{
      const likeRequest:ILikeRequest={
        UserId:this.auth.user.id,
        PostId:id
      }
      this.PostService.likePost(likeRequest).subscribe(resp=>{
           this.fetchPosts()
           this.rs.ReloadPostPage(true);
           }
      );
  }


  onlyActive(items: any[]): any[] {
    return items.filter(p => p.isActive==true);
  }


  pageChangeEvent(event: number){
    this.currentPage = event;
    this.fetchPosts();
 }
 

  ngOnInit(): void {
    this.fetchPosts()
    this.translateCng()
  }


   
 sortBy(e){

    if(this.params.has('sortOrder')){
     this.params=this.params.delete('sortOrder')
    }
    this.params=this.params.append("sortOrder",e)
    this.currentPage=1;
    this.fetchPosts()
 }

 onSearchTextEntered(searchValue:string){
    if(this.params.has('keyword')){
      this.params=this.params.delete('keyword')
    }
    this.params = this.params.append("keyword", searchValue)

    this.currentPage=1;
    this.fetchPosts()
 }



 onTagSelected(e){
  if(this.params.getAll('TagsIds')){
    this.params=this.params.delete('TagsIds');
  }
  e.forEach(element => {
    this.params=this.params.append('TagsIds',element)
  });
  this.currentPage=1;
  this.fetchPosts()
}

onCatSelected(e){
  if(this.params.getAll('CategoryIds')){
    this.params=this.params.delete('CategoryIds');
  }
  e.forEach(element => {
    this.params=this.params.append('CategoryIds',element)
  });
  this.currentPage=1;
  this.fetchPosts()
}

}
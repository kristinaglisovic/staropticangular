import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { IAddPostRequest } from 'src/app/interfaces/Post/IAddPostRequest';
import { IUpdatePostRequest } from 'src/app/interfaces/Post/IUpdatePostRequest';
import { environment } from 'src/environments/environment';
import { Post } from '../../interfaces/Post/IPost';
import {ILikeRequest} from 'src/app/interfaces/Post/ILikeRequest';
@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http:HttpClient) { }

  apiBaseUrl=environment.apiBaseUrl;
  getHeaders(): any {
    let token = localStorage.getItem("token");

    if(token)
      return {
        Authorization: "Bearer " + localStorage.getItem("token")
      };
    
    return {};
  }



  getAllPosts(pageNo:number,qparams:HttpParams,eng:boolean):Observable<Post[]>{
    return this.http.get<Post[]>(this.apiBaseUrl+'/api/posts'+'?page='+ pageNo+'&isActive=true'+"&eng="+eng,{params:qparams});
  }
 
  getRecentPosts(qparams:HttpParams,eng:boolean):Observable<Post[]>{
    return this.http.get<Post[]>(this.apiBaseUrl+'/api/posts?recent=true&perPage=5'+"&eng="+eng,{params:qparams});
  }


  getOnePostById(id:string,eng:boolean):Observable<Post>{
    return this.http.get<Post>(this.apiBaseUrl+'/api/posts/'+id+"?eng="+eng).pipe(catchError(this.handleError));
  }




  /*proveriti da li se koristi */
  getAllActivePosts():Observable<Post[]>{
    return this.http.get<Post[]>(this.apiBaseUrl+'/api/posts?isActive=true',{headers: this.getHeaders()});
  }

 
  /*back*/
  getAllAdminPosts(pageNo:number,perPage:number,eng:boolean):Observable<Post[]>{
    return this.http.get<Post[]>(this.apiBaseUrl+'/api/posts'+'?page='+ pageNo+'&perPage='+perPage+'&eng='+eng, {headers: this.getHeaders()});
  }



  deletePost(id:number){
    return this.http.delete<Post>(this.apiBaseUrl+'/api/posts/'+id,{headers: this.getHeaders()});
  }
  
  changeStatus(id:number){
    return this.http.patch<Post>(this.apiBaseUrl+"/api/posts/"+id,id,{headers: this.getHeaders()});
  }







  updatePost(updatePostRequest:IUpdatePostRequest ):Observable<Post>{
    return this.http.patch<Post>(this.apiBaseUrl+'/api/posts/edit',updatePostRequest,{headers: this.getHeaders()});
  }

 

  addPost(addPostRequest:IAddPostRequest):Observable<Post>{
    return this.http.post<Post>(this.apiBaseUrl+'/api/posts/',addPostRequest,{headers: this.getHeaders()});
  }



  likePost(likeRequest:ILikeRequest){
    return this.http.patch<Post>(this.apiBaseUrl+"/api/posts/like/",likeRequest,{headers: this.getHeaders()});
  }


  private handleError(error: HttpErrorResponse) {
    let errorMessage='';
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      if(error.status==404){
        errorMessage='Not found'
      }
      
    }
    // Return an observable with a user-facing error message.

    return throwError(() => new Error(errorMessage));
  }

   
}

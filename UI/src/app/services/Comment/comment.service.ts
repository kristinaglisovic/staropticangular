import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { IAddCommentRequest } from 'src/app/interfaces/Comment/IAddCommentRequest';
import { IComment } from 'src/app/interfaces/Comment/IComment';
import { IUpdateCommentRequest } from 'src/app/interfaces/Comment/IUpdateCommentRequest';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class CommentService {

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


  addComment(addCommentRequest:IAddCommentRequest ):Observable<Comment>{
    return this.http.post<Comment>(this.apiBaseUrl+'/api/comments/',addCommentRequest,{headers: this.getHeaders()});
  }


  getAllComments(pageNo:number,perPage:number):Observable<IComment[]>{
    return this.http.get<IComment[]>(this.apiBaseUrl+'/api/comments'+'?page='+ pageNo+'&perPage='+perPage,{headers: this.getHeaders()});
  }


  changeStatus(id:number):Observable<Comment>{
    return this.http.patch<Comment>(this.apiBaseUrl+'/api/comments/'+id,id,{headers: this.getHeaders()});
  }


  
  deleteComment(id:number){
    return this.http.delete<Comment>(this.apiBaseUrl+'/api/comments/'+id,{headers: this.getHeaders()});
  }


  getAllActiveComments():Observable<IComment[]>{
    return this.http.get<IComment[]>(this.apiBaseUrl+'/api/comments?isActive=true',{headers: this.getHeaders()});
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

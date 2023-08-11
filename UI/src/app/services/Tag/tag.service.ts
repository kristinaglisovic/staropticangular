import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Tag } from 'src/app/interfaces/Tag/ITag';
import { IUpdateTagRequest } from 'src/app/interfaces/Tag/IUpdateTagRequest';
import { environment } from 'src/environments/environment';
import { catchError, Observable, throwError } from 'rxjs';
import { IAddTagRequest } from 'src/app/interfaces/Tag/IAddTagRequest';

@Injectable({
  providedIn: 'root'
})
export class TagService {

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



  getAllTags(pageNo:number,perPage:number,qparams:HttpParams,eng:boolean):Observable<Tag[]>{
    return this.http.get<Tag[]>(this.apiBaseUrl+'/api/tags'+'?page='+ pageNo+'&perPage='+perPage+"&eng="+eng,{params:qparams});
  }

  
  getPostTags(eng:boolean):Observable<Tag[]>{
    return this.http.get<Tag[]>(this.apiBaseUrl+'/api/tags?perPage=-1'+"&eng="+eng);
  }

  getAllActiveTags():Observable<Tag[]>{
    return this.http.get<Tag[]>(this.apiBaseUrl+'/api/tags?isActive=true',{headers: this.getHeaders()});
  }


  deleteTag(id:number){
    return this.http.delete<Tag>(this.apiBaseUrl+'/api/tags/'+id,{headers: this.getHeaders()});
  }


  changeStatus(id:number){
    return this.http.patch<Tag>(this.apiBaseUrl+"/api/tags/"+id,id,{headers: this.getHeaders()});
  }



  
  addTag(addTagRequest:IAddTagRequest ):Observable<Tag>{
    return this.http.post<Tag>(this.apiBaseUrl+'/api/tags/',addTagRequest, {headers: this.getHeaders()});
  }

  updateTag(updateTagRequest:IUpdateTagRequest ):Observable<Tag>{
    return this.http.patch<Tag>(this.apiBaseUrl+'/api/tags/edit',updateTagRequest,{headers: this.getHeaders()});
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

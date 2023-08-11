import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from 'src/app/interfaces/Category/ICategory';
import { environment } from 'src/environments/environment';
import { catchError, Observable, throwError } from 'rxjs';
import { IUpdateCategoryRequest } from 'src/app/interfaces/Category/IUpdateCategoryRequest';
import { IAddCategoryRequest } from 'src/app/interfaces/Category/IAddCategoryRequest';
import { QueryFlags } from '@angular/compiler/src/render3/view/compiler';
@Injectable({
  providedIn: 'root'
})
export class CategoryService {

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

  
  /*back*/
  getAllCategories(pageNo:number,perPage:number,eng:boolean):Observable<Category[]>{
    return this.http.get<Category[]>(this.apiBaseUrl+'/api/categories'+'?page='+ pageNo+'&perPage='+perPage+"&eng="+eng,{headers: this.getHeaders()});
  }

  

  getAllActiveCategories():Observable<Category[]>{
    return this.http.get<Category[]>(this.apiBaseUrl+'/api/categories?isActive=true',{headers: this.getHeaders()});
  }



  /*front*/
  getAllPostCategories(eng:boolean):Observable<Category[]>{
    return this.http.get<Category[]>(this.apiBaseUrl+'/api/categories?perPage=-1'+"&eng="+eng);
  }

  

  deleteCategory(id:number){
    return this.http.delete<Category>(this.apiBaseUrl+'/api/categories/'+id,{headers: this.getHeaders()});
  }

  changeStatus(id:number){
    return this.http.patch<Category>(this.apiBaseUrl+"/api/categories/"+id,id,{headers: this.getHeaders()});
  }

  addCategory(addCategoryRequest:IAddCategoryRequest ):Observable<Category>{
    return this.http.post<Category>(this.apiBaseUrl+'/api/categories/',addCategoryRequest,{headers: this.getHeaders()});
  }

  updateCategory(updateCategoryRequest:IUpdateCategoryRequest ):Observable<Category>{
    return this.http.patch<Category>(this.apiBaseUrl+'/api/categories/edit',updateCategoryRequest,{headers: this.getHeaders()});
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

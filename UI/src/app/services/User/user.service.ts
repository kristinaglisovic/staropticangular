import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from 'src/app/interfaces/User/IUser';
import { environment } from 'src/environments/environment';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

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



  getAllUsers(pageNo:number,perPage:number):Observable<User[]>{
    return this.http.get<User[]>(this.apiBaseUrl+'/api/users'+'?page='+ pageNo+'&perPage='+perPage,{headers: this.getHeaders()});
  }

  getAllActiveUsers():Observable<User[]>{
    return this.http.get<User[]>(this.apiBaseUrl+'/api/users?isActive=true',{headers: this.getHeaders()});
  }

    
  deleteUser(id:number){
    return this.http.delete<User>(this.apiBaseUrl+'/api/users/'+id,{headers: this.getHeaders()});
  }

  deactivateUserAndItsPosts(id:number){
      return this.http.patch<User>(this.apiBaseUrl+"/api/users/deactivate/"+id,id,{headers:this.getHeaders()});
  }

  changeStatus(id:number){
    return this.http.patch<User>(this.apiBaseUrl+"/api/users/"+id,id,{headers:this.getHeaders()});
  }





  getOneUserById(id:any):Observable<User>{
    return this.http.get<User>(this.apiBaseUrl+'/api/users/'+id,{headers: this.getHeaders()}).pipe(catchError(this.handleError));
  }


  updateUser(formData:any):Observable<User>{
    return this.http.patch<User>(this.apiBaseUrl+'/api/users/edit',formData,{headers:this.getHeaders()});
  }





  addUser(formData:any ):Observable<User>{
    return this.http.post<User>(this.apiBaseUrl+'/api/users/',formData);
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

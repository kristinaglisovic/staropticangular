import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  user:any=JSON.parse(localStorage.getItem('user'));

  apiBaseUrl=environment.apiBaseUrl;
 
  
  constructor(private http:HttpClient) { }
  login(data:any):Observable<any>{
    return this.http.post(this.apiBaseUrl+'/api/token',data);
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IImage } from 'src/app/interfaces/Image/IImage';
import { environment } from 'src/environments/environment';
import { catchError, Observable, throwError } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class ImagesService {

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



  getAllImages(pageNo:number,perPage:number):Observable<IImage[]>{
    return this.http.get<IImage[]>(this.apiBaseUrl+'/api/images'+'?page='+ pageNo+'&perPage='+perPage,{headers: this.getHeaders()});
  }

  deleteImage(id:number){
    return this.http.delete<IImage>(this.apiBaseUrl+'/api/images/'+id, {headers: this.getHeaders()});
  }

  addImage(formData:any){
    return this.http.post<IImage>(this.apiBaseUrl+'/api/images',formData,{headers: this.getHeaders()});
  }

  postsOnlyImages(){
    return this.http.get<IImage>(this.apiBaseUrl+'/api/images?perPage=-1&isActive=true&postsOnly=true',{headers: this.getHeaders()});
  }


}

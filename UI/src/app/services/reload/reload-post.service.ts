import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReloadPostService {



  private subject=new Subject<boolean>();
  constructor() { }

  ReloadPostPage(rp:boolean){
    this.subject.next(rp)
  }

  Reload():Observable<boolean>{

    return this.subject.asObservable();

  }
}

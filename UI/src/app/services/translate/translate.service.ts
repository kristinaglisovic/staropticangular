import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TranslateToEngService {

  private subject=new Subject<boolean>();
  constructor() { }

  TranslateToEng(eng:boolean){
    this.subject.next(eng)
  }

  TranslateTo():Observable<boolean>{
    return this.subject.asObservable();
  }

  

}

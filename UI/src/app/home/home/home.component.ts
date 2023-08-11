import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Post } from 'src/app/interfaces/Post/IPost';
import { PostService } from 'src/app/services/Post/post.service';
import { TranslateToEngService } from 'src/app/services/translate/translate.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  translate:boolean;
  
  constructor(private ts:TranslateToEngService) {
  }

  ngOnInit(): void {

    this.ts.TranslateTo().subscribe((d=>{
      this.translate=d;
    }))
  }


}

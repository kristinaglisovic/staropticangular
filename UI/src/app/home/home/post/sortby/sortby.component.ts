import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-sortby',
  templateUrl: './sortby.component.html',
  styleUrls: ['./sortby.component.css']
})
export class SortbyComponent implements OnInit {

  constructor() { }



  sortOrder:string;
  options: string[] = [
    'Komentari rastuće', 'Komentari opadajuće',  'Lajkovi rastuće','Lajkovi opadajuće','Podrazumevano'
  ];


  ngOnInit(): void {
  }
  @Output() sortOrderSelected:EventEmitter<string>=new EventEmitter<string>();
  
  sortBy(e){
    console.log(e)
    switch(e.source.value) {
      case 'Komentari rastuće':
         this.sortOrder='CommentsAsc'
        break;
      case 'Komentari opadajuće':
        this.sortOrder='CommentsDesc'
        break;
        case 'Lajkovi rastuće':
      this.sortOrder='LikesAsc'
        break;
    case 'Lajkovi opadajuće':
        this.sortOrder='LikesDesc'
        break;
      default:
      this.sortOrder=''
      break;
    }
    this.sortOrderSelected.emit(this.sortOrder)
  }
}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/Auth/auth.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  constructor(
    private router:Router,
    public auth: AuthService
    ) { }
  

  ngOnInit(): void {
  }

  navigate(){
    this.router.navigate(['/home'])
    .then(() => {
      window.location.reload();
    });
 }

  logout(){
    localStorage.removeItem('user');
    this.auth.user = null;
  }
}

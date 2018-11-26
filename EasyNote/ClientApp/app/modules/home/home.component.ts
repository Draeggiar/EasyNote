import { Component, OnInit } from '@angular/core';
import { UserService } from 'ClientApp/app/services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  isLoggedIn: Boolean;
  userName:string;

  constructor(private userService: UserService) {
  }

  logout() {
    this.userService.logout();
  }

  ngOnInit() {
    this.isLoggedIn = this.userService.isLoggedIn();
    this.userName = this.userService.getNameOfLoggedUser();
  }
}

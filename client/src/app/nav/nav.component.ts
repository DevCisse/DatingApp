import { Observable } from 'rxjs';
import { ThrowStmt } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/User';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};


  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    //this.getCurrentUser()

  }

  login() {
    this.accountService.login(this.model).subscribe(res => {
      console.log(res);

    }, error => {
      console.log(error);
    })
  }

  logout() {
    this.accountService.logout();

  }

  // getCurrentUser() {
  //   this.accountService.currentUser$.subscribe(user => {
  //     this.loggedIn = !!user
  //   }, error => {
  //     console.log(error);
  //   })
  // }
}

import { JsonPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { User } from './_models/User';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'The Dating App';
  users: any;

  constructor(private accountServce : AccountService) { }

  ngOnInit() {
    //throw new Error("");
    //this.getUsers2()
    this.setCurrentUser();
  }

  setCurrentUser(){
    const user : User = JSON.parse(localStorage.getItem("user"));
    this.accountServce.setCurrentUser(user)
  }

  // getUsers2(){
  //   this.http.get("https://localhost:5001/api/Users").subscribe({
  //     next : response => this.users = response,
  //     error : error => console.log(error)
  //   })
  // }

}

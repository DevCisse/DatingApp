import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  users : any = {};

  constructor(private http : HttpClient) { }

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }


  getUsers() {
    this.http.get("https://localhost:5001/api/Users").subscribe(res => {
      this.users = res;
    }, error => { console.log(error) })
  }

//4
  cancelRegisterMode(event : boolean){
    this.registerMode = false;
  }

}
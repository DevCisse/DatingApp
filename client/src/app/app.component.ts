import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'The Dating App';
  users: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    //throw new Error("");
    this.getUsers2()

  }

  getUsers() {
    this.http.get("https://localhost:5001/api/Users").subscribe(res => {
      this.users = res;
    }, error => { console.log(error) })
  }


  getUsers2(){
    this.http.get("https://localhost:5001/api/Users").subscribe({
      next : response => this.users = response,
      error : error => console.log(error)
    })
  }

}

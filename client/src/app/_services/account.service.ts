import { User } from '../_models/user';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {map} from 'rxjs/operators'
import { ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl

//we create an observable to store our user

  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }



  //we ar using map from rxjs
  //anything we put inside pipe will be an rxjx
  login(model : any){
    return this.http.post(this.baseUrl + 'account/login' , model ).pipe(
      map((response :User) => {
        const user = response;

        if(user){
          localStorage.setItem("user",JSON.stringify(user));
          this.currentUserSource.next(user)
        };

      })
    )
  }


  register(model :any){
   return this.http.post(this.baseUrl + 'account/register',model).pipe(
     map((user: User) =>{
       if(user){
         localStorage.setItem('user',JSON.stringify(user))
        this.currentUserSource.next(user)
       }
      // return user;
     })
   )
  }

  setCurrentUser(user : User){
    this.currentUserSource.next(user)

  }


  logout(){
    localStorage.removeItem("user");
    this.currentUserSource.next(null)
  }
}

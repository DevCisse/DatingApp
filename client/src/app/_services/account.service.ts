import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {map} from "rxjs/operators";
import { User } from '../_models/User';
import {ReplaySubject} from 'rxjs';
import { environment } from 'src/environments/environment';
import { PresenceService } from './presence.service.ts.service';
@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;

  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();


  constructor(private http: HttpClient,private presenceService : PresenceService) { }

  login(model : any){
    //return this.http.post(this.baseUrl + 'account/login',model);
    return this.http.post(this.baseUrl + 'account/login',model).pipe(
      map((resposne : User) => {
        const user = resposne;

        if(user){
          // localStorage.setItem("user",JSON.stringify(user));
          // this.currentUserSource.next(user);
          this.setCurrentUser(user);
        }
      })
    )
  }

  register(model : any){
    return this.http.post(this.baseUrl + 'account/register',model).pipe(
      map((user : User) =>{
        if(user){
          // localStorage.setItem("user",JSON.stringify(user));
          // this.currentUserSource.next(user);
          this.setCurrentUser(user);
        }
        return user;
      })
    )
  }


  setCurrentUser(user: User) {

    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);

    this.presenceService.createHubConnection(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);

    this.presenceService.stopHubConnection();
  }

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]))
  }
}

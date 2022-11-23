import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/Member';
import { map } from 'rxjs/operators';



@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;
  members : Member[] = [];
  constructor(private httpClient : HttpClient) { }


  // getMembers() : Observable<Member[]>{
  //   return this.httpClient.get<Member[]>(this.baseUrl + "users",httpOptions);
  // }

  getMembers() {
    if(this.members.length > 0) return of(this.members); //returnin observable because this is what we are expecting
    return this.httpClient.get<Member[]>(this.baseUrl + "users").pipe(
     map(members  =>{
      this.members = members;
      return members;
     })
    );
  }

  getMember(username : string) {
    const member = this.members.find(member => member.username === username);
    if(member !== undefined) return of(member);
    return this.httpClient.get<Member>(this.baseUrl + "users/" + username);
  }

  updateMember(member : Member){
    return this.httpClient.put(this.baseUrl + "users", member).pipe(
      //nothing is coming back from the api hence ()
      map(() =>{
        const index = this.members.indexOf(member);
        this.members[index] = member;

      })
    );
  }

  setMainPhoto(photoId: number) {
    return this.httpClient.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number) {
    return this.httpClient.delete(this.baseUrl + 'users/delete-photo/' + photoId);
  }
}

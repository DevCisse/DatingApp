import { AccountService } from 'src/app/_services/account.service';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/Member';
import { map, take } from 'rxjs/operators';
import { PaginatedResult } from '../_models/Pagination';
import { ReturnStatement } from '@angular/compiler';
import { UserParams } from '../_models/userParams';
import { User } from '../_models/User';
import { getPaginatedResult, getPaginationHeaders } from './paginatinonHelper';



@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;
  members : Member[] = [];

  memberCache  = new Map();
  userParams : UserParams | undefined
  user : User | undefined;

  constructor(private httpClient : HttpClient,private accountService : AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if(user){
          this.userParams = new UserParams(user);
          this.user = user
        }
      }
     })

   }
 // paginatedResult:PaginatedResult<Member[]> = new PaginatedResult<Member[]>();



  // getMembers() : Observable<Member[]>{
  //   return this.httpClient.get<Member[]>(this.baseUrl + "users",httpOptions);
  // }

  // getMembers() {
  //   if(this.members.length > 0) return of(this.members); //returnin observable because this is what we are expecting
  //   return this.httpClient.get<Member[]>(this.baseUrl + "users").pipe(
  //    map(members  =>{
  //     this.members = members;
  //     return members;
  //    })
  //   );
  // }

  getUserParams() {
    return this.userParams;
  }

  setUserParams(params: UserParams) {
    this.userParams = params;
  }



  resetUserParams() {
    if (this.user) {
      this.userParams = new UserParams(this.user);
      return this.userParams;
    }
    return;
  }

  getMembers(userParams : UserParams){
    const response = this.memberCache.get(Object.values(userParams).join('-'));

    if (response) return of(response);

    let params = getPaginationHeaders(userParams.pageNumber,userParams.pageSize);
      //to get the httpresponse



      params = params.append('minAge',userParams.minAge)
      params = params.append('maxAge',userParams.maxAge)
      params = params.append('gender',userParams.gender)
      params = params.append('orderBy',userParams.orderBy)

      //we need to observe the response
  return getPaginatedResult<Member[]>(this.baseUrl + 'users', params,this.httpClient).pipe(
      map(response => {
        this.memberCache.set(Object.values(userParams).join('-'), response);
        return response;
      })
    )
  }



  getMember(username : string) {
    const member = [...this.memberCache.values()]
    .reduce((arr, elem) => arr.concat(elem.result), [])
    .find((member: Member) => member.username === username);

  if (member) return of(member);
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

  addLike(username : string){
    return this.httpClient.post(this.baseUrl + 'likes/' + username, {});
  }


  getLikes(predicate:string, pageNumber:number, pageSize : number){

    let params = getPaginationHeaders(pageNumber,pageSize,);

    params = params.append('predicate',predicate);

    return getPaginatedResult<Member[]>(this.baseUrl + 'likes',params,this.httpClient);


    //return this.httpClient.get<Member[]>(this.baseUrl + 'likes?predicate=' + predicate);

  }

}

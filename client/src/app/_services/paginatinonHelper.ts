import { HttpClient, HttpParams } from "@angular/common/http";
import { map } from "rxjs/operators";
import { PaginatedResult } from "../_models/Pagination";

export function getPaginatedResult<T>(url:string,params: HttpParams, httpClient:HttpClient) {
  const paginatedResult :PaginatedResult<T> = new PaginatedResult<T>();
  return httpClient.get<T>(url , { observe: 'response', params }).pipe(
    map(response => {
      if (response.body) {
        paginatedResult.result = response.body;

        const pagination = response.headers.get("pagination");

        if (pagination) {
          paginatedResult.pagination = JSON.parse(pagination);
        }

        return paginatedResult;
      }
    })
  );
}

export function getPaginationHeaders(pageNumber:number,pageSize :number) {
  let params = new HttpParams(); //utility provide by angular provided this to add http headers


    params = params.append("pageNumber", pageNumber);
    params = params.append("pageSize", pageSize);

  return params;
}

import { HttpClient } from "@angular/common/http";
import { RegistrateUser } from "./registrationUser";
import { Observable, of } from "rxjs";
import { map, catchError } from "rxjs/operators";
import { Injectable } from "@angular/core";


@Injectable({
    providedIn: 'root',
  })
export class RegisterService {
    isLoggedIn = false;
  
    registerUrl: string =  'http://localhost:52295/StaGod/';
  
    constructor(private http: HttpClient) { }
  
    login(user: RegistrateUser): Observable<any> {
      return this.http.post<any>(this.registerUrl, `FirstName=`+ user.FirstName +`&LastName=`+ user.LastName, { 'headers': { 'Content-type': 'x-www-form-urlencoded' } }).pipe(
        map(res => {
console.log(this.registerUrl, `FirstName=`+ user.FirstName +`&LastName=`+ user.LastName);
        }),
  
        catchError(this.handleError<any>('login'))
      );
    }
  
 
    private handleError<T>(operation = 'operation', result?: T) {
      return (error: any): Observable<T> => {
        return of(result as T);
      };
    }
  }
  
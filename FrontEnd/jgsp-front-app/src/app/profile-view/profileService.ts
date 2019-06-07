import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable, of } from "rxjs";
import { map, catchError } from "rxjs/operators";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { RegistrateUser } from "../register/registrationUser";


@Injectable({
    providedIn: 'root',
  })
export class ProfileService {

  registerUrl: string =  'http://localhost:52295/api/Account/';
    constructor(private http: HttpClient,private route:Router) { }
  
    showProfile(email:string): Observable<any> {

        return this.http.get<RegistrateUser>(this.registerUrl+"UserInformation?email="+email)
        .pipe(
          catchError(this.handleError<any[]>('login'))
        );
    }


    update(user:RegistrateUser): Observable<string> {

      return this.http.put<string>(this.registerUrl+"UpdateUser",user,{ 'headers': { 'Content-type': 'application/json' }} ).pipe(
        map(res => {
        alert("Successfully update!");
        localStorage.email = user.Email;
        }),
        catchError(this.handleError<any>('login'))
      );
  }
  
 
    private handleError<T>(operation = 'operation', result?: T) {
      return (error: any): Observable<T> => {
        alert("Can't update at this time!");
        return of(result as T);
      };
    }
  }
  
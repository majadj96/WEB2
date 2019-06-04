import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Price } from "./price";

@Injectable({
    providedIn: 'root',
  })
export class PriceService {
  
    registerUrl: string =  'http://localhost:52295/api/Price/';
  
    constructor(private http: HttpClient) { }
  
    showPrices(ticket:string,user:string) : Observable<number> {
        return this.http.get<number>(this.registerUrl+"GetOnePrice?ticket="+ticket+"&user="+user)
          .pipe(
            //catchError(this.handleError<Hero[]>('getHeroes', []))
          );
      }
  }
  
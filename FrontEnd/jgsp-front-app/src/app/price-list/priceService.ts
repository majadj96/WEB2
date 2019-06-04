import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Ticket } from "./price";

@Injectable({
    providedIn: 'root',
  })
export class PriceService {
  
    registerUrl: string =  'http://localhost:52295/api/';
  
    constructor(private http: HttpClient) { }
  
    showPrices(ticket:string,user:string) : Observable<number> {
        return this.http.get<number>(this.registerUrl+"Price/GetOnePrice?ticket="+ticket+"&user="+user)
          .pipe(
            //catchError(this.handleError<Hero[]>('getHeroes', []))
          );
      }

      buyOneHour(ticket:Ticket){
       
        return this.http.post<any>(this.registerUrl+"Tickets/Buy?TypeOfTicket="+ ticket.TypeOfTicket +"&UserName="+ ticket.UserName, { 'headers': { 'Content-type': 'x-www-form-urlencoded' } }).pipe(
          map(res => {
          }),
          //catchError(this.handleError<any>('login'))
        );
      }
  }
  
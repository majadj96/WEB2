import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ScheduleLine } from '../models/ScheduleLine';

@Injectable({
  providedIn: 'root'
})
export class ScheduleAdminService {

  private baseUrl: string;
  constructor(private client: HttpClient) {
    this.baseUrl = "http://localhost:52295/";
   }

   public getSchedule() : Promise<ScheduleLine[]>{
    return this.client.get<ScheduleLine[]>(this.baseUrl+"api/Line/GetScheduleAdmin").toPromise<ScheduleLine[]>();
 
   }
}

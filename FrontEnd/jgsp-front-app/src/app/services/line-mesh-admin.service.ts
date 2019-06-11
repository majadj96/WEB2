import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Line } from '../models/Line';

@Injectable({
  providedIn: 'root'
})
export class LineMeshAdminService {

  private baseUrl: string;

  constructor(private client: HttpClient) {
    this.baseUrl = "http://localhost:52295/";
   }


   public getLines() : Promise<Line[]>{
    return this.client.get<Line[]>(this.baseUrl+"api/Line/GetLines").toPromise<Line[]>();
    
   }

   public getStations() : Promise<string[]>{
    return this.client.get<string[]>(this.baseUrl+"api/Station/GetStations").toPromise<string[]>();
    
   }

   public addLine(line :Line){
    return this.client.post<any>(this.baseUrl+"api/Line/AddLine",line, {'headers': {'Content-type': 'application/json'}});
  }

  public editLine(line :Line){
    return this.client.post<any>(this.baseUrl+"api/Line/EditLine",line, {'headers': {'Content-type': 'application/json'}});
  }

  public deleteLine(line :Line){

    return this.client.delete(`http://localhost:52295/api/Line/DeleteLine/${line.Number}`);
  
    }
}

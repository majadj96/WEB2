import { Component, OnInit, Input, NgZone } from '@angular/core';
import { MarkerInfo } from '../admin-station/map/model/marker-info.model';
import { GeoLocation } from '../admin-station/map/model/geolocation';
import { Polyline } from '../admin-station/map/model/polyline';
import { Line } from '../models/Line';
import { ScheduleAdminService } from '../services/schedule-admin.service';


@Component({
  selector: 'app-line-mesh',
  templateUrl: './line-mesh.component.html',
  styleUrls: ['./line-mesh.component.css'],
  styles: ['agm-map {height: 500px; width: 700px;}'] //postavljamo sirinu i visinu mape
})
export class LineMeshComponent implements OnInit {

  markerInfo: MarkerInfo;
  public polyline: Polyline;
  public zoom: number;
  linesAll: Line[];


  public lines = [];

lala(a){
  alert(a);
}



  async ngOnInit() {
    this.markerInfo = new MarkerInfo(new GeoLocation(45.242268, 19.842954), 
      "assets/ftn.png",
      "Jugodrvo" , "" , "http://ftn.uns.ac.rs/691618389/fakultet-tehnickih-nauka");

      this.polyline = new Polyline([], 'blue', { url:"assets/busicon.png", scaledSize: {width: 50, height: 50}});
      this.linesAll = await this.scheduleAdminService.getLines();

      this.linesAll.forEach(v=>this.lines.push(v.Number));
    }

  constructor(private ngZone: NgZone,private scheduleAdminService: ScheduleAdminService){

  }

  placeMarker($event){
    this.polyline.addLocation(new GeoLocation($event.coords.lat, $event.coords.lng))
    console.log(this.polyline)
  }

}

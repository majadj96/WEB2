import { Component, OnInit, Input, NgZone } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { MarkerInfo } from './model/marker-info.model';
import { GeoLocation } from './model/geolocation';
import { Polyline } from './model/polyline';
import { MapsAPILoader } from '@agm/core';
import { GoogleMapsAPIWrapper } from '@agm/core';
import { google } from '@agm/core/services/google-maps-types';
import { Station } from './model/station';
import { MapService } from './mapService';



@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
  styles: ['agm-map {height: 500px; width: 700px;}'] //postavljamo sirinu i visinu mape
})
export class MapComponent implements OnInit {

  markerInfo: MarkerInfo;
  public polyline: Polyline;
  public zoom: number;
  public stationObj:Station;
  public message:string;
  public stations:Station[];
  public isLoaded:boolean;
  public canEdit:boolean;
  public statin:Station;
public name:string;

  station = this.fb.group({
    Name: ['', Validators.required],
    Address: ['', Validators.required],
  });

  stationForm = this.fb.group({
    Address: ['', Validators.required],
    Latitude: ['', Validators.required],
    Longitude: ['', Validators.required]
  });

  ngOnInit() {
    this.markerInfo = new MarkerInfo(new GeoLocation(45.242268, 19.842954), 
      "assets/ftn.png",
      "Jugodrvo" , "" , "http://ftn.uns.ac.rs/691618389/fakultet-tehnickih-nauka");

      this.polyline = new Polyline([], 'pink', { url:"assets/busicon.png", scaledSize: {width: 50, height: 50}});
  }

  constructor(private ngZone: NgZone,private fb: FormBuilder,public mapService: MapService){
    this.isLoaded=false;
    this.canEdit=false;
    this.getStations();
  }


  placeMarker($event){
    if(this.canEdit){
      this.stationForm.controls['Latitude'].setValue($event.coords.lat);
      this.stationForm.controls['Longitude'].setValue($event.coords.lng);
    }
    this.polyline.removeLocation();
    this.polyline.addLocation(new GeoLocation($event.coords.lat, $event.coords.lng))
    alert($event.coords.lat+"   "+$event.coords.lng);   
    this.stationObj = new Station();
    this.stationObj.Latitude = $event.coords.lat;
    this.stationObj.Longitude = $event.coords.lng;
    //pozovi servis za stanicu 
  }

  addStation():void{
    this.isLoaded=true;
    this.stationObj.Name = this.station.controls['Name'].value;
    this.stationObj.Address = this.station.controls['Address'].value;
    this.station.controls['Address'].setValue("");
    this.station.controls['Name'].setValue("");
    this.mapService.add(this.stationObj).subscribe(ok=>{
    this.message=ok;
    this.getStations();
    this.canEdit=false;
    })
  }


  getStations():void{
    this.mapService.getStations().subscribe(stations=>{
     this.stations = stations;
     this.isLoaded=true;
    }); 
   }


   Update(){
    this.statin.Address =this.stationForm.controls['Address'].value;
    this.statin.Latitude =this.stationForm.controls['Latitude'].value;
    this.statin.Longitude =this.stationForm.controls['Longitude'].value;
      alert(this.statin.Name);
    this.mapService.update(this.statin).subscribe(ok=>{
     console.log(ok);
     this.getStations();
     this.canEdit=false;
   });

   }

   Edit(name){
    this.name=name;
    this.stations.forEach(elem => {
      if(elem.Name==name){
        this.statin=elem;
      }
    });
    alert(this.statin.Address);

    this.stationForm.controls['Address'].setValue(this.statin.Address);
    this.stationForm.controls['Latitude'].setValue(this.statin.Latitude);
    this.stationForm.controls['Longitude'].setValue(this.statin.Longitude);
    this.canEdit=true;

   }

   Delete(name){
     this.canEdit=false;
     this.mapService.delete(name).subscribe(data=>
      {console.log(data);
      this.getStations();
      if(this.stations.length==0){
        this.isLoaded=false;
      }
     }
      );
   }


}

import { Component, OnInit } from '@angular/core';
import { PriceService } from './priceService';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { Ticket } from './price';
import { ProfileService } from '../profile-view/profileService';
import { RegistrateUser } from '../register/registrationUser';

@Component({
  selector: 'app-price-list',
  templateUrl: './price-list.component.html',
  styleUrls: ['./price-list.component.css']
})
export class PriceListComponent implements OnInit {
public isPriceDataLoaded: boolean;
public isOneHour: boolean;

message:string;
selectedTicket;
selectedUser;
email;
isLogged:boolean;
prices:number;
tickets : Ticket[];
t:Ticket;
user:RegistrateUser;
  dataTicket:Array<Object> = [
    {name: "One-hour"},
    {name: "Day"},
    {name: "Mounth"},
    {name: "Year"}
];

dataUser:Array<Object> = [
  {name: "Student"},
  {name: "Pensioner"},
  {name: "Regular"}
];
dat:Date;
ticket:Ticket={IDticket:1, BoughtTime:this.dat,CheckIn:this.dat,TypeOfTicket:1,UserName:"guest"};

role;

emailForm = this.fb.group({
  email: ['', Validators.email],
});

  constructor(public priceService: PriceService,private fb: FormBuilder, public profileService : ProfileService) {
    this.isPriceDataLoaded=false;
    this.isOneHour=false;
    alert(localStorage.email);
    if(localStorage.email!=undefined){
    this.isLogged=true;
    this.getUser();
    this.showTickets();
    }else{
      this.isLogged=false;
    }
    this.message="";



  }

  ngOnInit() {
  }

  showTickets():void{
   this.priceService.showTickets().subscribe(tickets=>{
    this.tickets = tickets;
   }) 
  }
  

  showPrices(logged):void{
    if(!logged){
    this.priceService.showPrices(this.selectedTicket.name,this.selectedUser.name).subscribe(prices=>{
      this.prices=prices;
      this.isPriceDataLoaded=true;
    });
  }else{
    this.priceService.showPrice(this.selectedTicket.name,localStorage.email).subscribe(prices=>{
      this.prices=prices;
      this.isPriceDataLoaded=true;
    });
  }
  }

  selected1(){
    alert(this.selectedTicket.name+" "+this.selectedUser.name);
  }


  checkIn(id){
    this.t = new Ticket();
    this.t.IDticket=id;
    this.priceService.checkIn(this.t).subscribe((date)=>{
      this.showTickets();

    });
  }

  getUser(){
    this.profileService.showProfile(localStorage.email).subscribe(regUser=>{
      this.user=regUser;
    });
  }
  buyTicket(){
    this.role = localStorage.role;

    if(this.role==null){
      this.message = "Niste se registrovali mozete kupiti samo vremensku kartu!";
      this.isOneHour=true;
    }
    else{
      alert("registrovani ste"+this.role);
      //ukoliko je profil nevalidan - poz
      alert(this.user.VerificationStatus);

      if(this.user.VerificationStatus=="Valid"){
        alert("Validan profil");
      this.ticket.TypeOfTicket=this.selectedTicket.name;
      this.priceService.buyTicket(this.ticket).subscribe((data) => {
        console.log(data);
        this.showTickets();
      });
      }else{
        alert("Kontroler nije verifikovao Vas profil.");
      }
    }

  }

  buyOneHour(){
    alert(this.email);
    this.message="";
    this.ticket.UserName=this.email;
    this.priceService.buyOneHour(this.ticket).subscribe((data) => {
      console.log(data);
    });
  }

lala(){
  alert(this.prices);
}

}

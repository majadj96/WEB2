import { Component, OnInit } from '@angular/core';
import { PriceService } from './priceService';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { Ticket } from './price';

@Component({
  selector: 'app-price-list',
  templateUrl: './price-list.component.html',
  styleUrls: ['./price-list.component.css']
})
export class PriceListComponent implements OnInit {
public isPriceDataLoaded: boolean;
selectedTicket;
selectedUser;
email;
prices:number;

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
prikaz;

  constructor(public priceService: PriceService) {
    this.isPriceDataLoaded=false;
  }

  ngOnInit() {
  }

  showPrices():void{
    this.priceService.showPrices(this.selectedTicket.name,this.selectedUser.name).subscribe(prices=>{
      this.prices=prices;
      this.isPriceDataLoaded=true;
    });
  }

  selected1(){
    alert(this.selectedTicket.name+" "+this.selectedUser.name);
  }

  buyTicket(){
    this.role = localStorage.role;

    if(this.role==null){
      alert("Niste se registrovali mozete kupiti samo vremensku kartu!");
      this.prikaz="ok";
    }
    else{
      alert("registrovani ste"+this.role);
    }

  }

  buyOneHour(){
    alert(this.email);
    this.ticket.UserName=this.email;
    this.priceService.buyOneHour(this.ticket).subscribe((data) => {
      console.log(data);
    });
  }

lala(){
  alert(this.prices);
}

}

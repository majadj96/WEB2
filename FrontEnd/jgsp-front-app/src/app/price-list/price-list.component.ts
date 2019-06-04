import { Component, OnInit } from '@angular/core';
import { PriceService } from './priceService';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { Price } from './price';

@Component({
  selector: 'app-price-list',
  templateUrl: './price-list.component.html',
  styleUrls: ['./price-list.component.css']
})
export class PriceListComponent implements OnInit {

selectedTicket;
selectedUser;
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


  constructor(public priceService: PriceService) {
  }

  ngOnInit() {
  }

  showPrices():void{
    this.priceService.showPrices(this.selectedTicket.name,this.selectedUser.name).subscribe(prices=>this.prices=prices);
  }

  selected1(){
    alert(this.selectedTicket.name+" "+this.selectedUser.name);
  }
lala(){
  alert(this.prices);
}

}

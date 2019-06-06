import { Component, OnInit } from '@angular/core';
import { ScheduleLine } from '../../models/ScheduleLine';
import { Router } from '@angular/router';
import { ScheduleAdminService } from '../../services/schedule-admin.service';
import { Line } from 'src/app/models/Line';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-schedule-admin',
  templateUrl: './schedule-admin.component.html',
  styleUrls: ['./schedule-admin.component.css']
})
export class ScheduleAdminComponent implements OnInit {
  public isBtnAddClicked: boolean;
  schedule: ScheduleLine[];
  lines: Line[];
sl : ScheduleLine;
scheduleLine: ScheduleLine;

  public addForm: FormGroup;
  TypeDay:Array<Object> = [
    {name: "Work day"},
    {name: "Weekend"},
  
  ];

  constructor(private fb: FormBuilder,private scheduleAdminService: ScheduleAdminService) {
    this.addForm = this.fb.group({
      line: [''],
      day: [''],
      time: ['']

    });

    this.isBtnAddClicked = false;
    this.schedule = new Array<ScheduleLine>();
    this.sl = new ScheduleLine();
    
   }

  async ngOnInit() {
    this.schedule = await this.scheduleAdminService.getSchedule();
    
    
  }

  public async addScheduleLine(){
    this.isBtnAddClicked = true;
    this.lines = await this.scheduleAdminService.getLines();
  }

  public async onSubmit(){

    
    this.sl.Number = this.addForm.controls['line'].value;
    this.sl.Day= this.addForm.controls['day'].value;
    alert(this.sl.Day);
    this.sl.Time = this.addForm.controls['time'].value;
    

      this.scheduleAdminService.postSchedule(this.sl).subscribe(()=>{
        this.getSchedule();
        console.log("dasda");
      }, err => console.log(err));


    this.isBtnAddClicked = false;

   // this.schedule = await this.scheduleAdminService.getSchedule();
  }

  public async getSchedule(){
    this.schedule = await this.scheduleAdminService.getSchedule();
  }

  public  deleteLine(scheduleLine){
    alert("delete "+ scheduleLine.Number);
    this.scheduleAdminService.deleteLine(scheduleLine);
    alert("sta sad?");
  }


}

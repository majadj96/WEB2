import { Component, OnInit } from '@angular/core';
import { ScheduleLine } from '../../models/ScheduleLine';
import { Router } from '@angular/router';
import { ScheduleAdminService } from '../../services/schedule-admin.service';

@Component({
  selector: 'app-schedule-admin',
  templateUrl: './schedule-admin.component.html',
  styleUrls: ['./schedule-admin.component.css']
})
export class ScheduleAdminComponent implements OnInit {

  schedule: ScheduleLine[];

  constructor(private scheduleAdminService: ScheduleAdminService) { }

  async ngOnInit() {
    this.schedule = await this.scheduleAdminService.getSchedule();
  }


}

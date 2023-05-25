import {Component, OnInit} from '@angular/core';
import {MachineStateService} from "../../shared/services/machine-state.service";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  constructor() {
  }

  ngOnInit(): void {
  }

}

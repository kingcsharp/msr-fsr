import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../../../services/signalr.service';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-wipstatus',
  templateUrl: './wipstatus.component.html',
  styleUrls: ['./wipstatus.component.scss']
})
export class WipstatusComponent implements OnInit {

  constructor(private signalrService: SignalRService, private globalService: Globals) { }

  ngOnInit(): void {
    this.signalrService.subscribeWorkOrderUpdate();
  }


}

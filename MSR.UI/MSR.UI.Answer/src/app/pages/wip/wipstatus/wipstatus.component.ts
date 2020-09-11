import { Component, OnInit } from '@angular/core';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-wipstatus',
  templateUrl: './wipstatus.component.html',
  styleUrls: ['./wipstatus.component.scss']
})
export class WipstatusComponent implements OnInit {

  constructor(public globals: Globals) { }

  ngOnInit(): void {


  }


}
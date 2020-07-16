import { Component, OnInit } from '@angular/core';
import { HelpService } from '../../../services/api.client.generated';

@Component({
  selector: 'app-help',
  templateUrl: './help.component.html',
  styleUrls: ['./help.component.scss'],
  providers:[HelpService]
})
export class HelpComponent implements OnInit {

  constructor(private helpService: HelpService) { }

  ngOnInit(): void {
    //this.helpService.
  }

}

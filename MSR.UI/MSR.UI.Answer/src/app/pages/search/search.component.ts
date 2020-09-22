import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-search-component',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute) {
    this.activatedRoute.queryParams.subscribe(params => {
      let value_1 = params['search'];
    });
  }

  ngOnInit(): void {
    // let searchTerm = this.route.snapshot.queryParams["search"];
    // console.log(searchTerm);
  }

  

}

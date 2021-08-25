import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { HttpService } from '../../services/base.service';

@Component({
  selector: 'app-sample1',
  templateUrl: './sample1.component.html',
  styleUrls: ['./sample1.component.css']
})
export class Sample1Component implements OnInit {

  constructor(private service: HttpService, private route: ActivatedRoute, private router: Router) {


  }
  books: any = {
    data: []
  };
  query: any = {};
  page: number = 1; 
  size: number = 3;

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.page = params.page ?? 1;
      this.query = {
        from: this.size * (params.page - 1),
        size: this.size,
        query: {
          match_all: {}
        }
      };
      this.loadData();
    });

  }

  loadData() {
    this.service.Post("/book/api/BookManagement/search", { Query: JSON.stringify(this.query) }).subscribe(result => {
      this.books = result;
    });
  }

  pageChanged(page: number): void {

    const queryParams: Params = { page: page };

    this.router.navigate(
      [],
      {
        relativeTo: this.route,
        queryParams: queryParams,
        queryParamsHandling: 'merge', 
      }); 
  }



}

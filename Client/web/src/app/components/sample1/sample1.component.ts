import { Component, OnInit } from '@angular/core';
import { HttpService } from '../../services/base.service'; 

@Component({
  selector: 'app-sample1',
  templateUrl: './sample1.component.html',
  styleUrls: ['./sample1.component.css']
})
export class Sample1Component implements OnInit {

  constructor(private service: HttpService) { }
  books: any = {
    data: []
  };
  query: any = {
    from: 0,
    size: 2,
    query: {
      match_all: {}
    }
  };

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.service.Post("/book/api/BookManagement/search", { Query: JSON.stringify(this.query)}).subscribe(result => {
      this.books = result;
    });   
  }   

  pageChanged(page: number): void {
    this.query.from = this.query.size * (page - 1);
    this.loadData();
  }
  
}

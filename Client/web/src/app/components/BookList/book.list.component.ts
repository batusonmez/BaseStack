import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { HttpService } from '../../services/base.service';
import { Column } from '../data-table/Models/Column';

@Component({
  selector: 'book-list',
  templateUrl: './book.list.component.html',
  styleUrls: ['./book.list.component.css']
})
export class BookListComponent implements OnInit {

  constructor(private service: HttpService, private route: ActivatedRoute, private router: Router) {


  }
  books: any = {
    data: []
  };
  query: any = {};
  page: number = 1; 
  size: number = 3;

  columns: Column[] = [
    new Column("title", "Name", "Title"),
    new Column("description", "Description", "Description"),
    new Column("EditLink")
  ]

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {      
      this.page = params.page ?? 1;
      this.query = {
        from: this.size * (this.page - 1),
        size: this.size,
        query: {
          match_all: {}
        }
      };
      if (params.term) {
        var filterColumns = this.columns.filter(d => d.FilterParameter.length > 0).map(d => d.FilterParameter);
        this.query.query = {
          multi_match: {
            query: params.term,
            fields: filterColumns
          }
        }
      }

      this.loadData();
    });

  }

  loadData() {
    this.service.Post("/book/api/BookManagement/search", { Query: JSON.stringify(this.query) }).subscribe(result => {
      this.books = result;
      for (var i = 0; i < this.books.data.length; i++) {
        var book = this.books.data[i];
        debugger
       
        book.EditLink = "<a class=\"btn btn-light\" href=\"" + location.pathname+"/edit/"+ book.id + "\">Edit</a>";
        
      }
    });
  }

  navigate(query: any): void{
    const queryParams: Params = query;

    this.router.navigate(
      [],
      {
        relativeTo: this.route,
        queryParams: queryParams,
        queryParamsHandling: 'merge',
      });
  }

  pageChanged(page: number): void {
    this.navigate( { page: page });     
  }


  search(term: string) {
    this.navigate({ term: term,page:1 });
  }


}

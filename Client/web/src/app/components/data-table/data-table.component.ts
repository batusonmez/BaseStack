import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Column } from './Models/Column';

@Component({
  selector: 'data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.css']
})
export class DataTableComponent implements OnInit {

  @Input() Columns: Column[] = [];
  @Input() Data: any[] = [];
  @Input() Pageize: number = 2;
  @Input() Total: number = 0;
  @Input() Page: number = 1;

  @Output() PageChange = new EventEmitter<number>();
  ngOnInit(): void {

  }

  get PageCount(): number {
    if (!this.Total || !this.Pageize) { return 1; }

    return Math.ceil(this.Total / this.Pageize);
  }

  pageChanged(): void {
    this.PageChange.emit(this.Page);
  }

}

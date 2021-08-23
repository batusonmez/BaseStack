import { Component, Input, OnInit } from '@angular/core';
import { Column } from './Models/Column';

@Component({
  selector: 'data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.css']
})
export class DataTableComponent implements OnInit {

  @Input() Columns: Column[] = [];
  @Input() Data: any[] = [];

  ngOnInit(): void {



  }



}

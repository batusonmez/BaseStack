import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core'; 
import { CellComponent } from '../cell/cell.component';
import { DataTableConfig } from '../Models/DataTableConfig';

@Component({
  standalone: true,
  selector: 'table-host',
  templateUrl: './table-host.component.html',
  styleUrls: ['./table-host.component.scss'],
  
  imports: [CommonModule, CellComponent]
})
export class TableHostComponent implements OnInit{
  @Input() public Config!: DataTableConfig;
   
  constructor() {

  }

  ngOnInit() {
    if (!this.Config.Class) {
      this.Config.Class="table"
    }    
  }

  

}

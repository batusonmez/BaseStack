import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core'; 
import { CellConfig } from '../Models/CellConfig';

@Component({
  standalone: true,
  selector: 'cell',
  templateUrl: './cell.component.html',
  styleUrls: ['./cell.component.scss'],
  imports: [CommonModule]
})
export class CellComponent implements OnInit{
  @Input() public Config!: CellConfig;
  @Input() public Data: any;
  BindData: string="";
  constructor() {

  }

  ngOnInit() {
    this.BindData = this.Data[this.Config.Key];
 
  }
 

}

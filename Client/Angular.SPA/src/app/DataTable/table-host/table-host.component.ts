import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core'; 
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CellComponent } from '../cell/cell.component';
import { DataTableConfig } from '../Models/DataTableConfig';

@Component({
  standalone: true,
  selector: 'table-host',
  templateUrl: './table-host.component.html',
  styleUrls: ['./table-host.component.scss'],
  
  imports: [CommonModule, CellComponent,RouterModule ]
})
export class TableHostComponent implements OnInit{
  @Input() public Config!: DataTableConfig;
   
  constructor(private route: ActivatedRoute) {

  }

  ngOnInit() {
    if (!this.Config.Class) {
      this.Config.Class="table"
    }    

    this.route.queryParamMap.subscribe(params => {
      if(this.Config.Pager){
        this.Config.Pager.Page=Number.parseInt(params.get('page')??"1");
      }  
    });
  }

  Counter(i:number){
    return new Array(i);
  }
  

}

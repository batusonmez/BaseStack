import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute, ParamMap, RouterModule } from '@angular/router';
import { CellComponent } from '../cell/cell.component';
import { DataTableConfig } from '../Models/DataTableConfig';
import { KeyValuePair } from '../Models/KeyValue';

@Component({
  standalone: true,
  selector: 'table-host',
  templateUrl: './table-host.component.html',
  styleUrls: ['./table-host.component.scss'],
  imports: [CommonModule, CellComponent, RouterModule]
})
export class TableHostComponent implements OnInit {
  @Input() public Config!: DataTableConfig;
 
  private query: string = "_";
  private queryKeys: KeyValuePair[] = [];
  public currentPage:number=0;
  constructor(private route: ActivatedRoute) {

  }

  ngOnInit() {    
    this.Config.Class =this.Config.Class?? "table"         
    this.buildQueryKeys();
    this.route.queryParamMap.subscribe(params => {
      this.setFilters(params);
      if(this.Config.Pager){
        this.currentPage= Number.parseInt(params.get('page')??"0");
      }
    });

  }

  setFilters(params: ParamMap): void {
    let query: string = this.buildQuery(params);
 
    if (query != this.query) {
      this.query = query;
      this.Config.OnFilter(this.query);
    }
  }
 
  buildQuery(params: ParamMap): string {
    let query: string = ""
    for (let index = 0; index < this.queryKeys.length; index++) {
      let kv = this.queryKeys[0];
      kv.value = params.get(kv.key) ?? "";
    }   
    query = this.queryKeys.map(d=>{
      return d.value?d.key+"="+d.value:""
    }).join("&");
    return query;
  }

  buildQueryKeys(): void {
    this.queryKeys = [];
    if (this.Config.Pager) {
      this.queryKeys.push({ key: 'page' });
    }
  }

  Counter(i: number) {
    return new Array(i);
  }
 
}

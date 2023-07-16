import { Component, Input } from '@angular/core'; 
import { BaseFormControl } from '../BaseFormControl'; 

@Component({
  selector: 'app-datalist',
  templateUrl: './datalist.component.html',
  styleUrls: ['./datalist.component.scss']
})
export class DataListComponent extends BaseFormControl { 
  @Input() public TempInput: string = "";
  
  SetQuery(event:any):void{
    if(this.Config?.Event){
      this.Config.Event("query",event.target.value??"");
    }    
  }
  
}

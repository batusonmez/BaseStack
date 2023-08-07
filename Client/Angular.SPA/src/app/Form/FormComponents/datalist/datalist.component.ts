import { Component, Input } from '@angular/core';
import { BaseFormControl } from '../BaseFormControl';
import { DataListOption } from './datalist.options';

@Component({
  selector: 'app-datalist',
  templateUrl: './datalist.component.html',
  styleUrls: ['./datalist.component.scss']
})
export class DataListComponent extends BaseFormControl {
  public selectionValue: any = "";

  SetQuery(event: any): void {
    if (this.Config?.Event) {
      this.Config.Event("query", event.target.value ?? "");
    }
  }

  Clear(): void {

    setTimeout(() => {
      if (this.Config?.ComponentData?.Options) {
        this.Config.ComponentData.Options = [];
      }
    }, 100);
  }

  SetSelection(option: DataListOption): void {
    if (this.Config?.ComponentData){
      this.Config.ComponentData.Selection=option;
      this.selectionValue=option.Value;
    }
    if (this.Config?.Event) {
      this.Config.Event("optionSelect", option);
    } 
  }

}

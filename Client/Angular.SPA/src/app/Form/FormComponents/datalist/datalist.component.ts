import { Component, Input, OnInit } from '@angular/core';
import { BaseFormControl } from '../BaseFormControl';
import { DataListOption } from './datalist.options'; 
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  standalone:true,
  selector: 'app-datalist',
  templateUrl: './datalist.component.html',
  styleUrls: ['./datalist.component.scss'],
  imports:[FormsModule, ReactiveFormsModule,CommonModule ]
})
export class DataListComponent extends BaseFormControl implements OnInit  {
  public selectionValue: any = "";

  ngOnInit() {
    this.ClearSelection();
  }

  
  SetQuery(event: any): void {
    this.ClearSelection();
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

  ClearSelection():void{
    if(!this.Config.ComponentData.Selection){
      this.Config.ComponentData.Selection={}
    }
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

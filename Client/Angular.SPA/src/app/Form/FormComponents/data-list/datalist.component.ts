import { Component, Input, OnInit } from '@angular/core';
import { BaseFormControl } from '../BaseFormControl';
import { DataListOption } from './datalist.options';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ValidationMessageComponent } from '../../validation-message/validation-message.component';

@Component({
  standalone: true,
  selector: 'app-datalist',
  templateUrl: './datalist.component.html',
  styleUrls: ['./datalist.component.scss'],
  imports: [FormsModule, ReactiveFormsModule, CommonModule, ValidationMessageComponent]
})
export class DataListComponent extends BaseFormControl implements OnInit {
  
  public AcBoxController: FormControl = new FormControl('');
    ngOnInit() {    
      this.RegisterForm();
    this.ClearSelection();
    
  }


  public override RegisterForm(): void {
    super.RegisterForm();
    if(this.Config.ComponentData?.LabelFor){
      this.Host?.Form.addControl(this.Config.ComponentData?.LabelFor, this.AcBoxController);
    }    
  }

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

  ClearSelection(): void {
    this.SetValues(undefined,undefined)     ;
  }

  SetSelection(option: DataListOption): void {
 
      this.SetValues(option.Label,option.Value)         
 
    if (this.Config?.Event) {
      this.Config.Event("optionSelect", option);
    }
    this.Clear();
  }

  SetValues(label?:string,value?:string):void{
    this.AcBoxController.patchValue(label);
    this.Controller.patchValue(value)    
  }

}

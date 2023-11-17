import { Component, Input, OnInit } from '@angular/core';
import { BaseFormControl } from '../BaseFormControl';
import { DataListOption } from './datalist.options';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ValidationMessageComponent } from '../../validation-message/validation-message.component';
import { DataListConfig } from './datalist.config';

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
    if (this.DataListConfig?.LabelFor) {
      this.Host?.Form.addControl(this.DataListConfig?.LabelFor, this.AcBoxController);
    }
  }

  get DataListConfig(): DataListConfig | null {

    if (!this.Config?.ComponentData) {
      return null
    }

    return this.Config.ComponentData;
  }

  SetQuery(event: any): void {
    if (this.Config?.Event) {
      this.Config.Event("query", event.target.value ?? "");
    }
  }

  Clear(): void {

    setTimeout(() => {
      if (this.DataListConfig?.Options) {
        this.DataListConfig.Options = [];
      }
    }, 100);
  }

  ClearSelection(): void {
    this.SetValues(undefined, undefined);
  }

  SetSelection(option: DataListOption): void {

    if (this.DataListConfig?.Multiple) {
      this.SetValuesMultiple(option)
    } else {
      this.SetValues(option.Label, option.Value)
    }


    if (this.Config?.Event) {
      this.Config.Event("optionSelect", option);
    }
    this.Clear();
  }

  SetValues(label?: string, value?: string): void {
    this.AcBoxController.patchValue(label);
    this.Controller.patchValue(value);
  }

  SetValuesMultiple(option: DataListOption): void {
    this.AcBoxController.patchValue("");
    this.DataListConfig?.SelectionMultiple?.push(option);
    this.BindMultipleSelection(option);
  }

  RemoveMultipleSelection(option: any): void {
    let selections: any[] = this.Controller.value;
    selections = selections.filter(d => d != option);
    this.Controller.patchValue(selections);
  }

  BindMultipleSelection(option: DataListOption): void {
    if (!this.Controller.value) {
      this.Controller.patchValue([]);
    }
    let selections: any[] = this.Controller.value;
    let sc: any = {};
    if (this.DataListConfig) {
      sc[this.DataListConfig.ValueFor ?? "_unsetValueProp"] = option.Value
      sc[this.DataListConfig.LabelFor] = option.Label;
      selections.push(sc);
    }
    this.Controller.patchValue(selections);
  }

}

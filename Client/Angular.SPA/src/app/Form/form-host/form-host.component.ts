import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import {   FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FieldHostComponent } from '../field-host/field-host.component';
import { FormConfig } from '../Models/FormConfig';
import { IFormHost } from '../Models/IFormHost';
import { FormHostEvents } from './FormHostEvents';

@Component({
  standalone: true,
  selector: 'form-host',
  templateUrl: './form-host.component.html',
  styleUrls: ['./form-host.component.scss'],
  imports: [FieldHostComponent, CommonModule, ReactiveFormsModule,]
})
export class FormHostComponent implements OnInit, IFormHost {
  @Input() public Config!: FormConfig;
  @Input()  public set Value(val:any){
    if(val){
      this.Form.patchValue(val);
    }
  };
  public Form: FormGroup = new FormGroup({   }); 
  constructor() {

  }
  ngOnInit() {
    this.buildForm();
 
  } 

  public buildForm(): void {
    this.Form = new FormGroup({});
  
  }
  onSubmit() {
    this.Config.FormEvent(FormHostEvents.SUBMIT, this);    
  }
}

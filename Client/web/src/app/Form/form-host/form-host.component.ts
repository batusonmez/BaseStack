import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import {   FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormBuilderService } from '../Core/FormBuilderService';
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
  public Form: FormGroup = new FormGroup({   }); 
  constructor(private formBuilder: FormBuilderService) {

  }
  ngOnInit() {
    this.buildForm();
 
  }
 

  public buildForm(): void {
    this.Form = this.formBuilder.BuildForm(this); 
  
  }
  onSubmit() {
    this.Config.FormEvent(FormHostEvents.SUBMIT, this);    
  }
}

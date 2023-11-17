import { Component, Input, OnInit } from '@angular/core'; 
import { BaseFormControl } from '../BaseFormControl';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ValidationMessageComponent } from "../../validation-message/validation-message.component";

@Component({
    standalone: true,
    selector: 'app-select-input',
    templateUrl: './select-input.component.html',
    styleUrls: ['./select-input.component.scss'],
    imports: [FormsModule, ReactiveFormsModule, CommonModule, ValidationMessageComponent]
})
export class SelectInputComponent extends BaseFormControl  implements OnInit{
  ngOnInit(): void {
    this.RegisterForm();
  }

   ngAfterViewInit(): void {      
    this.Controller.setValue(false);
  }
    
}

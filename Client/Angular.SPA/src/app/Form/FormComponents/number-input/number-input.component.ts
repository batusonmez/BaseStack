import { Component, Input, OnInit } from '@angular/core'; 
import { BaseFormControl } from '../BaseFormControl';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ValidationMessageComponent } from '../../validation-message/validation-message.component';

@Component({
  standalone: true,
  selector: 'app-number-input',
  templateUrl: './number-input.component.html',
  styleUrls: ['./number-input.component.scss'],
  imports: [FormsModule, ReactiveFormsModule, CommonModule, ValidationMessageComponent]
})
export class NumberInputComponent extends BaseFormControl implements OnInit{
  ngOnInit(): void {
    this.RegisterForm();
  }

  
 
}

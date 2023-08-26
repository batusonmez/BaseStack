import { Component, Input, OnInit } from '@angular/core'; 
import { BaseFormControl } from '../BaseFormControl';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-hidden-input',
  templateUrl: './hidden-input.component.html',
  styleUrls: ['./hidden-input.component.scss'],
  imports: [FormsModule, ReactiveFormsModule, CommonModule]
})
export class HiddenInputComponent extends BaseFormControl  implements OnInit{
  ngOnInit(): void {
    this.RegisterForm();
  }  
}

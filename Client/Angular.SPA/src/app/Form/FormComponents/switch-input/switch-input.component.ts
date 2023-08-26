import { Component, Input, OnInit } from '@angular/core'; 
import { BaseFormControl } from '../BaseFormControl';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-switch-input',
  templateUrl: './switch-input.component.html',
  styleUrls: ['./switch-input.component.scss'],
  imports: [FormsModule, ReactiveFormsModule, CommonModule]
})
export class SwitchInputComponent extends BaseFormControl  implements OnInit{
  ngOnInit(): void {
    this.RegisterForm();
  }

   ngAfterViewInit(): void {      
    this.Controller.setValue(false);
  }
    
}

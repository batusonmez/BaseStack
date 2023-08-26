import { Component, Input, OnInit } from '@angular/core'; 
import { BaseFormControl } from '../BaseFormControl';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent extends BaseFormControl  implements OnInit{
  ngOnInit(): void {
    this.RegisterForm();
  }
  
}

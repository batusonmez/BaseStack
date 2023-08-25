import { Component, Input, OnInit } from '@angular/core';
import { BaseFormControl } from '../BaseFormControl';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'submit-button',
  standalone: true,
  templateUrl: './submit-button.component.html',
  styleUrls: ['./submit-button.component.scss'],
  imports: [CommonModule]
})
export class SubmitButtonComponent extends BaseFormControl  implements OnInit{
  ngOnInit(): void {
    this.RegisterForm();
  }

  Cancel(): void {
    if(this.Config?.Event){
      this.Config?.Event("Cancel",null);
    }

  }
}

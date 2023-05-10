import { Component, Input } from '@angular/core';
import { BaseFormControl } from '../BaseFormControl';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'submit-button',
  standalone: true,
  templateUrl: './submit-button.component.html',
  styleUrls: ['./submit-button.component.scss'],
  imports: [CommonModule]
})
export class SubmitButtonComponent extends BaseFormControl {

  Cancel(): void {
    if(this.Config?.Event){
      this.Config?.Event("Cancel",null);
    }

  }
}

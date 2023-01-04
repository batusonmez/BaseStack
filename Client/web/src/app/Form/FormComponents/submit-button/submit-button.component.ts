import { Component, Input } from '@angular/core';
import { FieldConfig } from '../../Models/FieldConfig';
import { IFormComponent } from '../../Models/IFormComponent';

@Component({
  selector: 'submit-button',
  templateUrl: './submit-button.component.html',
  styleUrls: ['./submit-button.component.scss']
})
export class SubmitButtonComponent implements IFormComponent {
  @Input() Config?: FieldConfig;
  
}

import { Component, Input } from '@angular/core';
import { FieldConfig } from '../../Models/FieldConfig';
import { IFormComponent } from '../../Models/IFormComponent';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent implements IFormComponent {
  @Input() Config?: FieldConfig;
  @Input() public TempInput: string = "";
}

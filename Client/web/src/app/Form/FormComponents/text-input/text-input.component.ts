import { Component, Input } from '@angular/core'; 
import { BaseFormControl } from '../BaseFormControl';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent extends BaseFormControl {

  @Input() public TempInput: string = "";

  test(): void {
    this.Host?.Config.FormEvent("temp", this.Host.Form.value,"xxxxxx")
  }
}

import { Component, Input } from '@angular/core'; 
import { BaseFormControl } from '../BaseFormControl';

@Component({
  selector: 'app-switch-input',
  templateUrl: './switch-input.component.html',
  styleUrls: ['./switch-input.component.scss']
})
export class SwitchInputComponent extends BaseFormControl {

  @Input() public TempInput: string = "";

 override ngAfterViewInit(): void {     
   super.ngAfterViewInit();
    this.Controller.setValue(false);
  }
}

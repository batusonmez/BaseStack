import { Component, Input, OnInit } from '@angular/core'; 
import { BaseFormControl } from '../BaseFormControl';

@Component({
  selector: 'app-switch-input',
  templateUrl: './switch-input.component.html',
  styleUrls: ['./switch-input.component.scss']
})
export class SwitchInputComponent extends BaseFormControl  implements OnInit{
  ngOnInit(): void {
    this.RegisterForm();
  }

  @Input() public TempInput: string = "";

   ngAfterViewInit(): void {      
    this.Controller.setValue(false);
  }
    
}

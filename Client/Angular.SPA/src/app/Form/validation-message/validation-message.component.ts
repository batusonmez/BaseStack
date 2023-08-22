import { CommonModule } from '@angular/common';
import {  Component, Input  } from '@angular/core';
import { FormControl } from '@angular/forms';
@Component({
  standalone: true,
  selector: 'validation-message',  
  templateUrl: './validation-message.component.html',
  styleUrls: ['./validation-message.component.scss'],
  imports:[CommonModule]
})
export class ValidationMessageComponent {
  @Input() public Controller!:FormControl;
}

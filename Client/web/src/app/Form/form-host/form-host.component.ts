import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormBuilderService } from '../Core/FormBuilderService';
import { FieldHostComponent } from '../field-host/field-host.component';
import { FormConfig } from '../Models/FormConfig';

@Component({
  standalone: true,
  selector: 'form-host',
  templateUrl: './form-host.component.html',
  styleUrls: ['./form-host.component.scss'],
  imports: [FieldHostComponent, CommonModule, ReactiveFormsModule,]
})
export class FormHostComponent {
  @Input() public Config!: FormConfig;
  form: FormGroup = new FormGroup({   }); 
  constructor(private formBuilder: FormBuilderService) {

  }

  ngAfterViewInit(): void {

    this.buildForm();
  }

  public buildForm(): void {
    this.form = this.formBuilder.BuildForm(this.Config); 
  
  }
  onSubmit() {
   
    console.warn(this.form.value);
  }
}

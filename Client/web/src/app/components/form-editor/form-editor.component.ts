import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { Field } from './Models/Field';

@Component({
  selector: 'form-editor',
  templateUrl: './form-editor.component.html',
  styleUrls: ['./form-editor.component.css']
})
export class FormEditorComponent implements OnInit {
  @Input() Fields: Field[] = [];
  payLoad = '';
 
  form!: FormGroup;
  constructor(private sanitizer: DomSanitizer) { }

  ngOnInit(): void {

    const group: any = {};

    this.Fields.forEach(f => {      
      group[f.Key] =   new FormControl(f.Value || '', f.Required ? Validators.required : null)
        
    });
    this.form= new FormGroup(group);
     
  }

  onSubmit() {
    this.payLoad = JSON.stringify(this.form.getRawValue());
  }

 

}

import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'form-editor',
  templateUrl: './form-editor.component.html',
  styleUrls: ['./form-editor.component.css']
})
export class FormEditorComponent implements OnInit {
  @Input() Fields: any[] = [];
  payLoad = '';
  form!: FormGroup;
  constructor() { }

  ngOnInit(): void {

    const group: any = {};

    this.Fields.forEach(f => {
      group[f.key] = f.required ? new FormControl(f.value || '', Validators.required)
        : new FormControl(f.value || '');
    });
    this.form= new FormGroup(group);
     
  }

  onSubmit() {
    this.payLoad = JSON.stringify(this.form.getRawValue());
  }

 

}

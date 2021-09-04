import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser'; 

@Component({
  selector: 'form-editor',
  templateUrl: './form-editor.component.html',
  styleUrls: ['./form-editor.component.css']
})
export class FormEditorComponent implements OnInit {
  @Input() Fields: any[] = [];
  @Output() OnSubmit = new EventEmitter();
  FooterFields: any[] = [];
  EditorFields: any[] = [];
  payLoad = '';
 
  form!: FormGroup;
  constructor(private sanitizer: DomSanitizer) { }

  ngOnInit(): void {

    const group: any = {};

    this.EditorFields = this.Fields.filter(d => d.Section == "body");
    this.FooterFields = this.Fields.filter(d => d.Section == "footer");
    this.EditorFields.forEach(f => {
       
      group[f.Key] =   new FormControl(f.Value || '', f.Required ? Validators.required : null)
        
    });
    
    this.form= new FormGroup(group);
     
  }

  onSubmit(): void {
    this.OnSubmit.emit(this.form.getRawValue());
//    this.payLoad = JSON.stringify(this.form.getRawValue());
  }

  FieldAction(sender: any): void {
    sender.Action();
  }

 

}

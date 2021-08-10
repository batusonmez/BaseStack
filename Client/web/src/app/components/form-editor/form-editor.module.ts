import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';


import { FormEditorComponent } from './form-editor.component' 

@NgModule({
  imports: [
    ReactiveFormsModule,
    CommonModule 
  ],
  declarations: [
    FormEditorComponent
  ],
  exports: [
    FormEditorComponent]
})
export class FormEditorModule { }

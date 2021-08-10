import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
 
import { Sample1Component } from './sample1.component'  
import { FormEditorModule } from '../form-editor/form-editor.module';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: Sample1Component
  }
];
@NgModule({
  declarations: [
    Sample1Component 
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule, 
    FormEditorModule
    
  ]
})
export class Sample1Module { }

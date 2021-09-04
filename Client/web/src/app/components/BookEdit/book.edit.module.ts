import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BookEditComponent } from './book.edit.component'  
import { FormEditorModule } from '../form-editor/form-editor.module';
import { RouterModule, Routes } from '@angular/router'; 

const routes: Routes = [
  {
    path: ':id',
    component: BookEditComponent
  },
  {
    path: '',
    component: BookEditComponent
  }
];
@NgModule({
  declarations: [
    BookEditComponent 
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,  
    FormEditorModule 
  ]
})
export class BookEditModule { }

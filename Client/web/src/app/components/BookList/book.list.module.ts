import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
 
import { BookListComponent } from './book.list.component'   
import { RouterModule, Routes } from '@angular/router';
import { DataTableModule } from '../data-table/data-table.module';

const routes: Routes = [
  {
    path: '',
    component: BookListComponent
  }
];
@NgModule({
  declarations: [
    BookListComponent 
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,  
    DataTableModule    
  ]
})
export class BookListModule { }

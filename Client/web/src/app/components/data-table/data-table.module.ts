import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


import { DataTableComponent } from './data-table.component' 

@NgModule({
  imports: [
    ReactiveFormsModule,
    CommonModule,
    FormsModule
  ],
  declarations: [
    DataTableComponent
  ],
  exports: [
    DataTableComponent]
})
export class DataTableModule { }

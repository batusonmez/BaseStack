import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookViewComponent } from './book.view.component'   
import { RouterModule, Routes } from '@angular/router'; 
import { CommentModule } from '../Comment/comment.module';

const routes: Routes = [
  {
    path: ':id',
    component: BookViewComponent
  }
];
@NgModule({
  declarations: [
    BookViewComponent 
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    CommentModule
  ]
})
export class BookViewModule { }

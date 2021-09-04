import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'books',
    loadChildren: () => import('./components/BookList/book.list.module').then(m => m.BookListModule)
  },
  {
    path: 'edit',
    loadChildren: () => import('./components/BookEdit/book.edit.module').then(m => m.BookEditModule)
  },
  {
    path: 'view',
    loadChildren: () => import('./components/BookView/book.view.module').then(m => m.BookViewModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

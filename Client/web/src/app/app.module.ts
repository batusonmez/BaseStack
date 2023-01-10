import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TableHostComponent } from './DataTable/table-host/table-host.component';
import { FormHostComponent } from './Form/form-host/form-host.component';
import { TextInputComponent } from './Form/FormComponents/text-input/text-input.component';

@NgModule({
  declarations: [
    AppComponent,
 
    TextInputComponent
  ],
  imports: [
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    FormHostComponent,
    TableHostComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

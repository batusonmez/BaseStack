import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TextInputComponent } from './Form/FormComponents/text-input/text-input.component';
import { HttpClientModule } from '@angular/common/http';
import { LoadingScreenComponent } from './loading-screen/loading-screen.component';
import { ToasterComponent } from './Toaster/toaster.component';


@NgModule({
  declarations: [
    AppComponent,
    TextInputComponent
  ],
  imports: [
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule ,
    HttpClientModule ,
    LoadingScreenComponent,
    ToasterComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

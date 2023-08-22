import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TextInputComponent } from './Form/FormComponents/text-input/text-input.component';
import { NumberInputComponent } from './Form/FormComponents/number-input/number-input.component';
import { HttpClientModule } from '@angular/common/http';
import { LoadingScreenComponent } from './loading-screen/loading-screen.component';
import { ToasterComponent } from './Toaster/toaster.component';
import { DataListComponent } from './Form/FormComponents/data-list/datalist.component'; 
import { SwitchInputComponent } from './Form/FormComponents/switch-input/switch-input.component';


@NgModule({
  declarations: [
    AppComponent,
    TextInputComponent,
  
    SwitchInputComponent
  ],
  imports: [   
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule ,
    HttpClientModule ,
    LoadingScreenComponent,
    ToasterComponent,
    DataListComponent,
    NumberInputComponent  
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

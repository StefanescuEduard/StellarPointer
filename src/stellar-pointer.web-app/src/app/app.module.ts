import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AngularMaterialModule } from './angular-material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { StellarObjectComponent } from './planet/stellar-object.component';
import { StellarObjectService } from './services/stellar-object/stellar-object.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [AppComponent, StellarObjectComponent],
  imports: [BrowserModule, AngularMaterialModule, BrowserAnimationsModule, HttpClientModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}

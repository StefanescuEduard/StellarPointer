import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AngularMaterialModule } from './angular-material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { StellarObjectComponent } from './planet/stellar-object.component';
import { HttpClientModule } from '@angular/common/http';
import { AppConfig } from 'src/config/app-config';

@NgModule({
  declarations: [AppComponent, StellarObjectComponent],
  imports: [
    BrowserModule,
    AngularMaterialModule,
    BrowserAnimationsModule,
    HttpClientModule,
  ],
  providers: [
    AppConfig,
    {
      provide: APP_INITIALIZER,
      useFactory: initializeAppConfig,
      deps: [AppConfig],
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

export function initializeAppConfig(appConfig: AppConfig): () => Promise<void> {
  return () => appConfig.loadEnvironmentFile();
}

import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

const extraProviders = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
];

export function getBaseUrl(): string {
  return document.getElementsByTagName('base')[0].href;
}

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(extraProviders)
  .bootstrapModule(AppModule)
  .catch((err) => console.error(err));

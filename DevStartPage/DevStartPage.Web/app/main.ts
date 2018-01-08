import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { enableProdMode } from '@angular/core';
import { AppModule } from './boot';
import { environment } from './environment';
const platform = platformBrowserDynamic();

if (environment.production) {
    enableProdMode();
}
platform.bootstrapModule(AppModule);
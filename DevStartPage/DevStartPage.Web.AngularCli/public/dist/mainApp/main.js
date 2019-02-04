"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var platform_browser_dynamic_1 = require("@angular/platform-browser-dynamic");
var core_1 = require("@angular/core");
var boot_1 = require("./boot");
var environment_1 = require("./environment");
var platform = platform_browser_dynamic_1.platformBrowserDynamic();
if (environment_1.environment.production) {
    core_1.enableProdMode();
}
platform.bootstrapModule(boot_1.AppModule);

//# sourceMappingURL=main.js.map

/**
 * System configuration for Angular samples
 * Adjust as necessary for your application needs.
 */
(function (global) {
    System.config({

        baseURL: '/',

        paths: {
            // paths serve as alias
            'npm:': '/node_modules/'
        },
        // map tells the System loader where to look for things
        map: {
            // our app is within the app folder
            app: 'public/dist/mainApp',
            // angular bundles
            '@angular/core': 'npm:@angular/core/bundles/core.umd.js',
            '@angular/common': 'npm:@angular/common/bundles/common.umd.js',
            '@angular/compiler': 'npm:@angular/compiler/bundles/compiler.umd.js',
            '@angular/platform-browser': 'npm:@angular/platform-browser/bundles/platform-browser.umd.js',
            '@angular/platform-browser-dynamic': 'npm:@angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.js',
            '@angular/http': 'npm:@angular/http/bundles/http.umd.js',
            '@angular/router': 'npm:@angular/router/bundles/router.umd.js',
            '@angular/forms': 'npm:@angular/forms/bundles/forms.umd.js',
            '@swimlane/ngx-datatable': '/node_modules/@swimlane/ngx-datatable/release/index.js',
            // other libraries
            'rxjs': 'npm:rxjs',
            'moment': 'npm:moment',
            'ngx-bootstrap': 'npm:ngx-bootstrap',
            'angular-2-data-table': 'npm:angular-2-data-table/dist',
            'ng2-dnd': 'node_modules/ng2-dnd/bundles/index.umd.js',
            'ng2-toasty': 'npm:ng2-toasty/bundles/index.umd.js',
            'lodash': 'node_modules/lodash',
            'file-saver': 'node_modules/file-saver',
            'ngx-tabs': 'node_modules/ngx-tabs',
            'ngx-dropdown': 'node_modules/ngx-dropdown',
            'ngx-progressbar': 'node_modules/ngx-progressbar/bundles/ngx-progressbar.umd.js',
            'ngx-mydatepicker': 'npm:ngx-mydatepicker/bundles/ngx-mydatepicker.umd.min.js',
            'ng2-charts': 'npm:ng2-charts',
            'text-mask-core': 'npm:text-mask-core',
            'angular2-text-mask': 'npm:angular2-text-mask/dist/angular2TextMask.js',
            'text-mask-addons': 'npm:text-mask-addons',
            'angular2-recaptcha': 'node_modules/angular2-recaptcha'
        },
        // packages tells the System loader how to load when no filename and/or no extension
        packages: {
            app: {
                main: './main.js',
                defaultExtension: 'js'
            },
            rxjs: {
                defaultExtension: 'js'
            },
            'moment': {
                main: './moment.js',
                defaultExtension: 'js'
            },
            'angular-2-data-table': {
                format: 'cjs',
                defaultExtension: 'js',
                main: 'index.js'
            },
            'ngx-bootstrap': { format: 'cjs', main: 'bundles/ngx-bootstrap.umd.js', defaultExtension: 'js' },
            'ng2-charts': { defaultExtension: 'js' },
            'lodash': { main: 'index.js', defaultExtension: 'js' },
            'file-saver': { format: 'cjs', main: 'FileSaver.js', defaultExtension: 'js' },
            'ngx-tabs': { main: 'index.js', defaultExtension: 'js' },
            'ngx-dropdown': { main: 'index.js', defaultExtension: 'js' },            
            'text-mask-core': {
            defaultExtension: 'js'
            },
            'angular2-text-mask': {
                defaultExtension: 'js'
            },
            'text-mask-addons': { defaultExtension: 'js' },
            'angular2-recaptcha': { defaultExtension: 'js', main: 'index' }
        }
    });
})(this);
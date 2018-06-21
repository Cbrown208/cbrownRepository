const del = require('del');
const gulp = require('gulp');
const cleanCSS = require('gulp-clean-css');
const colors = require('colors');
const concat = require('gulp-concat');
const plumber = require('gulp-plumber');
const runSequence = require('run-sequence');
const sourcemaps = require('gulp-sourcemaps');
const sysBuilder = require('systemjs-builder');
const tslint = require('gulp-tslint');
const tsc = require('gulp-typescript');
const sass = require('gulp-sass');
const ts = require('gulp-typescript');
var util = require('gulp-util');
const uglify = require('gulp-uglify');

var gulpLoadPlugins = require('gulp-load-plugins');
var browserSync = require('browser-sync');
var $ = gulpLoadPlugins();
var reload = browserSync.reload;

const tscConfig = require('./app/tsconfig.json');

// compiles 
var tsProject = ts.createProject('app/tsconfig.json', {
	typescript: require('typescript')
});


//************************************************* Build **************************************************//
gulp.task('default', function (callback) {
	runSequence('build:prod', callback);
});

gulp.task('build:dev', function (callback) {
	runSequence('clean', 'scripts:dev', 'copy', 'styles', callback);
});

gulp.task('build:debug', function (callback) {
	runSequence('scripts:dev', callback);
});

gulp.task('build:prod', function (callback) {
	runSequence('clean', 'scripts', 'copy', 'styles', callback);
});

//************************************************* Clean **************************************************//
// Clean the js distribution directory
gulp.task('clean', ['clean:dist:js', 'clean:dist:css', 'clean:lib', 'clean:scripts']);

gulp.task('clean:dist:js', function () {
    return del('public/dist/js/*');
});

// Clean the css distribution directory
gulp.task('clean:dist:css', function () {
    return del('public/dist/css/*');
});

gulp.task('clean:generated:css', function () {
    return del('Content/generated/css/*');
});

// Clean library directory
gulp.task('clean:lib', function () {
    return del('public/lib/**/*');
});

// Clean scripts directory
gulp.task('clean:scripts', function () {
    return del('Scripts/**/*');
});

// Lint Typescript
gulp.task('lint:ts', function () {
    return gulp.src('app/**/*.ts')
        .pipe(tslint())
        .pipe(tslint.report('verbose', { emitError: false }));
});

// Compile TypeScript to JS
gulp.task('compile:ts', function () {
    return gulp

        .src("App/**/*.ts")
        .pipe(plumber({
            errorHandler: function (err) {
                console.error('>>> [tsc] Typescript compilation failed'.bold.green);
                this.emit('end');
            }
        }))
        .pipe(sourcemaps.init())
        .pipe(tsProject(), undefined, ts.reporter.fullReporter())
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest('public/dist/mainApp'));

   
});

// Generate systemjs-based builds
gulp.task('bundle:dev:js', function () {
    var builder = new sysBuilder('', './systemjs.config.js');
    return builder.buildStatic('app', 'public/dist/js/main.app.min.js', { minify: false, encodeNames: false, rollup: true })
        .then(function () {
            return del(['public/dist/js/**/*', '!public/dist/js/main.app.min.js']);
        })
        .catch(function (err) {
            console.error('>>> [systemjs-builder] Bundling failed'.bold.green, err);
        });
});

// Generate systemjs-based builds
gulp.task('bundle:js', function () {
    var builder = new sysBuilder('', './systemjs.config.js');
    return builder.buildStatic('app', 'Scripts/main.app.min.js', { minify: true, encodeNames: false, rollup: true })
        .then(function () {
            return del(['public/dist/js/**/*', '!public/dist/js/main.app.min.js']);
        })
        .catch(function (err) {
            console.error('>>> [systemjs-builder] Bundling failed'.bold.green, err);
        });
});


gulp.task('minify:js', function () {
    return gulp
        .src('public/dist/js/main.app.min.js')
        .pipe(uglify())
        .pipe(gulp.dest('public/dist/js/main.app.min.js'));
});

gulp.task('copy:scripts', function () {
    return gulp.src('public/dist/js/main.app.min.js')
        .pipe(gulp.dest('Scripts'));
});


// Lint Sass
gulp.task('lint:sass', function () {
    return gulp.src('src/**/*.scss')
        .pipe(plumber({
            errorHandler: function (err) {
                console.error('>>> [sass-lint] Sass linting failed'.bold.green);
                this.emit('end');
            }
        }))
        .pipe(sassLint())
        .pipe(sassLint.format())
        .pipe(sassLint.failOnError());
});


var GlobalsassPaths = [
    'Content/styles/_bootstrap.variables.scss',
    'Content/styles/main.scss'
];

// Compile SCSS to CSS, concatenate, and minify
gulp.task('compile:sass', function () {
    // concat and minify global scss files
    gulp
        .src(GlobalsassPaths)
        .pipe(plumber({
            errorHandler: function (err) {
                console.error('>>> [sass] Sass global style compilation failed'+ err);
                this.emit('end');
            }
        }))
        //.pipe(sourcemaps.init())
        .pipe(sass({ errLogToConsole: true }))
        .pipe(concat('styles.min.css'))
        .pipe(cleanCSS())
        //.pipe(sourcemaps.write())
        .pipe(gulp.dest('Content/generated/css'));

    // minify component specific scss files
    //gulp
    //    .src('src/css/component/*.scss')
    //    .pipe(plumber({
    //        errorHandler: function (err) {
    //            console.error('>>> [sass] Sass component style compilation failed'.bold.green);
    //            this.emit('end');
    //        }
    //    }))
    //    .pipe(sourcemaps.init())
    //    .pipe(sass({ errLogToConsole: true }))
    //    .pipe(cleanCSS())
    //    .pipe(sourcemaps.write())
    //    .pipe(gulp.dest('Content/generated/css'));
});


var cssComponentPaths = [
    'node_modules/ng2-slim-loading-bar/bundles/style.css',
    'node_modules/ng2-toasty/bundles/style-bootstrap.css',
	'app/Shared/Widgets/Spinner/spinner.css',
	'Content/styles/main.css'
];

var cssGlobalPaths = [
    //'Content/dist/bootstrap/css/bootstrap.min.css',
    'Content/styles/ng2-toasty.css',
    'Content/styles/main.css',
    'Content/font-awesome.css',
    'Content/font-awesome.min.css'
];

// Concat and minify CSS
gulp.task('minify:css', function () {

    gulp.src('Content/fonts/**/*')
        .pipe(gulp.dest('content/generated/fonts'));

    // concat and minify global css files
    gulp
        .src(cssGlobalPaths)
        .pipe(concat('global.min.css'))
        .pipe(cleanCSS())
        .pipe(gulp.dest('content/generated/css'));

    // minify component css files
    return gulp
        .src(cssComponentPaths)
        .pipe(concat('component.min.css'))
        .pipe(cleanCSS())
        .pipe(gulp.dest('content/generated/css'));
});


gulp.task('copy:cssBundle', function () {
    gulp.src(
            'public/dist/css/**/*')
        .pipe(gulp.dest('content/generated/css'));
});


// Copy dependencies
gulp.task('copy:libs', function () {


    // concatenate non-angular2 libs, shims & systemjs-config
    gulp.src([
            'node_modules/jquery/dist/jquery.min.js',
            'node_modules/bootstrap/dist/js/bootstrap.min.js',
            'node_modules/es6-shim/es6-shim.min.js',
            'node_modules/es6-promise/dist/es6-promise.min.js',
            'node_modules/core-js/client/shim.min.js',
            'node_modules/zone.js/dist/zone.js',
            'node_modules/reflect-metadata/Reflect.js',
            // 'node_modules/systemjs/dist/system-polyfills.js',
            'node_modules/systemjs/dist/system.src.js',
            'node_modules/chart.js/dist/Chart.bundle.js',
            'Content/plugins/chartjsplugin.js',
            'systemjs.config.js'
        ])
        .pipe(concat('vendors.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest('Scripts/lib/js'));

    // copy source maps
    return gulp.src([
        'node_modules/es6-shim/es6-shim.map',
        'node_modules/reflect-metadata/Reflect.js.map',
        'node_modules/systemjs/dist/system-polyfills.js.map'
    ]).pipe(gulp.dest('Scripts/lib/js'));

    //gulp.src(['node_modules/rxjs/**/*'])
    // .pipe(gulp.dest('public/lib/js/rxjs'));

    //return gulp.src(['node_modules/@angular/**/*'])
    //  .pipe(gulp.dest('public/lib/js/@angular')); 
});

gulp.task('copy:CSS', function () {
    gulp.src([
        'node_modules/bootstrap/dist/css/bootstrap.*'
    ]).pipe(gulp.dest('public/lib/css'));
});

// UNUSED
// Copy static assets
gulp.task('copy:assets', function () {
    return gulp.src(
            [
                '*.json',
                '*.html',
                '*.css',
                '!*.ts',
                '!*.scss'
            ],
            { base: 'src/**' })
        .pipe(gulp.dest('public/dist'))
});

// Update the tsconfig files based on the glob pattern
gulp.task('tsconfig-glob', function () {
    return tsconfig({
        configPath: '.',
        indent: 2
    });
});

// Watch src files for changes, then trigger recompilation
gulp.task('watch:src', function () {
    gulp.watch('src/**/*.ts', ['scripts']);
    gulp.watch('src/**/*.scss', ['styles']);
});

// Run Express, auto rebuild and restart on src changes
gulp.task('serve', ['watch:src'], function () {
    var server = liveServer.new('server.js');
    server.start();

    gulp.watch('server.js', server.start.bind(server));
});

// Compile .ts files unbundled for tests
gulp.task('compile:specs', function () {
    return gulp
        .src([
            "src/**/*.ts",
            "typings/*.d.ts"
        ])
        .pipe(plumber({
            errorHandler: function (err) {
                console.error('>>> [tsc] Typescript tests compilation failed'.bold.green);
                this.emit('end');
            }
        }))
        .pipe(tsc(testTscConfig.compilerOptions))
        .pipe(gulp.dest('tests'));
});

gulp.task('test', ['compile:specs'], function () {
    gulp.watch('src/**/*.ts', ['compile:specs']);
});

gulp.task('lint', ['lint:ts', 'lint:sass']);

gulp.task('copy', function (callback) {
	//runSequence('copy:libs', callback); //TODO changed here 
	runSequence('copy:libs', 'copy:scripts', callback);
});

gulp.task('scripts', function (callback) {
	runSequence('compile:ts', 'bundle:js', callback);
});

gulp.task('scripts:dev', function (callback) {
    runSequence('compile:ts', 'bundle:dev:js', callback);
});

gulp.task('styles', ['minify:css', 'compile:sass']);

//Others
gulp.task('iisexpress', function () {
    gulp.src('')
        .pipe($.shell([
            '"C:\\Program Files\\IIS Express\\iisexpress.exe" /config:..\\.vs\\config\\applicationhost.config /site:DevStartPage /systray:false'
        ]));
});

var src = '',
    app = src + 'app/',
    content = 'content/',
    styles = content + 'styles/',
    generated = content + 'generated/',
    generatedCss = generated + 'css/',
    generatedJs = generated + 'js/',
    public = src + 'public/',
    dist = public + 'dist/',
    mainApp = dist + 'mainApp',
    tsFiles = app + '**/*.ts',
    htmlFiles = app + '**/*.html',
    //sassFile = styles + 'main.scss',
    cssFiles = styles + '**/*.css';

var config = {
    app: app,
    content: content,
    styles: styles,
    generated: generated,
    generatedCss: generatedCss,
    generatedJs: generatedJs,
    public: public,
    dist: dist,
    mainApp: mainApp,
    tsFiles: tsFiles,
    htmlFiles: htmlFiles,
    //sassFile: sassFile,
    cssFiles: cssFiles
};

var bsConfig = {
    port: 3000,
    proxy: {
		target: "http://localhost:28812/",
        middleware: function(req, res, next){
            //if(req.url == '/') {
            //    req.url = req.url + "?clientId=3503&facId=5318";
            //}        
            next();
        }
    }, // "http://localhost:57909?clientId=52060&facId=2566",
    injectChanges: false,
    files: [
        "content/**/*.css",
        "app/**/*.ts",
        "app/**/*.js",
        "app/**/*.css",
        "app/**/*.html"
    ]
};

function startBrowsersync(config) {
    bsIns = browserSync.create();
    bsIns.init(config);
    bsIns.reload();
}

gulp.task('tsc-app', function () {
    return compileTs(config.tsFiles);
});

gulp.task('watch-ts', function () {
    return gulp.watch(config.tsFiles, function (file) {
        util.log('Compiling ' + file.path + '...');
        return compileTs(file.path, true);
    });
});

gulp.task('watch-html', function () {
    gulp.watch(config.htmlFiles);
});

gulp.task('css', function () {
    return gulp.src(config.cssFiles)
        .pipe(gulp.dest(config.tmpApp));
});

gulp.task('watch-css', function () {
    gulp.watch(config.cssFiles);
});

//gulp.task('sass', function () {
//    return gulp.src(config.sassFile)
//        .pipe(sass().on('error', sass.logError))
//        .pipe(gulp.dest(config.styles));
//});

//gulp.task('watch-sass', function () {
//    var location = config.styles + '*.scss';
//    gulp.watch(location, ['sass']);
//});

gulp.task('serve-dev', function () {
    runSequence(['tsc-app'],
		['watch-ts', 'watch-html', 'watch-css'], function () {
        startBrowsersync(bsConfig);
    });
});

function compileTs(files, watchMode) {
    watchMode = watchMode || false;
    var outputDir = 'public/dist/mainApp/'; 
    var tsProject = ts.createProject('app/tsconfig.json');
    var res = gulp.src(files, {
            base: 'app/',
            outDir: outputDir
        })
        .pipe(sourcemaps.init())
        .pipe(tsProject())
        .on('error', function () {
            if (watchMode) {
                return;
            }
            process.exit(1);
        });
    return res.js
        .pipe(sourcemaps.write())
        .pipe(gulp.dest(outputDir));
}


import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TestComponent } from './Test/test.component';
import { DownloadsComponent } from './Downloads/index'
import { StartPageComponent } from './StartPage/index'

const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
	{ path: 'home', component: StartPageComponent, pathMatch: 'full' },
	{ path: 'downloads', component: DownloadsComponent },
	{ path: 'testing', component: TestComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { useHash: true })],
    exports: [RouterModule]
})
export class AppRoutingModule { }
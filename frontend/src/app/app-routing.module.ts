import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { InfoWeatherComponent } from './info-weather/info-weather.component';
import { InfoForecastComponent } from './info-forecast/info-forecast.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { InfoHistoricalComponent } from './info-historical/info-historical.component';

// Define the path
const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'info-weather', component: InfoWeatherComponent },
  { path: 'info-historical', component: InfoHistoricalComponent },
  { path: 'info-forecast', component: InfoForecastComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

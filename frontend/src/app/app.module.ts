import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; // Agregar FormsModule
import { RouterModule, Routes } from '@angular/router'; // Importa RouterModule y Routes
import { provideHttpClient, withInterceptorsFromDi, withFetch   } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InfoWeatherComponent } from './info-weather/info-weather.component';
import { ForecastComponent } from './forecast/forecast.component';
import { HomeComponent } from './home/home.component';




// Define las rutas
const appRoutes: Routes = [
  { path: '', component: HomeComponent },  // Ruta para la página de inicio
  { path: 'home', component: HomeComponent },  // Ruta para la To-Do List
  { path: 'info-weather', component: InfoWeatherComponent },  // Redirige a la página de tareas
  { path: 'forecast', component: ForecastComponent },  // Redirige a la página de tareas archivadas
];

@NgModule({
  declarations: [
    AppComponent,
    InfoWeatherComponent,
    ForecastComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(appRoutes),
    FormsModule
  ],
  providers: [
    provideHttpClient(withInterceptorsFromDi(), withFetch())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

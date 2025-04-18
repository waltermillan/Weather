import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import {
  provideHttpClient,
  withInterceptorsFromDi,
  withFetch,
} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';

import { InfoWeatherComponent } from './info-weather/info-weather.component';
import { InfoForecastComponent } from './info-forecast/info-forecast.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { SuccessDialogComponent } from './modals/success-dialog/success-dialog.component';
import { FailureDialogComponent } from './modals/failure-dialog/failure-dialog.component';
import { WarningDialogComponent } from './modals/warning-dialog/warning-dialog.component';
import { InfoHistoricalComponent } from './info-historical/info-historical.component';
import { ShowDataWeatherComponent } from './show-data-weather/show-data-weather.component';

@NgModule({
  declarations: [
    AppComponent,
    InfoWeatherComponent,
    InfoForecastComponent,
    HomeComponent,
    LoginComponent,
    SuccessDialogComponent,
    FailureDialogComponent,
    WarningDialogComponent,
    InfoHistoricalComponent,
    ShowDataWeatherComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    MatSnackBarModule,
    BrowserAnimationsModule,
    MatDialogModule,
  ],
  providers: [
    provideHttpClient(withInterceptorsFromDi(), withFetch()),
    provideAnimationsAsync(),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

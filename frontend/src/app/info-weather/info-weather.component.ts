import { Component, OnInit } from '@angular/core';

import { MatDialog } from '@angular/material/dialog';
import { FailureDialogComponent } from '../modals/failure-dialog/failure-dialog.component';
import { WarningDialogComponent } from '../modals/warning-dialog/warning-dialog.component';
import { City } from '../models/city.model';
import { CityService } from '../services/city.service';
import { WeatherService } from '../services/weather.service';
import { Weather } from '../models/weather.model';
import { HistoricalQuery } from '../models/historical-query.model';
import { HistoricalQueryService } from '../services/historical-query.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-info-weather',
  templateUrl: './info-weather.component.html',
  styleUrls: ['./info-weather.component.css'],
})
export class InfoWeatherComponent implements OnInit {
  title: string = 'Info Weather';

  cities?: City[];

  weather: Weather | null = null;
  // Selected City
  selectedCity: string = '';

  constructor(
    private cityService: CityService,
    private weatherService: WeatherService,
    private historicalQueryService: HistoricalQueryService,
    private authService: AuthService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.getAllCities();

    this.dialog.open(WarningDialogComponent, {
      data: {
        message: 'Please select a city.',
      },
    });
  }

  getAllCities() {
    this.cityService.getAll().subscribe({
      next: (data) => {
        this.cities = data.sort((a, b) => a.name.localeCompare(b.name));
      },
      error: (error) => {
        console.error('Error getting cities.', error);
        this.dialog.open(FailureDialogComponent, {
          data: {
            message: 'Error getting cities.',
          },
        });
      },
    });
  }

  getWeatherInfo(city: string): void {
    this.weatherService.getWeatherInfoByCity(city).subscribe({
      next: (data) => {
        this.weather = data;
        this.saveWeatherInfo(); //Save the info consulted.
      },
      error: (error) => {
        console.error('Error getting data weather.', error);
        this.dialog.open(FailureDialogComponent, {
          data: {
            message: 'Error getting data weather.',
          },
        });
      },
    });
  }

  saveWeatherInfo() {
    let query = new HistoricalQuery();
    Object.assign(query, {
      id: 0,
      userId: this.authService.getUserId(),
      queryParams: this.selectedCity,
      response: JSON.stringify(this.weather),
      queriedAt: new Date(),
    });

    this.historicalQueryService.add(query).subscribe({
      next: (data) => {
        console.log('Historical query added successfully');
      },
    });
  }
}

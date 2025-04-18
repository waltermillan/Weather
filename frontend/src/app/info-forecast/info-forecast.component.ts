import { Component, OnInit } from '@angular/core';

import { City } from '../models/city.model';
import { CityService } from '../services/city.service';
import { Weather } from '../models/weather.model';
import { WeatherService } from '../services/weather.service';

import { MatDialog } from '@angular/material/dialog';
import { FailureDialogComponent } from '../modals/failure-dialog/failure-dialog.component';
import { WarningDialogComponent } from '../modals/warning-dialog/warning-dialog.component';

@Component({
  selector: 'app-info-forecast',
  templateUrl: './info-forecast.component.html',
  styleUrl: './info-forecast.component.css',
})
export class InfoForecastComponent implements OnInit {
  startDate: string = '';
  endDate: string = '';
  note: string = 'Always will be three days from today.';

  cities?: City[];

  weathers: Weather[] = [];
  // Selected City
  selectedCity: string = '';
  currentIndex: number = 0;

  constructor(
    private cityService: CityService,
    private weatherService: WeatherService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.setDateRange();
    this.getAllCountries();

    this.dialog.open(WarningDialogComponent, {
      data: {
        message:
          'The forecast for the next 3 days will be brought. Please choose a city',
      },
    });
  }

  getForecastInfo() {
    this.weatherService
      .getForecastInfoByCity(this.selectedCity, this.startDate, this.endDate)
      .subscribe({
        next: (data) => {
          this.weathers = data;
        },
        error: (error) => {
          console.error('Error loading forecasts.');
          this.dialog.open(FailureDialogComponent, {
            data: {
              message: 'Error loading forecasts.',
            },
          });
        },
      });
  }

  getAllCountries() {
    this.cityService.getAll().subscribe({
      next: (data) => {
        this.cities = data.sort((a, b) => a.name.localeCompare(b.name));
      },
      error: (error) => {
        console.error('Error loading cities.');
        this.dialog.open(FailureDialogComponent, {
          data: {
            message: 'Error loading cities.',
          },
        });
      },
    });
  }

  setDateRange(): void {
    const today = new Date();

    const tomorrow = new Date(today);
    tomorrow.setDate(today.getDate() + 1);

    const futureDate = new Date(tomorrow);
    futureDate.setDate(tomorrow.getDate() + 2); // Includes 3 days from tomorrow

    this.startDate = tomorrow.toISOString().split('T')[0]; // yyyy-MM-dd
    this.endDate = futureDate.toISOString().split('T')[0]; // yyyy-MM-dd

    console.log('startDate:', this.startDate); // e.g: 2025-04-18
    console.log('endDate:', this.endDate); // e.g: 2025-04-20
  }

  get currentWeather(): Weather | null {
    return this.weathers.length ? this.weathers[this.currentIndex] : null;
  }

  nextDay() {
    if (this.currentIndex < this.weathers.length - 1) {
      this.currentIndex++;
    }
  }

  prevDay() {
    if (this.currentIndex > 0) {
      this.currentIndex--;
    }
  }
}

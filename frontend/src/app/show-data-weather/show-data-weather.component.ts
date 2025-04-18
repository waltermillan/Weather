import { Component, Input } from '@angular/core';

import { Weather } from '../models/weather.model';

@Component({
  selector: 'app-show-data-weather',
  templateUrl: './show-data-weather.component.html',
  styleUrl: './show-data-weather.component.css',
})
export class ShowDataWeatherComponent {
  @Input() weather: Weather | null = null;
  @Input() selectedCity: string = '';

  constructor() {}
}

import { Component, OnInit } from '@angular/core';
import { WeatherService } from '../services/weather.service';
import { Weather } from '../models/weather.model';

@Component({
  selector: 'app-info-weather',
  templateUrl: './info-weather.component.html',
  styleUrls: ['./info-weather.component.css']
})
export class InfoWeatherComponent implements OnInit {
  title: string = 'Info Weather';

  // Lista de las ciudades españolas más importantes
  cities: string[] = [
    'Madrid', 'Barcelona', 'Valencia', 'Sevilla', 'Zaragoza',
    'Málaga', 'Murcia', 'Palma de Mallorca', 'Las Palmas de Gran Canaria', 'Bilbao',
    'Alicante', 'Córdoba', 'Valladolid', 'Vigo', 'Gijón',
    'Lleida', 'Tarragona', 'Burgos', 'Salamanca', 'León'
  ];

  // Propiedad que almacenará los datos del clima
  public weather: Weather | null = null;  // Inicializa como null para poder comprobarlo en el HTML

  // Ciudad seleccionada
  selectedCity: string = '';

  constructor(private weatherService: WeatherService) {}

  ngOnInit(): void {
    this.cities.sort();
    
    // Verifica que haya una ciudad seleccionada antes de llamar al método
    if (this.selectedCity) {
      //this.getWeatherInfo(this.selectedCity);
    }
  }

  // Método para obtener la información del clima
  getWeatherInfo(city: string): void {
    this.weatherService.getWeatherInfoByCity(city).subscribe({
      next: (data) => {
        console.log(data)
        this.weather = data;  // Asigna los datos recibidos a la propiedad 'weather'
        
      },
      error: (error) => {
        console.error('Error al obtener los datos del clima:', error);
      }
    });
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Weather } from '../models/weather.model';
import { map, catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  private apiUrl = 'http://localhost:5184/api/Weather/Get?city=';  // URL de la API

  constructor(private http: HttpClient) {}

  // Método para obtener la información del clima por ciudad
  getWeatherInfoByCity(city: string): Observable<Weather> {
    const url = `${this.apiUrl}${city}`;
    console.log(url);
    return this.http.get<any>(url).pipe(
      map(response => {
        console.log('Respuesta de la API:', response);
  
        // Ya no se necesita acceder a response.days, la respuesta es directamente el objeto
        if (!response || !response.location) {
          throw new Error('Datos no válidos de la API');
        }
  
        // Creamos el objeto 'Weather' directamente a partir de la respuesta
        const weather: Weather = {
          location: response.location,
          temperature: response.temperature,
          tempmax: response.tempMax,
          tempmin: response.tempMin,
          humidity: response.humidity,
          precipprob: response.precipProb,
          sunrise: new Date(`1970-01-01T${response.sunrise}Z`),  // Convertir a Date
          sunset: new Date(`1970-01-01T${response.sunset}Z`),  // Convertir a Date
          description: response.description,
          pressure: response.pressure,
          icon: response.icon
        };
  
        return weather;
      }),
      catchError(error => {
        console.error('Error al obtener los datos del clima:', error);
        return throwError(() => new Error('No se pudo obtener el clima.'));
      })
    );
  }    
}

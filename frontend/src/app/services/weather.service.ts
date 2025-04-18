import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Weather } from '../models/weather.model';
import { map, catchError } from 'rxjs/operators';
import { GLOBAL } from '../configuration/configuration.global';

@Injectable({
  providedIn: 'root',
})
export class WeatherService {
  constructor(private http: HttpClient) {}

  // Method for obtaining weather information by city
  getWeatherInfoByCity(city: string): Observable<Weather> {
    const url = `${GLOBAL.apiWeatherUrl}/${city}`;
    console.log('urL: ' + url);
    return this.http.get<any>(url).pipe(
      map((response) => {
        if (!response || !response.location) {
          throw new Error('Datos no válidos de la API');
        }

        const weather: Weather = {
          datetime: new Date().toISOString().split('T')[0],
          location: response.location,
          temperature: response.temperature,
          tempmax: response.tempMax,
          tempmin: response.tempMin,
          humidity: response.humidity,
          precipprob: response.precipProb,
          sunrise: new Date(`1970-01-01T${response.sunrise}Z`),
          sunset: new Date(`1970-01-01T${response.sunset}Z`),
          description: response.description,
          pressure: response.pressure,
          icon: response.icon,
        };

        return weather;
      }),
      catchError((error) => {
        console.error('Error al obtener los datos del clima:', error);
        return throwError(() => new Error('No se pudo obtener el clima.'));
      })
    );
  }

  // Method for obtaining weather forecast information by city for the next 3 days
  getForecastInfoByCity(
    city: string,
    startDate: string,
    endDate: string
  ): Observable<Weather[]> {
    const url = `${GLOBAL.apiWeatherUrl}/forecast/${city}?date1=${startDate}&date2=${endDate}`;
    console.log('URL: ' + url);

    return this.http.get<any>(url).pipe(
      map((response) => {
        console.log('Respuesta de la API:', response);

        if (!Array.isArray(response) || response.length !== 3) {
          throw new Error(
            'Datos no válidos de la API o no hay 3 días disponibles'
          );
        }

        const weatherArray: Weather[] = response.map((day: any) => ({
          datetime: day.datetime,
          location: city,
          temperature: day.temperature,
          tempmax: day.tempMax,
          tempmin: day.tempMin,
          humidity: day.humidity,
          precipprob: day.precipProb,
          sunrise: new Date(`1970-01-01T${day.sunrise}Z`),
          sunset: new Date(`1970-01-01T${day.sunset}Z`),
          description: day.description,
          pressure: day.pressure,
          icon: day.icon,
        }));

        return weatherArray;
      }),
      catchError((error) => {
        console.error('Error al obtener los datos del clima:', error);
        return throwError(() => new Error('No se pudo obtener el clima.'));
      })
    );
  }
}

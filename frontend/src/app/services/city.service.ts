import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { City } from '../models/city.model';
import { GLOBAL } from '../configuration/configuration.global';

@Injectable({
  providedIn: 'root',
})
export class CityService {
  endpoint: string = 'cities';

  constructor(private http: HttpClient) {}

  getAll(): Observable<City[]> {
    const url = `${GLOBAL.apiBaseUrl}/${this.endpoint}`;
    return this.http.get<City[]>(url);
  }

  getById(id?: number): Observable<City> {
    const url = `${GLOBAL.apiBaseUrl}${this.endpoint}/${id}`;
    return this.http.get<City>(url);
  }

  add(city: City): Observable<any> {
    const url = `${GLOBAL.apiBaseUrl}${this.endpoint}`;
    return this.http.post<any>(url, city);
  }

  update(city: City): Observable<any> {
    const url = `${GLOBAL.apiBaseUrl}${this.endpoint}/${city.id}`;
    return this.http.put<any>(url, city);
  }

  delete(id: number): Observable<any> {
    const url = `${GLOBAL.apiBaseUrl}${this.endpoint}/${id}`;
    return this.http.delete<any>(url);
  }
}

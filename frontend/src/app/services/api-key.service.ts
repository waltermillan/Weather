import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { City } from '../models/city.model';
import { GLOBAL } from '../configuration/configuration.global';
import { ApiKey } from '../models/api-key.model';

@Injectable({
  providedIn: 'root',
})
export class ApiKeyService {
  endpoint: string = 'apikeys';

  constructor(private http: HttpClient) {}

  getAll(): Observable<ApiKey[]> {
    const url = `${GLOBAL.apiBaseUrl}/${this.endpoint}`;
    return this.http.get<ApiKey[]>(url);
  }

  getById(id?: number): Observable<ApiKey> {
    const url = `${GLOBAL.apiBaseUrl}${this.endpoint}/${id}`;
    return this.http.get<ApiKey>(url);
  }

  add(apikey: ApiKey): Observable<any> {
    const url = `${GLOBAL.apiBaseUrl}${this.endpoint}`;
    return this.http.post<any>(url, apikey);
  }

  update(apikey: ApiKey): Observable<any> {
    const url = `${GLOBAL.apiBaseUrl}${this.endpoint}/${apikey.id}`;
    return this.http.put<any>(url, apikey);
  }

  delete(id: number): Observable<any> {
    const url = `${GLOBAL.apiBaseUrl}${this.endpoint}/${id}`;
    return this.http.delete<any>(url);
  }
}

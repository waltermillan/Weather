import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { GLOBAL } from '../configuration/configuration.global';
import { HistoricalQuery } from '../models/historical-query.model';
import { HistoricalQueryDTO } from '../models/historical-query-dto.model';

@Injectable({
  providedIn: 'root',
})
export class HistoricalQueryService {
  endpoint: string = 'historicalqueries';

  constructor(private http: HttpClient) {}

  getAll(): Observable<HistoricalQueryDTO[]> {
    const url = `${GLOBAL.apiBaseUrl}/${this.endpoint}/users`;
    return this.http.get<HistoricalQueryDTO[]>(url);
  }

  getById(id?: number): Observable<HistoricalQuery> {
    const url = `${GLOBAL.apiBaseUrl}/${this.endpoint}/${id}`;
    return this.http.get<HistoricalQuery>(url);
  }

  add(historicalQuery: HistoricalQuery): Observable<any> {
    const url = `${GLOBAL.apiBaseUrl}/${this.endpoint}`;
    return this.http.post<any>(url, historicalQuery);
  }

  update(historicalQuery: HistoricalQuery): Observable<any> {
    const url = `${GLOBAL.apiBaseUrl}/${this.endpoint}/${historicalQuery.id}`;
    return this.http.put<any>(url, historicalQuery);
  }

  delete(id: number): Observable<any> {
    const url = `${GLOBAL.apiBaseUrl}/${this.endpoint}/${id}`;
    return this.http.delete<any>(url);
  }
}

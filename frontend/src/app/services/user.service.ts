import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { GLOBAL } from '../configuration/configuration.global';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  endpoint = 'users';

  constructor(private http: HttpClient) {}

  getAll(): Observable<User[]> {
    const url = `${GLOBAL.apiBaseUrl}/${this.endpoint}`;
    return this.http.get<User[]>(url);
  }

  getById(id?: number): Observable<User> {
    const url = `${GLOBAL.apiBaseUrl}${this.endpoint}/${id}`;
    return this.http.get<User>(url);
  }

  add(user: User): Observable<any> {
    const url = `${GLOBAL.apiBaseUrl}${this.endpoint}`;
    return this.http.post<any>(url, user);
  }

  update(user: User): Observable<any> {
    const url = `${GLOBAL.apiBaseUrl}${this.endpoint}/${user.id}`;
    return this.http.put<any>(url, user);
  }

  delete(id: number): Observable<any> {
    const url = `${GLOBAL.apiBaseUrl}${this.endpoint}/${id}`;
    return this.http.delete<any>(url);
  }
}

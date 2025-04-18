import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { GLOBAL } from '../configuration/configuration.global';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  endpoint: string = 'users';

  private loggedInKey = 'isLoggedIn';
  private userNameKey = 'userName';
  private userIdKey = 'userId';
  private userNameSubject = new BehaviorSubject<string>(''); // BehaviorSubject to store the user name
  userName$ = this.userNameSubject.asObservable(); // expose the observable for other components to subscribe to

  constructor(private http: HttpClient) {
    // Only access localStorage if we are in the browser
    if (typeof window !== 'undefined') {
      const storedUserName = localStorage.getItem(this.userNameKey);
      if (storedUserName) {
        this.userNameSubject.next(storedUserName);
      }
    }
  }

  login(username: string, password: string): Observable<any> {
    const url = `${GLOBAL.apiBaseUrl}/${this.endpoint}/login`;

    const body = {
      usr: username,
      psw: password,
    };

    return this.http.post(url, body).pipe(
      tap((res: any) => {
        localStorage.setItem(this.loggedInKey, 'true');
        localStorage.setItem(this.userNameKey, username);
        localStorage.setItem(this.userIdKey, res.id);
        this.userNameSubject.next(username);
      })
    );
  }

  logout() {
    // Only interact with localStorage if in the browser
    if (typeof window !== 'undefined') {
      localStorage.removeItem(this.loggedInKey);
      localStorage.removeItem(this.userNameKey);
      this.userNameSubject.next('');
    }
  }

  getUserId(): number | null {
    const storedId = localStorage.getItem(this.userIdKey);
    return storedId ? +storedId : null;
  }

  isLoggedIn(): boolean {
    // Only interact with localStorage if in the browser
    if (typeof window !== 'undefined') {
      return localStorage.getItem(this.loggedInKey) === 'true';
    }
    return false;
  }
}

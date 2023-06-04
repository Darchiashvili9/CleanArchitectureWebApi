import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { User } from '../models';
import { loginRequest, registerRequest } from '../models/authentication';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<any>;
  public currentUser$: Observable<User>;
  public log: loginRequest = new loginRequest();
  public reg: registerRequest = new registerRequest();

  constructor(private http: HttpClient, private router: Router) {
    // Check if localStorage has item currentUser then parse it into an object or return null
    this.currentUserSubject = new BehaviorSubject<User>(
      localStorage.getItem('currentUser') ? JSON.parse(localStorage.getItem('currentUser') || '{}') : null);
    this.currentUser$ = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(email: string, password: string): Observable<void> {

    this.log.email = email;
    this.log.password = password;

    return this.http.post<any>(`${environment.apiUrl}/auth/login`, this.log)
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        return user;
      }));
  }

  logout(): void {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    this.router.navigate(['/login']);
  }

  register(username: string, email: string, password: string): Observable<void> {

    this.reg.userName = username;
    this.reg.email = email;
    this.reg.password = password;

    return this.http.post<any>(`${environment.apiUrl}/auth/register`, this.reg);
  }
}

import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from "@angular/common/http"
import { BehaviorSubject, map, Observable, ReplaySubject } from 'rxjs';
import { User } from '../_models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private userSubject = new ReplaySubject<User | undefined>(1);
  public user$ = this.userSubject.asObservable();

  constructor(private router: Router, private http: HttpClient) {
    if (localStorage.getItem('user')) {
      this.setCurrentUser(JSON.parse(localStorage.getItem('user')!));
    }
    else {
      this.setCurrentUser(undefined);
    }
  }

  login(user: any) {
    return this.http.post<User>(`${this.baseUrl}account/login`, user)
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('user', JSON.stringify(user));
        this.setCurrentUser(user);
        return user;
      }));
  }

  register(user: User) {
    return this.http.post(`${environment.apiUrl}account/register`, user);
  }

  logout() {
    // remove user from local storage and set current user to null
    localStorage.removeItem('user');
    this.setCurrentUser(undefined);
    this.router.navigate(['/account/login']);
  }

  setCurrentUser(user: User | undefined) {
    this.userSubject.next(user);
  }

  getAll() {
    return this.http.get<User[]>(`${environment.apiUrl}users`);
  }

  approveUser(username: string) {
    return this.http.put(`${environment.apiUrl}account/approve/${username}`, username);
  }

  delete(username: string) {
    return this.http.put(`${environment.apiUrl}account/approve/${username}`, username);
  }
}

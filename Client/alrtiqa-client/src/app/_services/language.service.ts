import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  private subject = new BehaviorSubject<string>("en");
  constructor() { }

  setLanguage(message: string) {
    this.subject.next(message);
  }

  getLanguage(): Observable<string> {
    return this.subject.asObservable();
  }
}

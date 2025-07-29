import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class QuizService {
  private gameModeSubject: BehaviorSubject<string>;
  public gameMode$: Observable<string>;

  constructor(private http: HttpClient) {
    this.gameModeSubject = new BehaviorSubject<string>(localStorage.getItem('gameMode') || 'BINARY');
    this.gameMode$ = this.gameModeSubject.asObservable();
  }
  /**
   * Fetches a new quiz question from the API.
   * @returns An observable containing the quiz question data.
   */
  questionGenerator(): Observable<any> {
    return this.http.get(`${environment.apiUrl}/Quiz/GetRandomQuestion`);
  }


  selectGameMode(mode: string): void {
    localStorage.setItem('gameMode', mode.toString());
    this.gameModeSubject.next(mode);
  }
}

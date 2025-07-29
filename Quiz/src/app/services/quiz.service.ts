import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import { quote, AnswerDataModel } from '../models/quote';

@Injectable({
  providedIn: 'root'
})
export class QuizService {
  private gameModeSubject: BehaviorSubject<string>;
  public gameMode$: Observable<string>;
  public log: quote = new quote();


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

    //test
  //  return this.http.get(`${environment.apiUrl}/Quiz/GetTestDeutschland`);
  }

  addQuote(quoteText: string, answers: AnswerDataModel[]): Observable<any> {
    this.log.quoteText = quoteText;
    this.log.answers = answers;

    return this.http.post<any>(`${environment.apiUrl}/Quote/add`, this.log);
  }


  selectGameMode(mode: string): void {
    localStorage.setItem('gameMode', mode.toString());
    this.gameModeSubject.next(mode);
  }
}

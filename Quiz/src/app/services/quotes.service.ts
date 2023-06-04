import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';

const quotesManagerUrl = `${environment.apiUrl}/QuoteManager`;

@Injectable({
  providedIn: 'root'
})
export class QuotesService {

  constructor(private http: HttpClient) { }

  getQuotes(): Observable<any> {
    return this.http.get(quotesManagerUrl);
  }

  createQuote(data: any): Observable<any> {
    return this.http.post(quotesManagerUrl, data);
  }

  editQuote(data: any): Observable<any> {
    return this.http.put(quotesManagerUrl, data);
  }

  deleteQuote(quoteId: number): Observable<any> {
    return this.http.delete(quotesManagerUrl, { params: {quoteId: quoteId.toString()} } );
  }
}

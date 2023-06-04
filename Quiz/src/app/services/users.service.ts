import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';

const usersManagerUrl = `${environment.apiUrl}/UsersManager`;

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) { }

  getUsers(): Observable<any> {
    return this.http.get(usersManagerUrl);
  }

  createUser(data: any): Observable<any> {
    return this.http.post(usersManagerUrl, data);
  }

  editUser(data: any): Observable<any> {
    return this.http.put(usersManagerUrl, data);
  }

  deleteUser(userId: string): Observable<any> {
    return this.http.delete(usersManagerUrl, { params: {userId} } );
  }
}

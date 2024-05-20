import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  headers = new HttpHeaders();

  constructor(private http: HttpClient) {
    this.headers = this.headers.set('auth_token', '2687C7D9CE6F');
  }

  public getManyWithParams<T>(entity: string, params?: HttpParams): Observable<T[]> {
    return this.http.get<T[]>(`${environment.baseApiUrl}${entity}`, { params: params, headers: this.headers });
  }

  public getMany<T>(entity: string): Observable<T[]> {
    return this.http.get<T[]>(`${environment.baseApiUrl}${entity}`, { headers: this.headers });
  }
}
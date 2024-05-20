import { Attendance } from '../models/attendance.model';
import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AttendanceService {

  constructor(
    private api: ApiService
  ) { }

  fetchAttendances(productCode?: string, seasonId?: number, gameId?: number): Observable<Attendance[]> {
    let queryParams = new HttpParams();

    if (productCode) {
      queryParams = queryParams.append('productCode', productCode);
    }

    if (seasonId != null && gameId != null) {
      queryParams = queryParams.append('seasonId', seasonId);
      queryParams = queryParams.append('gameId', gameId);
    }

    return this.api.getManyWithParams<Attendance>('Attendance', queryParams);
  }
}
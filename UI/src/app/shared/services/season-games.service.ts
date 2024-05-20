import { SeasonGames } from '../models/season-games.model';
import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SeasonGamesService {

  constructor(
    private api: ApiService
  ) { }

  fetchSeasonGames(): Observable<SeasonGames[]> {
    return this.api.getMany<SeasonGames>('SeasonGames');
  }
}
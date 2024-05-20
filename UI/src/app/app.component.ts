import { Attendance, AttendanceType } from "./shared/models/attendance.model";
import { SeasonGamesService } from "./shared/services/season-games.service";
import { AttendanceService } from "./shared/services/attendance.service";
import { Game, SeasonGames } from "./shared/models/season-games.model";
import { Observable, debounceTime, map, switchMap } from "rxjs";
import { MatTableDataSource } from "@angular/material/table";
import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { FormControl } from "@angular/forms";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
})
export class AppComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  displayedColumns: string[] = ['attendanceType', 'memberId', 'barcode'];
  dataSource = new MatTableDataSource<Attendance>();

  searchBy = new FormControl('1');
  seasonId = new FormControl(null);
  gameId = new FormControl(null);
  productCode = new FormControl(null);

  seasonGamesData$: Observable<SeasonGames[]>;
  gamesData$: Observable<Game[]>;

  constructor(
    private attendanceService: AttendanceService,
    private seasonGamesService: SeasonGamesService,
  ) { }

  ngOnInit(): void {
    // get seasons with games data
    this.setSeasonGamesData();
    // get attendance data according to filters
    this.filterAttendanceData();
    // sets up a subscription to changes in the productCode form control.
    this.productCode.valueChanges.pipe(
      debounceTime(500),
      switchMap(async () => this.filterAttendanceData())
    ).subscribe();
    // sets up a subscription to changes in the gameId form control.
    this.gameId.valueChanges.pipe(
      switchMap(async () => this.filterAttendanceData())
    ).subscribe();
  }

  // ============================== Get Attendances ============================== //

  // fetch the filtered attendance data
  filterAttendanceData() {
    this.fetchAttendanceData().subscribe((res: Attendance[]) => {
      this.dataSource.data = res;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  // encapsulates the logic of actual API call to fetch the attendance data based on the filters
  fetchAttendanceData(): Observable<Attendance[]> {
    return this.attendanceService
      .fetchAttendances(this.productCode.value, this.seasonId.value, this.gameId.value);
  }

  // ============================== Get Seasons And Games ============================== //

  // encapsulates the logic of actual API call to fetch the seasons games data
  fetchSeasonGamesData(): Observable<SeasonGames[]> {
    return this.seasonGamesService
      .fetchSeasonGames();
  }
  // set the observable that seasons game data
  setSeasonGamesData() {
    this.seasonGamesData$ = this.fetchSeasonGamesData();
  }
  // get games by season id
  fetchGamesBySeasonId(seasonId: number): Observable<Game[]> {
    return this.seasonGamesData$.pipe(
      map(seasons => {
        const selectedSeason = seasons.find(season => season.id === seasonId);
        return selectedSeason ? selectedSeason.games : [];
      })
    );
  }
  // set the observable that hold games data
  onSeasonSelectionChange(event: any) {
    this.gamesData$ = this.fetchGamesBySeasonId(event.value);
  }

  // ============================== Get Attendance Type Name ============================== //

  // get attendance type name
  mapAttendanceTypeToString(type: AttendanceType): string {
    switch (type) {
      case AttendanceType.SeasonTicket:
        return 'Season Ticket';
      case AttendanceType.GameTicket:
        return 'Game Ticket';
      default:
        return '';
    }
  }

  // ============================== Set Filtering Type ============================== //

  // on change filter type (All, Product Code, Game Id, And Season Id)
  onChangeSearchType() {
    this.productCode.setValue(null);
    this.gameId.setValue(null);
    this.seasonId.setValue(null);
    this.onSeasonSelectionChange(0);
  }
}
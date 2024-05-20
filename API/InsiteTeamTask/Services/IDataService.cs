using System.Collections.Generic;
using InsiteTeamTask.API.Models;
using InsiteTeamTask.Models;

namespace InsiteTeamTask.Services
{
    public interface IDataService
    {
		IEnumerable<Attendance> GetAttendance();

		IEnumerable<Attendance> GetAttendanceForGame(int? seasonId, int? gameId);

		IEnumerable<Attendance> GetAttendanceForProduct(string productCode);

        IEnumerable<SeasonGames> GetSeasonsWithGames();
	}
}
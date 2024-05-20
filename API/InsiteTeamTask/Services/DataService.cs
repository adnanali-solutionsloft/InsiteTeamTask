using InsiteTeamTask.Data.Providers;
using System.Collections.Generic;
using InsiteTeamTask.Data.Models;
using InsiteTeamTask.Data.Store;
using InsiteTeamTask.API.Models;
using InsiteTeamTask.Models;
using System.Linq;

namespace InsiteTeamTask.Services
{
	public class DataService : IDataService
	{
		private readonly IDataProvider _dataProvider;

		public DataService(IDataProvider dataProvider)
		{
			_dataProvider = dataProvider;
		}

		/// <summary>
		/// Retrieves attendance records
		/// </summary>
		/// <returns>Returns a list of attendance records.</returns>
		public IEnumerable<Attendance> GetAttendance()
		{
			var gameTickets = GetTickets();

			var seasonTickets = GetSeasonTickets();

			var allAttendances = gameTickets.Union(seasonTickets).ToList();

			return allAttendances;
		}

		/// <summary>
		/// Retrieves attendance records for specific season and game
		/// </summary>
		/// /// <param name="seasonId"></param>
		/// <param name="gameId"></param>
		/// <returns>Returns a list of attendance records.</returns>
		public IEnumerable<Attendance> GetAttendanceForGame(int? seasonId, int? gameId)
		{
			var gameTicketsAttendances = GetTickets(null, seasonId, gameId);

			return gameTicketsAttendances;
		}

		/// <summary>
		/// Retrieves attendance records for specific product
		/// </summary>
		/// <param name="productCode"></param>
		/// <returns>Returns a list of attendance records.</returns>
		public IEnumerable<Attendance> GetAttendanceForProduct(string productCode)
		{
			var gameTickets = GetTickets(productCode);

			var seasonTickets = GetSeasonTickets(productCode);

			var allAttendances = gameTickets.Union(seasonTickets).ToList();

			return allAttendances;
		}

		/// <summary>
		/// Retrieves seasons with games
		/// </summary>
		/// <returns>Returns a list of season records with games.</returns>
		public IEnumerable<SeasonGames> GetSeasonsWithGames()
		{
			var seasonsWithGames = DataStore.Seasons.Select(season => new SeasonGames
			{
				Id = season.Id,
				Name = season.Name,
				Games = DataStore.Games.Where(game => game.SeasonId == season.Id).ToList()
			}).ToList();

			return seasonsWithGames;
		}

		private IEnumerable<Attendance> GetTickets(string productCode = null, int? seasonId = null, int? gameId = null)
		{
			var gameTickets = from ticket in _dataProvider.GetTickets()
							  join product in _dataProvider.GetProducts() on ticket.ProductId equals product.Id
							  where (seasonId == null || product.SeasonId == seasonId) &&
									(gameId == null || product.GameId == gameId) &&
									(productCode == null || product.Id.ToLower().Contains(productCode.ToLower())) &&
									(product.Type == ProductType.Ticket)

							  select new Attendance
							  {
								  AttendanceType = AttendanceType.GameTicket,
								  Barcode = ticket.Barcode,
								  MemberId = 0
							  };

			return gameTickets.ToList();
		}

		private IEnumerable<Attendance> GetSeasonTickets(string productCode = null)
		{
			var seasonTickets = from member in _dataProvider.GetMembers()
								join product in _dataProvider.GetProducts() on member.ProductId equals product.Id
								where (productCode == null || product.Id.ToLower().Contains(productCode.ToLower())) &&
									(product.Type == ProductType.Member)

								select new Attendance
								{
									AttendanceType = AttendanceType.SeasonTicket,
									Barcode = string.Empty,
									MemberId = member.Id
								};

			return seasonTickets.ToList();
		}
	}
}
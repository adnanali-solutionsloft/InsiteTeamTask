using System.Collections.Generic;
using InsiteTeamTask.API.Models;
using Microsoft.AspNetCore.Mvc;
using InsiteTeamTask.Services;

namespace InsiteTeamTask.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SeasonGamesController : ControllerBase
	{
		private readonly IDataService _service;

		public SeasonGamesController(IDataService service)
		{
			_service = service;
		}

		/// <summary>
		/// Retrieves seasons with games
		/// </summary>
		/// <returns>Returns a list of season records with games.</returns>
		[HttpGet]
		public IEnumerable<SeasonGames> GetSeasonsWithGames()
		{
			return _service.GetSeasonsWithGames();
		}
	}
}
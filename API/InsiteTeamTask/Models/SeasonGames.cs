using InsiteTeamTask.Data.Models;
using System.Collections.Generic;

namespace InsiteTeamTask.API.Models
{
	public class SeasonGames
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Game> Games { get; set; }
	}
}
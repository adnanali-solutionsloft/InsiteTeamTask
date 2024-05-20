using Microsoft.AspNetCore.Mvc;
using InsiteTeamTask.Services;

namespace InsiteTeamTask.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AttendanceController : ControllerBase
	{
		private readonly IDataService _service;

		public AttendanceController(IDataService service)
		{
			_service = service;
		}

		/// <summary>
		/// Retrieves attendance records based on the provided query parameters.
		/// </summary>
		/// <param name="productCode"></param>
		/// <param name="seasonId"></param>
		/// <param name="gameId"></param>
		/// <returns>Returns a list of attendance records matching the query parameters.</returns>
		/// <returns>If no parameters are provided, returns all attendance records.</returns>
		[HttpGet]
		public ActionResult Get([FromQuery] string productCode, [FromQuery] int? seasonId, [FromQuery] int? gameId)
		{
			if (!string.IsNullOrEmpty(productCode))
			{
				var attendance = _service.GetAttendanceForProduct(productCode);

				return Ok(attendance);
			}

			if (seasonId.HasValue && gameId.HasValue)
			{
				var attendance = _service.GetAttendanceForGame(seasonId.Value, gameId.Value);

				return Ok(attendance);
			}

			var allAttendance = _service.GetAttendance();
			return Ok(allAttendance);
		}
	}
}
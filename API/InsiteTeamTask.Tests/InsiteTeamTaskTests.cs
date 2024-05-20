using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsiteTeamTask.Data.Providers;
using InsiteTeamTask.Services;

namespace InsiteTeamTask.Tests
{
	[TestClass]
	public class InsiteTeamTaskTests
	{
		private DataProvider mockDataProvider;
		private DataService mockDataService;

		public InsiteTeamTaskTests()
		{
			mockDataProvider = new DataProvider();
			mockDataService = new DataService(mockDataProvider);
		}

		[TestMethod]
		public void GetAttendanceTestMethod()
		{
			// Act
			var attendance = mockDataService.GetAttendance();

			// Assert
			Assert.IsNotNull(attendance);
		}

		[TestMethod]
		[DataRow(19, 4)]
		public void GetAttendanceForGameTestMethod(int? seasonId, int? gameId)
		{
			// Act
			var attendance = mockDataService.GetAttendanceForGame(seasonId, gameId);

			// Assert
			Assert.IsNotNull(attendance);
		}

		[TestMethod]
		[DataRow("CH5490")]
		public void GetAttendanceForProductTestMethod(string productCode)
		{
			// Act
			var attendance = mockDataService.GetAttendanceForProduct(productCode);

			// Assert
			Assert.IsNotNull(attendance);
		}

		[TestMethod]
		public void GetSeasonsWithGamesTestMethod()
		{
			// Act
			var seasonsGame = mockDataService.GetSeasonsWithGames();

			// Assert
			Assert.IsNotNull(seasonsGame);
		}
	}
}
using AirboxDemo.Api.Controllers;
using AirboxDemo.Api.Models;
using AirboxDemo.Domain.DataLayer;
using AirboxDemo.Domain.Models;
using AirboxDemo.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace AirboxDemo.Tests.Api.Controllers
{
    public class LocationControllerTests
    {
        private readonly LocationController _target;

        private readonly Mock<IGeolocationApiService> _mockGeolocationService;
        private readonly Mock<ILocationDataAccess> _mockLocationDataAccess;

        public LocationControllerTests()
        {
            this._mockGeolocationService = new Mock<IGeolocationApiService>(MockBehavior.Strict);
            this._mockLocationDataAccess = new Mock<ILocationDataAccess>(MockBehavior.Strict);

            this._target = new LocationController(this._mockGeolocationService.Object,
                this._mockLocationDataAccess.Object);
        }

        [Fact]
        public void GetUsersInArea_InvalidUserId_ReturnsBadRequestResult()
        {
            // Arrange
            var request = new GetUsersInAreaRequest
            {
                UserId = 0
            };

            // Act
            var actual = this._target.GetUsersInArea(request);

            // Assert
            actual.ShouldBeOfType<BadRequestResult>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-100)]
        public void GetUsersInArea_UnauthorisedUserId_ReturnsForbidResult(int invalidUserId)
        {
            // Arrange
            var request = new GetUsersInAreaRequest
            {
                UserId = invalidUserId
            };

            // Act
            var actual = this._target.GetUsersInArea(request);

            // Assert
            actual.ShouldBeOfType<ForbidResult>();
        }

        [Fact]
        public void GetUsersInArea_AreaCoordinatesSupplied_ReturnsOkObjectResult()
        {
            // Arrange
            var areaCoordinates = new AreaCoordinates();
            var request = new GetUsersInAreaRequest
            {
                UserId = 123,
                AreaCoordinates = areaCoordinates,
                AreaName = "Area"
            };

            var expectedResult = new List<UserLocation>();

            this._mockLocationDataAccess.Setup(lda => lda.GetCurrentLocationForAllUsersByAreaCoordinates(It.IsAny<AreaCoordinates>())).Returns(expectedResult);

            // Act
            var actual = this._target.GetUsersInArea(request);

            // Assert
            var result = actual.ShouldBeOfType<OkObjectResult>();

            result.Value.ShouldBe(expectedResult);

            this._mockLocationDataAccess.Verify(lda => lda.GetCurrentLocationForAllUsersByAreaCoordinates(areaCoordinates), Times.Once);
        }

        [Fact]
        public void GetUsersInArea_AreaCoordinatesNotSupplied_ReturnsOkObjectResult()
        {
            // Arrange
            var request = new GetUsersInAreaRequest
            {
                UserId = 123,
                AreaName = "Area"
            };

            var expectedResult = new List<UserLocation>();

            this._mockLocationDataAccess.Setup(lda => lda.GetCurrentLocationForAllUsersByAreaName(It.IsAny<string>())).Returns(expectedResult);

            // Act
            var actual = this._target.GetUsersInArea(request);

            // Assert
            var result = actual.ShouldBeOfType<OkObjectResult>();

            result.Value.ShouldBe(expectedResult);

            this._mockLocationDataAccess.Verify(lda => lda.GetCurrentLocationForAllUsersByAreaName("Area"), Times.Once);
        }
    }
}

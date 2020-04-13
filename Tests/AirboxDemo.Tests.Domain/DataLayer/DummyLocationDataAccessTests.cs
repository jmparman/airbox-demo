using AirboxDemo.Domain.DataLayer;
using AirboxDemo.Domain.Models;
using Shouldly;
using System;
using Xunit;

namespace AirboxDemo.Tests.Domain.DataLayer
{
    public class DummyLocationDataAccessTests
    {
        private readonly DummyLocationDataAccess _target;
        
        public DummyLocationDataAccessTests()
        {
            this._target = new DummyLocationDataAccess();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-123456)]
        public void SetCurrentLocation_InvalidUserId_ThrowsException(int invalidUserId)
        {
            // Arrange
            var userLocation = new UserLocation
            {
                UserId = invalidUserId,
                Longitude = 0,
                Latitude = 0,
                Area = "Area"
            };

            // Act
            // Assert
            Should.Throw<ArgumentException>(() => this._target.SetCurrentLocation(userLocation));
        }

        [Theory]
        [InlineData(-180.0001)]
        [InlineData(-181)]
        [InlineData(180.0001)]
        [InlineData(181)]
        public void SetCurrentLocation_InvalidLongitude_ThrowsException(double invalidLongitude)
        {
            // Arrange
            var userLocation = new UserLocation
            {
                UserId = 10,
                Longitude = invalidLongitude,
                Latitude = 0,                
                Area = "Area"
            };

            // Act
            // Assert
            Should.Throw<ArgumentException>(() => this._target.SetCurrentLocation(userLocation));
        }

        [Theory]
        [InlineData(-90.0001)]
        [InlineData(-91)]
        [InlineData(90.0001)]
        [InlineData(91)]
        public void SetCurrentLocation_InvalidLatitude_ThrowsException(double invalidLatitude)
        {
            // Arrange
            var userLocation = new UserLocation
            {
                UserId = 10,
                Longitude = 0,
                Latitude = invalidLatitude,
                Area = "Area"
            };

            // Act
            // Assert
            Should.Throw<ArgumentException>(() => this._target.SetCurrentLocation(userLocation));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SetCurrentLocation_InvalidArea_ThrowsException(string invalidArea)
        {
            // Arrange
            var userLocation = new UserLocation
            {
                UserId = 42,
                Longitude = 0,
                Latitude = 0,
                Area = invalidArea
            };

            // Act
            // Assert
            Should.Throw<ArgumentException>(() => this._target.SetCurrentLocation(userLocation));
        }

        [Fact]
        public void SetCurrentLocation_ValidInput_ReturnsTrue()
        {
            // Arrange
            var userLocation = new UserLocation
            {
                UserId = 42,
                Longitude = 0,
                Latitude = 0,
                Area = "Area"
            };

            // Act
            var actual = this._target.SetCurrentLocation(userLocation);

            // Assert
            actual.ShouldBeTrue();
        }
    }
}

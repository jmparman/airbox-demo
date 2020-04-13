using AirboxDemo.Api.Models;
using AirboxDemo.Domain.DataLayer;
using AirboxDemo.Domain.Models;
using AirboxDemo.Service;
using AirboxDemo.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AirboxDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IGeolocationApiService _geolocationService;
        private readonly ILocationDataAccess _locationDataAccess;

        public LocationController(IGeolocationApiService geolocationService, ILocationDataAccess locationDataAccess)
        {
            this._geolocationService = geolocationService;
            this._locationDataAccess = locationDataAccess;
        }

        [HttpGet("all-users-current/{id}")]
        [ProducesResponseType(typeof(IEnumerable<UserLocation>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetCurrentLocationForAllUsers(int id)
        {
            // make sure it's a valid id
            if (id == 0)
            {
                return this.BadRequest();
            }

            // make sure it's authorised to do what it's trying 
            if (id < 0)
            {
                return this.Forbid();
            }

            var userLocations = this._locationDataAccess.GetCurrentLocationForAllUsers();

            return this.Ok(userLocations);
        }

        [HttpGet("user-current-location/{id}")]
        [ProducesResponseType(typeof(UserLocation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetCurrentLocationForUser(int id)
        {
            // make sure it's a valid id
            if (id == 0)
            {
                return this.BadRequest();
            }

            // make sure it's authorised to do what it's trying 
            if (id < 0)
            { 
                return this.Forbid();
            }

            // make sure it exists in the store
            if (id > 10)
            {
                return this.NotFound();
            } 

            var userLocation = this._locationDataAccess.GetCurrentLocationForUser(id);

            return this.Ok(userLocation);
        }

        [HttpGet("user-location-history/{id}")]
        [ProducesResponseType(typeof(IEnumerable<UserLocation>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetUserLocationHistory(int id)
        {
            // make sure it's a valid id
            if (id == 0)
            {
                return this.BadRequest();
            }

            // make sure it's authorised to do what it's trying 
            if (id < 0)
            {
                return this.Forbid();
            }

            var locationHistory = this._locationDataAccess.GetLocationHistoryForUser(id);

            return this.Ok(locationHistory);
        }

        [HttpGet("users-in-area")]
        [ProducesResponseType(typeof(IEnumerable<UserLocation>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetUsersInArea([FromBody] GetUsersInAreaRequest usersInAreaRequest)
        {
            // make sure it's a valid id
            if (usersInAreaRequest.UserId == 0)
            {
                return this.BadRequest();
            }

            // check other params are valid

            // make sure it's authorised to do what it's trying 
            if (usersInAreaRequest.UserId < 0)
            {
                return this.Forbid();
            }

            if (usersInAreaRequest.AreaCoordinates != null)
            {
                return this.Ok(this._locationDataAccess.GetCurrentLocationForAllUsersByAreaCoordinates(usersInAreaRequest.AreaCoordinates));
            }

            return this.Ok(this._locationDataAccess.GetCurrentLocationForAllUsersByAreaName(usersInAreaRequest.AreaName));
        }

        [HttpPost("set-current-location")]
        [ProducesResponseType(typeof(UserLocation), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult SetCurrentLocation([FromBody] SetCurrentLocationRequest currentLocationRequest)
        {
            // make sure it's valid
            if (currentLocationRequest.UserId == 0)
            {
                return this.BadRequest();
            }

            // check other params are valid

            // make sure it's authorised to do what it's trying 
            if (currentLocationRequest.UserId < 0)
            {
                return this.Forbid();
            }

            // map the address into something the  service knows about - automapper
            var locationAddress = new LocationAddress
            {
                Street = currentLocationRequest.CurrentLocation.AddressLine1,
                Town = currentLocationRequest.CurrentLocation.Town,
                PostCode = currentLocationRequest.CurrentLocation.PostCode,
                Country = currentLocationRequest.CurrentLocation.Country
            };

            var currLoc = this._geolocationService.GetLatLongForLocation(locationAddress);
            var userLoc = new UserLocation
            {
                UserId = currentLocationRequest.UserId,
                Longitude = currLoc.Item1,
                Latitude = currLoc.Item2,                
                Area = currentLocationRequest.Area
            };

            var savedSuccessfully = this._locationDataAccess.SetCurrentLocation(userLoc);

            if (savedSuccessfully)
            {
                return this.CreatedAtAction(
                "GetCurrentLocationForUser",
                new { id = userLoc.UserId },
                userLoc);
            }

            this.ModelState.AddModelError("SetCurrentLocation", "Something went wrong trying to save the current location");

            var badRequestResponse = new BadRequestResponse(this.ModelState);

            return this.BadRequest(badRequestResponse);
        }
    }
}

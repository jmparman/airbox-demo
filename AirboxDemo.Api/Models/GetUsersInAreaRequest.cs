using AirboxDemo.Domain.Models;

namespace AirboxDemo.Api.Models
{
    public class GetUsersInAreaRequest
    {
        public int UserId { get; set; }
        public string AreaName { get; set; }
        public AreaCoordinates AreaCoordinates {get; set; }
    }
}

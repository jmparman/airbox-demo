namespace AirboxDemo.Api.Models
{
    public class SetCurrentLocationRequest
    {
        public int UserId { get; set; }
        public Address CurrentLocation { get; set; }
        public string Area { get; set; }
    }
}

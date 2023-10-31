using Database.Models;

namespace ScieenceAPI.Models
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public Response Favourites { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}

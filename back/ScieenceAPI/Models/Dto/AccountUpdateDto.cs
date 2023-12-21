using Database.Models;

namespace ScieenceAPI.Models.Dto
{
    public class AccountUpdateDto
    {
        public string Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public List<DbPublication> Favourites { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}

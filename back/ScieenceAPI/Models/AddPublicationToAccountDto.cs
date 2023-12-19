using Database.Models;

namespace ScieenceAPI.Models
{
    public class AddPublicationToAccountDto
    {
        public AccountUpdateDto account {  get; set; }
        public Publication publicationToAdd { get; set; }
    }
}

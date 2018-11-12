using Microsoft.AspNetCore.Identity;

namespace EasyNote.Core.Model.DbEntities
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? FacebookId { get; set; }
        public string PictureUrl { get; set; }
    }
}

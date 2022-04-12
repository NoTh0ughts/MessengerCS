using System.Collections.Generic;

#nullable disable

namespace Data.Entity
{
    public partial class User
    {
        public User()
        {
            Dialogs = new HashSet<Dialog>();
            UserAccesses = new HashSet<UserAccess>();
            Values = new HashSet<Value>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Avatar { get; set; }
        public int? BotId { get; set; }
        public string Password { get; set; }

        public virtual Bot Bot { get; set; }
        public virtual ICollection<Dialog> Dialogs { get; set; }
        public virtual ICollection<UserAccess> UserAccesses { get; set; }
        public virtual ICollection<Value> Values { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Messenger.DB.entity
{
    public partial class Dialog
    {
        public Dialog()
        {
            Calls = new HashSet<Call>();
            UserAccesses = new HashSet<UserAccess>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdAdmin { get; set; }
        public string RefAvatar { get; set; }

        public virtual User IdAdminNavigation { get; set; }
        public virtual ICollection<Call> Calls { get; set; }
        public virtual ICollection<UserAccess> UserAccesses { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Messenger.DB.entity
{
    public partial class UserAccess
    {
        public UserAccess()
        {
            Messages = new HashSet<Message>();
        }

        public int UserId { get; set; }
        public int DialogId { get; set; }
        public int Id { get; set; }

        public virtual Dialog Dialog { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}

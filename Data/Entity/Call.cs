using System;
using System.Collections.Generic;

#nullable disable

namespace Messenger.DB.entity
{
    public partial class Call
    {
        public int Id { get; set; }
        public string RecordRef { get; set; }
        public TimeSpan? Continuously { get; set; }
        public int DialogId { get; set; }

        public virtual Dialog Dialog { get; set; }
    }
}

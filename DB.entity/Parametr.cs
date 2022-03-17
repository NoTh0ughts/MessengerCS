using System;
using System.Collections.Generic;

#nullable disable

namespace Messenger.DB.entity
{
    public partial class Parametr
    {
        public Parametr()
        {
            Values = new HashSet<Value>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Value> Values { get; set; }
    }
}

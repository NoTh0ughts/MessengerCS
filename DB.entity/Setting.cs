using System;
using System.Collections.Generic;

#nullable disable

namespace Messenger.DB.entity
{
    public partial class Setting
    {
        public string Пользователь { get; set; }
        public string Параметр { get; set; }
        public int? IntValue { get; set; }
        public string StringValue { get; set; }
    }
}

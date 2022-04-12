#nullable disable

namespace Data.Entity
{
    public partial class Value
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int? IntValue { get; set; }
        public string StringValue { get; set; }
        public int ParameterId { get; set; }

        public virtual User IdUserNavigation { get; set; }
        public virtual Parameter Parameter { get; set; }
    }
}

namespace Data.Entity
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Token { get; set; }
        
        public bool IsAdmin { get; set; }
    }
}
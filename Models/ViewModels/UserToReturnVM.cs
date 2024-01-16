namespace BookLibrarySoln.Models.ViewModels
{
    public class UserToReturnVM
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>();
        //public string PhotoUrl { get; set; } = string.Empty;

    }
}


namespace BookLibrarySoln.Models.ViewModels
{
    public class ManageUserVM
    {
        public string RoleName { get; set; }
        public List<UserToReturnVM> TableData { get; set; } = new List<UserToReturnVM>();
        public UserToReturnVM UserDetail { get; set; }
    }
}

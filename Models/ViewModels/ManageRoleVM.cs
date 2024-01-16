using System.Runtime.CompilerServices;

namespace BookLibrary.Models.ViewModels
{
    public class ManageRoleVM
    {
        public string RoleName { get; set; }
        public List<RolesToReturnVM> RolesToReturn { get; set; } = new List<RolesToReturnVM>();
    }
}

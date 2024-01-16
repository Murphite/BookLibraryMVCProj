using BookLibrary.Models.ViewModels;
using BookLibrarySoln.Models.Entities;
using BookLibrarySoln.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrarySoln.Controllers
{
    [Authorize(Roles = "admin")]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DashboardController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AcceptVerbs("Post", "Get")]
        public async Task<IActionResult> ManageUser(ManageUserVM model, string? userId)
        {
            if (!string.IsNullOrEmpty(model.RoleName))
            {
                var selectedUser = await _userManager.FindByIdAsync(userId);
                var userRoles = await _userManager.GetRolesAsync(selectedUser);
                if (!userRoles.Any(x => x.ToLower() == model.RoleName.ToLower()))
                {
                    await _userManager.AddToRoleAsync(selectedUser, model.RoleName);
                }
            }

            var users = _userManager.Users;
            var manageUserViewModel = new ManageUserVM();

            if (users != null && users.Any())
            {
                manageUserViewModel.TableData = users.Select(u => new UserToReturnVM
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    //Role = r.Name
                }).ToList();

                if (!string.IsNullOrEmpty(userId))
                {
                    var user = manageUserViewModel.TableData.FirstOrDefault(x => x.Id == userId);
                    if (user != null)
                    {
                        var roleUser = users.First(x => x.Id == userId);
                        var userRoles = await _userManager.GetRolesAsync(roleUser);
                        user.Roles = userRoles.ToList();
                        manageUserViewModel.UserDetail = user;
                    }
                }
            }

            return View(manageUserViewModel);
        }

        [AcceptVerbs("Post", "Get")]
        //[AllowAnonymous]
        public async Task<IActionResult> ManageRole(ManageRoleVM model)
        {
            if (!string.IsNullOrEmpty(model.RoleName))
            {
                var allRoles = _roleManager.Roles;
                if (!allRoles.Any(x => x.Name.ToLower() == model.RoleName.ToLower()))
                {
                    await _roleManager.CreateAsync(new IdentityRole(model.RoleName));
                }
            }

            var roles = _roleManager.Roles;
            var manageRoleViewModel = new ManageRoleVM();

            if (roles != null && roles.Any())
            {
                manageRoleViewModel.RolesToReturn = roles.Select(r => new RolesToReturnVM
                {
                    Id = r.Id,
                    Name = r.Name,
                }).ToList();
            }

            return View(manageRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var deleteResult = await _userManager.DeleteAsync(user);
                if (deleteResult.Succeeded)
                {
                    return RedirectToAction("ManageUser");
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                var deleteResult = await _roleManager.DeleteAsync(role);
                if (deleteResult.Succeeded)
                {
                    return RedirectToAction("ManageRole");
                }
            }

            return View("ManageRole");
        }
    }
}


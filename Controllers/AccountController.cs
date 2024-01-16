using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookLibrarySoln.Services.Interface;
using BookLibrarySoln.Models.ViewModels;
using BookLibrarySoln.Models.Entities;

namespace BookLibrarySoln.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, IEmailService emailService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserToRegisterVM model)
        {
            if (ModelState.IsValid)
            {
                // continue coding
                var user = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email
                };

                var createUserResult = await _userManager.CreateAsync(user, model.Password);
                if (createUserResult.Succeeded)
                {
                    // add role to newly created user
                    var addRoleResult = await _userManager.AddToRoleAsync(user, "regular");
                   
                    if (addRoleResult.Succeeded)
                    {
                        // send email confirmation link
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var link = Url.Action("ConfirmEmail", "Account", new { user.Email, token }, Request.Scheme);
                        var body = $@"Hi {user.FirstName}, please click 
the link <a href='{link}'>here</a> to confirm your account's email";

                        // Log the generated link for debugging
                        Console.WriteLine($"Generated Confirmation Link: {link}");

                        await _emailService.SendEmailAsync(user.Email, "Confirm Email", body);

                        // Log a message for debugging
                        Console.WriteLine($"Redirecting to RegisterCongrats page for user {user.FirstName}");

                        return RedirectToAction("RegisterCongrats", "Account", new { name = user.FirstName });
                    }

                    foreach (var err in addRoleResult.Errors)
                    {
                        ModelState.AddModelError(err.Code, err.Description);
                    }
                }


            }

            return View(model);
        }

        [HttpGet]
        public IActionResult RegisterCongrats(string name)
        {
            ViewBag.Name = name;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string Email, string token)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user != null)
            {
                var confirmEmailResult = await _userManager.ConfirmEmailAsync(user, token);
                if (confirmEmailResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in confirmEmailResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return View(ModelState);
            }

            ModelState.AddModelError("", "Email confirmation failed");
            return View(ModelState);
        }

        [HttpGet]
        public IActionResult Login(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserToLoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (await _userManager.IsEmailConfirmedAsync(user))
                    {
                        var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (loginResult.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid credentials");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email not confirmed yet!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid credentials");
                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync((string)model.Email);
                if (user != null)
                {
                    //send reset password link
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var link = Url.Action("ResetPassword", "Account", new { user.Email, token }, Request.Scheme);
                    var body = $@"Hi {user.FirstName}, You appear to have forgotten your password, click the link <a href='{link}'>here</a> to reset password";
                    await _emailService.SendEmailAsync(user.Email, "Forgot Password", body);

                    ViewBag.Message = "Reset password link has been sent to the email provided. If correct, you should already have gotten it by now";
                    return View("ResetPasswordEmailSent", "Account");
                }

                ModelState.AddModelError("", "Invalid Email");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string Email, string token)
        {
            var resetPasswordModel = new ResetPasswordVM { Email = Email, Token = token };
            return View(resetPasswordModel);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var resetPasswordResult = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                    if (resetPasswordResult.Succeeded)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        foreach (var error in resetPasswordResult.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                        return View(model);

                    }

                }
                ModelState.AddModelError("", "Invalid Email");


            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}

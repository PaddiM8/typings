using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Typings.Models;

namespace Typings.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager,
                                 IEmailSender emailSender,
                                 IWebHostEnvironment environment,
                                 ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _environment = environment;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        
        public IActionResult Overview()
        {
            return View();
        }
        
        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult EmailConfirmedSuccessFully()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ResendConfirmationEmail()
        {
            return View();
        }
        
        [AllowAnonymous]
        public IActionResult EmailConfirmationPrompt(string? userId = null, string? code = null)
        {
            ViewData["userId"] = userId;
            ViewData["code"] = code;
            
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, true);
            if (result.Succeeded)
            {
                _logger.LogInformation($"Logged in as {model.Username}.");
                
                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                _logger.LogInformation("Locked out.");
                ModelState.AddModelError("Username", "Too many failed login attempts. Temporarily locked out.");

                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            if (!user.EmailConfirmed)
            {
                await SendConfirmationEmail(user);

                return _environment.IsProduction()
                    ? RedirectToAction("EmailConfirmationPrompt")
                    : RedirectToAction("EmailConfirmationPrompt", new
                    {
                        userId = await _userManager.GetUserIdAsync(user),
                        code = await _userManager.GenerateEmailConfirmationTokenAsync(user)
                    });
            }
            
            _logger.LogInformation("Invalid login attempt.");
            ModelState.AddModelError("Username", "Invalid login attempt.");

            return View(model);
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation( $"Created user: {model.Username}.");
                
                if (_environment.IsProduction())
                {
                    await SendConfirmationEmail(user);
                    
                    return RedirectToAction("EmailConfirmationPrompt");
                }
                
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                
                return RedirectToAction("EmailConfirmationPrompt", new { userId, code });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("Username", error.Description);
                _logger.LogInformation( $"Failed to create user: {error.Description}.");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResendConfirmationEmail(EmailViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await SendConfirmationEmail(await _userManager.FindByEmailAsync(model.Email));
            ViewData["Message"] = "Confirmation email sent.";

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                _logger.LogInformation("Email confirmed successfully.");
                await _signInManager.SignInAsync(user, true);

                return RedirectToAction("EmailConfirmedSuccessfully");
            }

            return View("Error");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteAccount(DeleteAccountViewModel model)
        {
            if (!ModelState.IsValid) return View("Overview", model);
            var user = await _userManager.FindByNameAsync(User.Identity!.Name);
            
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                await _signInManager.SignOutAsync();
                var deletionResult = await _userManager.DeleteAsync(user);
                if (!deletionResult.Succeeded)
                {
                    foreach (var error in deletionResult.Errors)
                    {
                        _logger.LogInformation( error.Description);
                        ModelState.AddModelError("Password", error.Description);
                    }

                    return View("Overview", model);
                }
                
                _logger.LogInformation( $"Deleted user {User.Identity!.Name}.");
                await _signInManager.SignOutAsync();

                foreach (var cookie in Request.Cookies.Keys.Where(key => key.Contains("AspNet")))
                {
                    Response.Cookies.Delete(cookie);
                }

                return RedirectToAction("Index", "Home");
            }
            
            _logger.LogInformation("Incorrect password.");
            ModelState.AddModelError("Password", "Incorrect password.");

            return View("Overview", model);
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByNameAsync(User.Identity!.Name);
            _logger.LogInformation( "Changed password.");
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            
            if (result.Succeeded) return View();

            foreach (var error in result.Errors)
            {
                _logger.LogInformation(error.Description);
                ModelState.AddModelError("CurrentPassword", error.Description);
            }

            return View(model);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task SendConfirmationEmail(IdentityUser user)
        {
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                null,
                new { userId, code },
                Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
        }
    }
}

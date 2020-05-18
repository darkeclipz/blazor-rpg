using DungeonRpg.Engine;
using DungeonRpg.Services;
using DungeonRpg.Pages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DungeonRpg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private PlayerService PlayerService { get; set; }
        private RaceService RaceService { get; set; }
        private MapService MapService { get; set; }

        public UserController(IServiceProvider provider)
        {
            PlayerService = provider.GetService<PlayerService>();
            RaceService = provider.GetService<RaceService>();
            MapService = provider.GetService<MapService>();
        }

        // /api/User/GetUser
        [HttpGet("[action]")]
        public UserModel GetUser()
        {
            // Instantiate a UserModel
            var userModel = new UserModel
            {
                UserName = "[]",
                IsAuthenticated = false
            };

            // Detect if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Set the username of the authenticated user
                userModel.UserName = User.Identity.Name;
                userModel.IsAuthenticated = User.Identity.IsAuthenticated;
            };

            return userModel;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            var player = PlayerService.FindByName(model.Username);
            if (player != null && player.Password == model.Password)
            {
                try
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
                catch
                { }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Role, player.IsAdministrator ? "Administrator" : "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = HttpContext.Request.Host.Value
                };
                try
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                }
                catch
                {
                    return Redirect("/?errormessage=Failed%20to%20login");
                }

                return Redirect("/");
            }
            else
            {
                return Redirect("/?errormessage=Invalid%20username%20or%20password.");
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            var player = PlayerService.New();
            player.Name = model.Username;
            player.Password = model.Password;
            player.Race = RaceService.Find(model.RaceId);
            player.CurrentMapId = MapService.FindByName(Settings.DefaultMapName).Id;
            player.Position = Settings.DefaultPosition;

            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch
            { }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, player.Name),
                new Claim(ClaimTypes.Role, player.IsAdministrator ? "Administrator" : "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                RedirectUri = HttpContext.Request.Host.Value
            };
            try
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            }
            catch 
            {
                return BadRequest("Failed to login.");
            }

            PlayerService.Save();
            return Redirect("/");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch 
            { 
            }
            return Redirect("/");
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserModel
    {
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }
    }

    public class RegisterModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public Guid RaceId { get; set; }
    }
}

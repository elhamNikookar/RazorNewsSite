using Blazor.Model.DTOs.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Blazor.Business.Repository.IRepository;

namespace RazorNews.Pages.Users
{
    public class LoginUserModel : PageModel
    {

        #region Constructor
        [BindProperty]
        public LoginViewModel LoginUser { get; set; }

        private readonly IUserRepository _userRepository;
        public LoginUserModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var user = _userRepository.LoginUser(LoginUser);

            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("RoleTitle", user.Role.RoleTitle)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = LoginUser.RememberMe
                    };

                    HttpContext.SignInAsync(principal, properties);
                    return Redirect("/");
                }
                else
                    Redirect("/Account/AccessDeniedModel");
            }
            else
                ModelState.AddModelError("Email", "حساب کاربری یافت نشد!");

            return Redirect("/");
        }
    }
}

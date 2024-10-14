
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Blazor.Business.Repository.IRepository;
using Blazor.Model.DTOs.Users;

namespace CoffeeShop.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Constructor

        private readonly IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        //#region Register
        //[HttpGet]
        //[Route("Register")]
        //public IActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[Route("Register")]
        //public IActionResult Register(RegisterViewModel register)
        //{
        //    if (!ModelState.IsValid) return View(register);

        //    if (_userRepository.IsExistEmail(register.Email.FixEmail()))
        //    {
        //        ModelState.AddModelError("Email", "ایمیل قبلا ثبت شده است.");
        //        return View(register);
        //    }

        //    if (_userRepository.IsExistUserName(register.UserName))
        //    {
        //        ModelState.AddModelError("UserName", "این نام کاربری از قبل رزرو شده است.");
        //        return View(register);
        //    }

        //    //Register
        //    _userRepository.CreateNewUserProfile(register);

        //    return View("SuccessRegister", register);
        //}

        //#endregion

        //#region Login

        //[Route("Login")]
        //public IActionResult Login(bool EditProfile = false)
        //{
        //    ViewBag.EditProfile = EditProfile;
        //    return View();
        //}

        //[Route("Login")]
        //[HttpPost]
        //public IActionResult Login(LoginViewModel login)
        //{
        //    if (!ModelState.IsValid) return View(login);

        //    var user = _userRepository.LoginUser(login);

        //    if (user != null)
        //    {
        //        if (user.IsActive)
        //        {
        //            var claims = new List<Claim>
        //            {
        //                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        //                new Claim(ClaimTypes.Name, user.UserName)
        //            };

        //            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //            var principal = new ClaimsPrincipal(identity);
        //            var properties = new AuthenticationProperties
        //            {
        //                IsPersistent = login.RememberMe
        //            };

        //            HttpContext.SignInAsync(principal, properties);

        //            ViewBag.IsSuccess = true;
        //            return View();
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("Email", "حساب کاربری فعال نمی باشد.");
        //        }
        //    }
        //    else
        //        ModelState.AddModelError("Email", "حساب کاربری یافت نشد!");

        //    return View(login);
        //}


        //#endregion

        #region Logout

        //[Route("Logout")]
        //public IActionResult Logout()
        //{
        //    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return Redirect("/Login");
        //}

        #endregion

        //#region Reset Password

        //public IActionResult ResetPassword(string userName)
        //{
        //    return View(new ResetPasswordViewModel()
        //    {
        //        UserName = userName
        //    });
        //}


        //[HttpPost]
        //public IActionResult ResetPassword(ResetPasswordViewModel resetPassword)
        //{
        //    if (!ModelState.IsValid)
        //        return View(resetPassword);

        //    if (!_userService.IsExistUserName(resetPassword.UserName))
        //        return NotFound();

        //    _userService.ChangeUserPassword(resetPassword.UserName, resetPassword.Password);

        //    return Redirect("/Login");


        //}

        //#endregion

        //#region ForgotPassword

        //[Route("ForgotPassword")]
        //public IActionResult ForgotPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[Route("ForgotPassword")]
        //public IActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(forgot);
        //    }

        //    //string fixedEmail = forgot.Email.FixEmail();
        //    //DataLayer.Entities.User.User user = _userService.GetUserByEmail(fixedEmail);

        //    //if (user == null)
        //    //{
        //    //    ModelState.AddModelError("Email", "حساب کاربری یافت نشد.");
        //    //    return View();
        //    //}

        //    //string bodyEmail = _viewRenderService.RenderToStringAsync("_ForgotPassword", user);
        //    //ViewBag.IsSuccess = true;

        //    return View();
        //}

        //#endregion



    }
}

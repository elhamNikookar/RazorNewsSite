using Blazor.Business.Repository.IRepository;
using Blazor.Model.DTOs.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CommonLayer;
using Microsoft.Win32;
namespace RazorNews.Pages.Users
{
    public class RegisterModel : PageModel
    {
        #region Constructor
        [BindProperty]
        public RegisterViewModel UserModel { get; set; }

        private readonly IUserRepository _userRepository;
        public RegisterModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            if (await _userRepository.IsExistEmail(UserModel.Email.FixEmail()))
            {
                ModelState.AddModelError("Email", "این نام کاربری از قبل رزرو شده است.");
                return Page();
            }
            if (await _userRepository.IsExistUserName(UserModel.UserName))
            {
                ModelState.AddModelError("UserName", "این نام کاربری از قبل رزرو شده است.");
                return Page();
            }

            int userId = await _userRepository.CreateNewUserProfile(UserModel);
            TempData["SuccessMessage"] = "ثبت نام شما با موفقیت انجام شد.";
            return Redirect("/login");
            //return Redirect("/");
        }
    }
}

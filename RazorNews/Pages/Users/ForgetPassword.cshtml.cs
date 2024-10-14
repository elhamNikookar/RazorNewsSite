using Blazor.Business.Repository.IRepository;
using Blazor.Model.DTOs.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CommonLayer;

namespace RazorNews.Pages.Users
{
    public class ForgetPasswordModel : PageModel
    {
        #region Constructor
        [BindProperty]
        public RegisterViewModel UserModel { get; set; }

        private readonly IUserRepository _userRepository;
        public ForgetPasswordModel(IUserRepository userRepository)
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
                if (await _userRepository.IsExistUserName(UserModel.UserName))
                {
                    _userRepository.ChangeUserPassword(UserModel.UserName, UserModel.Password);
                    TempData["SuccessMessage"] = "رمزکاربری شما با موفقیت تغییر کرد.";
                    return Redirect("/login");
                }
            }

            ModelState.AddModelError("UserName", "چنین مشخصاتی یافت نشد.");
            return Page();
        }
    }
}

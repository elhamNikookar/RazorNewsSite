
using Blazor.Business.Repository.IRepository;
using Blazor.Model.DTOs.Users;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;


namespace RazorNews.Pages.UsersManagement
{
    [Authorize(Policy = "AdminOnly")]

    public class IndexModel : PageModel
    {

        #region Constructor

        private readonly IUserRepository _userRepository;
        public UsersForAdminViewModel UserForAdminViewModel { get; set; }

        public IndexModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion


        public void OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            UserForAdminViewModel = _userRepository.GetUsers(pageId, filterEmail, filterUserName);
        }

        public IActionResult OnPostToggleActivation(int userId, bool isActive)
        {
            var user = _userRepository.GetUserById(userId); // Adjust as necessary
            if (user != null)
            {
                user.IsActive = isActive;
                _userRepository.UpdateUser(user); // Adjust as necessary
                return new Microsoft.AspNetCore.Mvc.JsonResult(new { success = true });
            }
            return NotFound();
        }


        //public IActionResult OnPostToggleUserStatus(int userId)
        //{
        //    var user = _userRepository.GetUserById(userId);
        //    if (user != null)
        //    {
        //        user.IsActive = !user.IsActive;
        //        _userRepository.UpdateUser(user);
        //    }
        //    return RedirectToPage();
        //}
    }

}
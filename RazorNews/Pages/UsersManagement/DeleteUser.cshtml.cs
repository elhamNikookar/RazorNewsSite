
using Blazor.Business.Repository.IRepository;
using Blazor.Model.DTOs.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorNews.Pages.UsersManagement
{

    //[PermissionChecker(5)]
    public class DeleteUserModel : PageModel
    {

        #region Constructor

        public InformationUserViewModel InformationUserViewModel { get; set; }
        private readonly IUserRepository _userRepository;

        public DeleteUserModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        #endregion

        public void OnGet(int id)
        {
            ViewData["UserId"] = id;
            InformationUserViewModel = _userRepository.GetUserInformation(id);
        }

        public IActionResult OnPost(int UserId)
        {
            _userRepository.DeleteUser(UserId);
            return RedirectToPage("Index");
        }
    }
}
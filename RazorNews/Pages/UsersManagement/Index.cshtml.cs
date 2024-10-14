
using Blazor.Business.Repository.IRepository;
using Blazor.Model.DTOs.Users;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorNews.Pages.UsersManagement
{

    //[PermissionChecker(2)]
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


        public  void OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            UserForAdminViewModel = _userRepository.GetUsers(pageId, filterEmail, filterUserName);
        }


    }
}

using Blazor.Business.Repository.IRepository;
using Blazor.Model.DTOs.Users;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorNews.Pages.UsersManagement
{
    [Authorize(Roles = StaticDetail.AdminUser)]

    public class ListDeleteUsersModel : PageModel
    {
        private IUserRepository _userRepository;

        public ListDeleteUsersModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UsersForAdminViewModel UserForAdminViewModel { get; set; }

        public void OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            UserForAdminViewModel = _userRepository.GetDeleteUsers(pageId, filterEmail, filterUserName);
        }

    }
}
using Blazor.Business.Repository.IRepository;
using Blazor.Model.DTOs.Users;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorNews.Pages.UsersManagement
{
    [Authorize(Roles = StaticDetail.AdminUser)]

    public class EditUserModel : PageModel
    {

        #region Constructor

        [BindProperty]
        public EditUserViewModel EditUserViewModel { get; set; }

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;


        public EditUserModel(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        #endregion

        public void OnGet(int id)
        {
            EditUserViewModel = _userRepository.GetUserForShowInEditMode(id);
            ViewData["Roles"] = _roleRepository.GetAllRoles();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = _roleRepository.GetAllRoles();
                return Page();
            }

            _userRepository.EditUserFromAdmin(EditUserViewModel);

            //Edit Roles

            return RedirectToPage("Index");
        }
    }
}


using Blazor.Business.Repository;
using Blazor.Business.Repository.IRepository;
using Blazor.Data.Entities.UserEntities;
using Blazor.Model.DTOs.Users;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorNews.Pages.UsersManagement
{
    [Authorize]
    public class CreateUserModel : PageModel
    {
        #region Constructor

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public List<Role> Roles { get; set; }


        [BindProperty]
        public CreateUserViewModel CreateUserViewModel { get; set; }

        public CreateUserModel(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        #endregion

        public void OnGet()
        {
            Roles = _roleRepository.GetAllRoles();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            int userId = await _userRepository.CreateNewUserFromAdmin(CreateUserViewModel);

            //Add Roles

            return Redirect("/UsersManagement");
        }
    }
}

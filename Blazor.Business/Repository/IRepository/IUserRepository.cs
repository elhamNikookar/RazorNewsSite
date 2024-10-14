using Blazor.Data.Entities.UserEntities;
using Blazor.Model.DTOs.Users;

namespace Blazor.Business.Repository.IRepository
{
    public interface IUserRepository
    {

        #region Base Method

        Task<bool> IsExistUserName(string userName);
        Task<bool> IsExistEmail(string email);
        Task<int> AddUser(User user);
        User LoginUser(LoginViewModel login);
        User GetUserByEmail(string email);
        User GetUserById(int userId);
        User GetUserByUserName(string userName);
        void UpdateUser(User user);
        int GetUserIdbyUserName(string userName);
        void DeleteUser(int userId);

        #endregion


        #region UserPanel

        InformationUserViewModel GetUserInformation(string userName);
        InformationUserViewModel GetUserInformation(int userId);
        SideBarUserPanelViewModel GetSidebarUserPanelData(string userName);
        EditProfileViewModel GetDataForEditProfileUser(string userName);
        void EditProfile(string userName, EditProfileViewModel profile);
        bool CompareOldePassword(string userName, string password);
        void ChangeUserPassword(string userName, string newPassword);

        #endregion

        #region AdminPanel

        UsersForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");
        UsersForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");

        Task<int> CreateNewUserFromAdmin(CreateUserViewModel user);
        EditUserViewModel GetUserForShowInEditMode(int userId);
        void EditUserFromAdmin(EditUserViewModel editUser);

        #endregion

        #region Client
        Task<int> CreateNewUserProfile(RegisterViewModel profile);

        #endregion
    }
}

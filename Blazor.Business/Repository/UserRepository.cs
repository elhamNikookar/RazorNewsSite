using AutoMapper;
using CommonLayer;
using Blazor.Business.Repository.IRepository;
using Blazor.Data.Context;
using Blazor.Data.Entities.UserEntities;
using Blazor.Model.DTOs.Users;
using Microsoft.EntityFrameworkCore;


namespace Blazor.Business.Repository
{


    public class UserRepository : IUserRepository
    {

        #region Constructor

        private readonly ApplicationDbContext _context;
        private readonly string ImageFolder = "wwwroot/img/UserAvatar";

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion


        #region Base Method

        public async Task<bool> IsExistEmail(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsExistUserName(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }

        public async Task<int> AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public User LoginUser(LoginViewModel login)
        {
            string hashPassword = login.Password;//.EncodePasswordMd5();
            string email = login.Email.FixEmail();

            return _context.Users.Include(u => u.Role)
                .SingleOrDefault(u => u.Email == email && u.Password == hashPassword);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public int GetUserIdbyUserName(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName).UserId;
        }


        #endregion


        #region UserPanel

        public InformationUserViewModel GetUserInformation(string userName)
        {
            var user = GetUserByUserName(userName);

            InformationUserViewModel information = new InformationUserViewModel();
            information.UserName = user.UserName;
            information.Email = user.Email; ;
            information.RegisterDate = user.RegisterDate;

            return information;
        }

        public InformationUserViewModel GetUserInformation(int userId)
        {
            var user = GetUserById(userId);

            InformationUserViewModel information = new InformationUserViewModel();
            information.UserName = user.UserName;
            information.Email = user.Email; ;
            information.RegisterDate = user.RegisterDate;

            return information;
        }


        public SideBarUserPanelViewModel GetSidebarUserPanelData(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName)
                .Select(u => new SideBarUserPanelViewModel()
                {
                    UserName = u.UserName,
                    ImageName = u.UserAvatar,
                    RegisterDate = u.RegisterDate
                }).Single();
        }

        public EditProfileViewModel GetDataForEditProfileUser(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName)
                .Select(u => new EditProfileViewModel()
                {
                    AvatarName = u.UserAvatar,
                    Email = u.Email,
                    UserName = u.UserName
                }).Single();
        }

        public void EditProfile(string userName, EditProfileViewModel profile)
        {
            if (profile.UserAvatar != null)
            {
                string imagePath = "";
                if (profile.AvatarName != StaticDetail.DefaultUserAvatar)
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.AvatarName);
                    if (File.Exists(imagePath))
                        File.Delete(imagePath);
                }
                profile.AvatarName = NameGenerator.GeneratorUniqCode() + Path.GetExtension(profile.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.AvatarName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    profile.UserAvatar.CopyTo(stream);
                }
            }

            var user = GetUserByUserName(userName);

            user.UserName = profile.UserName;
            user.Email = profile.Email;
            user.UserAvatar = profile.AvatarName;

            UpdateUser(user);
        }

        public bool CompareOldePassword(string userName, string password)
        {
            string hashOldPassword = password;//.EncodePasswordMd5();
            return _context.Users.Any(u => u.UserName == userName && u.Password == hashOldPassword);
        }

        public void ChangeUserPassword(string userName, string newPassword)
        {
            var user = GetUserByUserName(userName);
            user.Password = newPassword;//.EncodePasswordMd5();

            UpdateUser(user);
        }

        public void DeleteUser(int userId)
        {
            User deleteUser = GetUserById(userId);
            deleteUser.IsDelete = true;
            UpdateUser(deleteUser);
        }

        #endregion


        #region AdminPanel

        public UsersForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.Users;

            if (!string.IsNullOrEmpty(filterEmail))
                result = result.Where(u => u.Email.Contains(filterEmail));

            if (!string.IsNullOrEmpty(filterUserName))
                result = result.Where(u => u.UserName.Contains(filterUserName));

            //Show Item In Page

            int skip = (pageId - 1) * StaticDetail.ItemPerPage;

            UsersForAdminViewModel list = new UsersForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / StaticDetail.ItemPerPage;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(StaticDetail.ItemPerPage).ToList();

            return list;
        }

        public UsersForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.Users.IgnoreQueryFilters().Where(u => u.IsDelete);

            if (!string.IsNullOrEmpty(filterEmail))
                result = result.Where(u => u.Email.Contains(filterEmail));

            if (!string.IsNullOrEmpty(filterUserName))
                result = result.Where(u => u.UserName.Contains(filterUserName));

            //Show Item In Page
            int take = 20;
            int skip = (pageId - 1) * take;

            UsersForAdminViewModel list = new UsersForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public async Task<int> CreateNewUserFromAdmin(CreateUserViewModel user)
        {
            User newUser = new User();

            newUser.Password = user.Password;//.EncodePasswordMd5();
            newUser.UserName = user.UserName;
            newUser.Email = user.Email;
            newUser.RegisterDate = DateTime.Now;
            newUser.IsActive = true;
            newUser.RoleId = user.RoleId;

            #region Save Avatar

            if (user.UserAvatar != null)
            {
                string imagePath = "";
                newUser.UserAvatar = NameGenerator.GeneratorUniqCode() + Path.GetExtension(user.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), ImageFolder, newUser.UserAvatar);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    user.UserAvatar.CopyTo(stream);
                }
            }
            else newUser.UserAvatar = StaticDetail.DefaultUserAvatar;


            #endregion

            return await AddUser(newUser);
        }

        public EditUserViewModel GetUserForShowInEditMode(int userId)
        {
            return _context.Users.Where(u => u.UserId == userId)
                .Select(u => new EditUserViewModel()
                {
                    UserId = u.UserId,
                    AvatarName = u.UserAvatar,
                    UserName = u.UserName,
                    IsActive = u.IsActive,
                    RoleId = u.RoleId,
                    Email = u.Email

                }).Single();
        }

        public void EditUserFromAdmin(EditUserViewModel editUser)
        {
            User user = GetUserById(editUser.UserId);
            user.Email = editUser.Email;
            user.IsActive = editUser.IsActive;
            if (!string.IsNullOrEmpty(editUser.Password))
                user.Password = editUser.Password;//.EncodePasswordMd5();

            if (editUser.UserAvatar != null)
            {
                //Delete old Image
                if (editUser.AvatarName != StaticDetail.DefaultUserAvatar)
                {
                    if (editUser.AvatarName == null) editUser.AvatarName = "";
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), ImageFolder, editUser.AvatarName);
                    if (File.Exists(deletePath))
                        File.Delete(deletePath);
                }

                //Save New Image
                user.UserAvatar = NameGenerator.GeneratorUniqCode() + Path.GetExtension(editUser.UserAvatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), ImageFolder, user.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    editUser.UserAvatar.CopyTo(stream);
                }
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        #endregion


        #region Client
        public async Task<int> CreateNewUserProfile(RegisterViewModel profile)
        {
            User newUser = new User();

            newUser.Password = profile.Password;//.EncodePasswordMd5();
            newUser.UserName = profile.UserName;
            newUser.Email = profile.Email;
            newUser.RegisterDate = DateTime.Now;
            newUser.IsActive = false;
            newUser.RoleId = 3;
            newUser.UserAvatar = StaticDetail.DefaultUserAvatar;

            return await AddUser(newUser);
        }


        #endregion
    }
}

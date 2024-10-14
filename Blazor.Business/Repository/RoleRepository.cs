using Blazor.Business.Repository.IRepository;
using Blazor.Data.Context;
using Blazor.Data.Entities.UserEntities;

namespace Blazor.Business.Repository
{
    public class RoleRepository : IRoleRepository
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        public List<Role> GetAllRoles()
        {
            var roles = _context.Roles.ToList();
            return roles;
        }
    }
}

using Blazor.Data.Entities.UserEntities;

namespace Blazor.Business.Repository.IRepository
{
    public interface IRoleRepository
    {
        List<Role> GetAllRoles();
    }
}

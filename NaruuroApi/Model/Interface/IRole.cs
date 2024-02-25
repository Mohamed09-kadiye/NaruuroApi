using System.Collections.Generic;

namespace NaruuroApi.Model
{
    public interface IRole
    {
        Role GetById(int id);
        List<Role> GetAllRole();

        void AddRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(int id);
    }
}

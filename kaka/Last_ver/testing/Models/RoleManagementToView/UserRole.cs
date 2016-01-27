using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace testing.Models.RoleManagementToView
{
    public class UserRole
    {
        public SelectList Users { get; set; }
        public SelectList Roles { get; set; }
        public List<string> UserRoles { get; set; }
        public UserRole()
        {
        }
        public UserRole(List<string> users, List<string> roles)
        {
            Users = new SelectList(users);
            Roles = new SelectList(roles);
        }
        public UserRole(List<string> users, List<string> roles,List<string> userRoles)
        {
            Users = new SelectList(users);
            Roles = new SelectList(roles);
            UserRoles = userRoles;
        }
    }
}

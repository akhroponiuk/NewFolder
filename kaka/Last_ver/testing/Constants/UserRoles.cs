using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace testing.Constants
{
    public static class UserRoles
    {
        public const string AdminRoleName = "Admin";

        public const string UserRoleName = "User";

        public const string EditorRoleName = "Editor";
        
    }
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}
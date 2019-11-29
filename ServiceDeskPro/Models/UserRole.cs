using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceDeskPro.Models
{
    public class UserRole
    {
        public List<SelectListItem> userRoles;
        public UserRole()
        {
            userRoles = new List<SelectListItem>();
        }

        public List<SelectListItem> GetRoles(RoleManager<IdentityRole> _roleManager)
        {
            var roles = _roleManager.Roles.ToList();
            foreach (var item in roles)
            {
                userRoles.Add(new SelectListItem() {
                
                    Value = item.Id,
                    Text = item.Name
                });
            }
            return userRoles;
        }
    }
}
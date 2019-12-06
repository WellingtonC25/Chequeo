using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using ServiceDeskPro.Models;
using System.Threading.Tasks;
using System;

[assembly: OwinStartupAttribute(typeof(ServiceDeskPro.Startup))]
namespace ServiceDeskPro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void  createRolesandUsers()
        {

            //ApplicationDbContext context = new ApplicationDbContext();

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //string[] Roles = { "Administrador", "Tecnico" };
            //IdentityResult identityRoles;

            //foreach (var roleName in Roles)
            //{
            //    var roleExiste = roleManager.RoleExists(roleName);
            //    if (!roleExiste)
            //    {
            //        identityRoles = roleManager.Create(new IdentityRole(roleName));
            //    }
            //}

            //var userr = UserManager.FindById("53eddab3-9b0a-4ed5-b262-93cf426b7056");
            //UserManager.AddToRole(userr.Id, "Admin");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Saugatshrestha.Data;

namespace Saugatshrestha.Controllers
{
    [Authorize(Roles="SuperAdmin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;

        public AdminController(RoleManager<IdentityRole> roleManager,
                                UserManager<IdentityUser> userManager,
                                ApplicationDbContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.context = context;
        }
        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRole(string roleName)
        {
            var role = new IdentityRole { Name = roleName, NormalizedName= roleName.ToUpper() };

            context.Roles.Add(role);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult AddUserToRole()
        {
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.Where(x=>!x.Name.Equals("SuperAdmin")).ToList();

            ViewBag.Users = new SelectList(users, "Id", "UserName");
            ViewBag.Roles = new SelectList(roles, "Name", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(string userId, string roleName)
        {
            var user = userManager.Users.FirstOrDefault(x => x.Id.Equals(userId));

            await userManager.AddToRoleAsync(user, roleName);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ViewUserRoles()
        {
            var userRoles = context
                                .UserRoles
                                .ToList();


            ViewBag.UserRoles = userRoles;

            return View();
        }
    }
}
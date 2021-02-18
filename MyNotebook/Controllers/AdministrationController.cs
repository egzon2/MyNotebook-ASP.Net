using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyNotebook.Data;
using MyNotebook.Models;

namespace MyNotebook.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;


        public AdministrationController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }


        public IActionResult ListRoles()
        {

            var roles = roleManager.Roles;

            return View(roles);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            // Find the role by Role ID
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };



            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {

                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                return View(model);
            }
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {

                return View("NotFound");
            }
            else
            {
                try
                {
                    var result = _context.Remove(role);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                { }

                return RedirectToAction(nameof(ListRoles));
            }
        }
        //Methods required for user management are below
        [Route("Administration")]

        public async Task<IActionResult> ListUsers(int? pageNumber, string tick, string searchString)
        {



            var users = from s in userManager.Users
                        select s;

            if (tick != null)
            {
                if (Equals(tick, "Admin"))
                {
                    users = users.Where(s => s.isAdmin == true);
                }
            }
            else
            {
                users = from s in userManager.Users
                        select s;
            }

            if (searchString != null)
            {
                users = users.Where(s => s.UserName.ToUpper().Contains(searchString.ToUpper()));

            }

            int pageSize = 10;
            return View(await PaginatedList<ApplicationUser>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        [HttpGet]
     
        public async Task<IActionResult> EditAcc(string id) {

            var user = await userManager.FindByIdAsync(id);
            return View(user);
        }

        [HttpGet]
      

        public async Task<IActionResult> setAdmin(string id, int tick) {
            var user = await userManager.FindByIdAsync(id);
            if (tick ==1) {
                user.isAdmin = true;
               await userManager.AddToRoleAsync(user, "Admin");
            }
            return RedirectToAction("ListUsers");
        }
       


    }
}

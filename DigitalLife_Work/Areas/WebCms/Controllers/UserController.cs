using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalLife_Work.Models.BLL;
using DigitalLife_Work.Models.DAL;
using DigitalLife_Work.Models.VM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalLife_Work.Areas.WebCms.Controllers
{
    [Area("WebCms")]
   // [Route("WebCms/")]
    [Route("WebCms/[controller]/[action]")]

    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly MyContext _context;
        public UserController(UserManager<AppUser> userManager, MyContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }
        //User index Function Start
        public IActionResult Index()
        {
            return View();
        }
        //User index Function End

        //User Creat Function Start
        public IActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel viewModel) 
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    Name = viewModel.Name,
                    Surname = viewModel.Surname,
                    Email = viewModel.Email,
                    UserName = viewModel.UserName
                };

                if (user ==null)
                {
                    return View();
                }


                var Role = await _context.Roles.FindAsync(viewModel.Role);
                if (Role == null)
                {
                    ModelState.AddModelError("Role", "Role secin");
                    return View();
                }

                IdentityResult result = await _userManager.CreateAsync(user, viewModel.Password);

                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                        return View();
                    }
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, Role.Name);
                }

                return RedirectToAction(nameof(Index));
            }
            return View();

        }
        //User Creat Function End

        //User Edit Function Start
        public async Task<IActionResult> Edit(string id, EditUserViewModel model)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            model.Id = user.Id;
            model.Name = user.Name;
            model.Surname = user.Surname;
            model.Email = user.Email;
            model.UserName = user.UserName;
            var userRoles = await _userManager.GetRolesAsync(user);
            model.Role = userRoles.FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel viewModel, string id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(viewModel.Id);
                user.Name = viewModel.Name;
                user.Surname = viewModel.Surname;
                user.Email = viewModel.Email;
                user.UserName = viewModel.UserName;

                //if you use current password
                var result = await _userManager.ChangePasswordAsync(user, viewModel.OldPassword, viewModel.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(nameof(viewModel.Password), error.Description);
                        return View(viewModel);
                    }
                }

                //pass and role
                var userRoles = await _userManager.GetRolesAsync(user);
                
                //delete all roles
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                var role = await _roleManager.FindByIdAsync(viewModel.Role);
                //add role to user
                await _userManager.AddToRoleAsync(user, role.Name);


                await _context.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        //User Edit Function End

        //User Delete Function Start
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(result);
            return RedirectToAction(nameof(Index));
        }
        //User Delete Function End


    }
}

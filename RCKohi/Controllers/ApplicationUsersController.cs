using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RCKohi.Data;
using RCKohi.Models;
using RCKohi.Models.ApplicationUsersViewModels;

namespace RCKohi.Controllers
{
    [Authorize(Roles = "admin")]
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationUsersController(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager
        )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: ApplicationUsers
        public async Task<IActionResult> Index()
        {
            var userViewModels = new List<UserViewModel>();
            var users = await _context.ApplicationUser.ToListAsync();
            foreach (ApplicationUser u in users)
            {
                UserViewModel _userViewModel = new UserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    PhoneNumber = u.PhoneNumber,
                    Email = u.Email,
                    Roles = await _userManager.GetRolesAsync(u),
                };
                userViewModels.Add(_userViewModel);
            }
            //return View(await _context.ApplicationUser.ToListAsync());
            return View(userViewModels);
        }

        // GET: ApplicationUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserViewModel userViewModel;

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            else
            {
                userViewModel = new UserViewModel
                {
                    Id = applicationUser.Id,
                    UserName = applicationUser.UserName,
                    PhoneNumber = applicationUser.PhoneNumber,
                    Email = applicationUser.Email,
                    Roles = await _userManager.GetRolesAsync(applicationUser),
                };
                return View(userViewModel);
            }
        }

        // GET: ApplicationUsers/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.RolesList = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "NormalizedName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model, string returnUrl = null, params string[] selectedRoles)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                if (model.Email == null)
                {
                    model.Email = "5140075@qq.com";
                }
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.PhoneNumber };
                var userResult = await _userManager.CreateAsync(user, model.Password);
                if (userResult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var roleResult = await _userManager.AddToRolesAsync(user, selectedRoles);
                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                    //return RedirectToAction(nameof(Index));
                }

                foreach (var error in userResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: ApplicationUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserViewModel userViewModel;

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            else
            {
                userViewModel = new UserViewModel
                {
                    Id = applicationUser.Id,
                    UserName = applicationUser.UserName,
                    PhoneNumber = applicationUser.PhoneNumber,
                    Email = applicationUser.Email,
                    Roles = await _userManager.GetRolesAsync(applicationUser),
                };
                ViewBag.RolesList = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "NormalizedName");
                return View(userViewModel);
            }
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserViewModel model, string returnUrl = null, params string[] selectedRoles)
        {

            if (id != model.Id)
            {
                return NotFound();
            }
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (user.PhoneNumber != model.PhoneNumber)
            {
                try
                {
                    user.PhoneNumber = model.PhoneNumber;
                    await _userManager.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }

            if (selectedRoles != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles.ToArray());

                var roleResult = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (roleResult.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // GET: ApplicationUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserViewModel userViewModel;

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            else
            {
                userViewModel = new UserViewModel
                {
                    Id = applicationUser.Id,
                    UserName = applicationUser.UserName,
                    PhoneNumber = applicationUser.PhoneNumber,
                    Email = applicationUser.Email,
                    Roles = await _userManager.GetRolesAsync(applicationUser),
                };
                return View(userViewModel);
            }
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            await _userManager.RemoveFromRolesAsync(applicationUser, roles.ToArray());

            // _context.ApplicationUser.Remove(applicationUser);
            await _userManager.DeleteAsync(applicationUser);
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}

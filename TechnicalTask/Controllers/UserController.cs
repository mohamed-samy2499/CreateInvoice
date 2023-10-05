

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace TechnicalTask.Controllers
{
    [Authorize(Roles = "Admin")]

    public class UserController : Controller
    {
        #region Properties
        public UserManager<IdentityUser> UserManager { get; }
        #endregion

        #region Constructor
        public UserController(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }
        #endregion

        #region Actions
        #region Index
        public IActionResult Index()
        {
            var Users = UserManager.Users;
            return View(Users);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {

            if (id == null)
                return NotFound();
            var User = await UserManager.FindByIdAsync(id);
            if (User == null)
                return NotFound();
            return View(ViewName, User);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(string id)
        {

            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, IdentityUser model)
        {
            if (id != model.Id)
                return NotFound();
            try
            {
                var user = await UserManager.FindByIdAsync(id);

                var result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            catch
            {
                return View(model);
            }
            return View(model);
        }
        #endregion


        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, IdentityUser model)
        {
            if (model.Id != id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try 
                {
                    var user = await UserManager.FindByIdAsync(id);
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    user.NormalizedEmail = model.NormalizedEmail;
                    user.NormalizedUserName = model.NormalizedUserName;
                    user.PhoneNumber = model.PhoneNumber;
                    user.SecurityStamp = model.SecurityStamp;
                    var result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                }
                catch
                {
                    return View(model);
                }
            }
            return View(model);
        }
        #endregion
        #endregion


    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using project.Models;

namespace project.Controllers
{
    public class RolesAdminController : Controller
    {

        public RolesAdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        //
        // GET: /Roles/
        [HttpGet]
        public ActionResult Index()
        {
            return View(_roleManager.Roles);
        }

        //
        // GET: /Roles/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var role = await _roleManager.FindByIdAsync(id);
            // Get the list of Users in this Role
            var users = new List<ApplicationUser>();

            // Get the list of Users in this Role
            foreach (var user in _userManager.Users.ToList())
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    users.Add((ApplicationUser)user);
                }
            }

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();
            return View(role);
        }

        //
        // GET: /Roles/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleViewModel roleViewModel)
        {
            //if (ModelState.IsValid)
            //{
                var role = new IdentityRole(roleViewModel.Name);
               
                var roleresult = _roleManager.CreateAsync(role).GetAwaiter().GetResult();
                if (!roleresult.Succeeded)
                {
                   // ModelState.AddModelError("", roleresult.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            //}
            //return View();
        }

        ////
        //// GET: /Roles/Edit/Admin
        //[HttpGet]
        //public async Task<ActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return BadRequest();
        //    }
        //    var role = await _roleManager.FindByIdAsync(id);
        //    if (role == null)
        //    {
        //        return BadRequest();
        //    }
        //    RoleViewModel roleModel = new RoleViewModel { Id = role.Id, Name = role.Name, descripcion = role.descripcion };
        //    return View(roleModel);
        //}

        ////
        //// POST: /Roles/Edit/5
        //[HttpPost]

        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Name,Id, descripcion")] RoleViewModel roleModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var role = await RoleManager.FindByIdAsync(roleModel.Id);
        //        role.Name = roleModel.Name;
        //        role.descripcion = roleModel.descripcion;
        //        await RoleManager.UpdateAsync(role);
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //
        // GET: /Roles/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return BadRequest();
            }
            return View(role);
        }

        //
        // POST: /Roles/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id, string deleteUser)
        {
            //if (ModelState.IsValid)
            //{
                if (id == null)
                {
                    return BadRequest();
                }
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return BadRequest();
                }
                IdentityResult result;
                if (deleteUser != null)
                {
                    result = await _roleManager.DeleteAsync(role);
                }
                else
                {
                    result = await _roleManager.DeleteAsync(role);
                }
                if (!result.Succeeded)
                {
                    //ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            //}
            //return View();
        }
    }
}

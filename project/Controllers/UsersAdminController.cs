using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace project.Controllers
{
    public class UsersAdminController : Controller
    {
       
        public UsersAdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        private readonly UserManager<IdentityUser> _userManager;
    

        private readonly RoleManager<IdentityRole> _roleManager;
       
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return BadRequest("request is incorrect");
            }
            var user = await _userManager.FindByIdAsync(id);

            ViewBag.RoleNames = await _userManager.GetRolesAsync(user);

            return View(user);
        }

        // GET: /Users/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = userViewModel.Email,
                    Email = userViewModel.Email,
                    nombre = userViewModel.nombre,
                    apellidos = userViewModel.apellidos,
                    cedula = userViewModel.cedula,
                    fechaNacimiento = userViewModel.fechaNacimiento
                };

                var adminresult = await _userManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await _userManager.AddToRolesAsync(user, selectedRoles);
                        if (!result.Succeeded)
                        {
                           // ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
                            return View();
                        }
                    }
                }
                else
                {
                    SendEmail(user.Email);
                    //ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(_roleManager.Roles, "Name", "Name");
                    return View();

                }
                SendEmail(user.Email);
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(_roleManager.Roles, "Name", "Name");
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest("BAD REQUEST");
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest("BAD REQUEST");
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return BadRequest("BAD REQUEST");
                }

                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return BadRequest("BAD REQUEST");
                }
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                   // ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }




        public class RegisterViewModel
        {
            [Required]
            public string nombre { get; set; }
            [Required]
            public int cedula { get; set; }
            [Required]
            public string apellidos { get; set; }
            [Required]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            public DateTime fechaNacimiento { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        //Correo
        private void SendEmail(string Email)
        {
            using (SmtpClient client = new SmtpClient()
            {
                Host = "smtp.office365.com",
                Port = 587,
                UseDefaultCredentials = false, // This require to be before setting Credentials property
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("no-reply-elearning@outlook.com", "Elearning123*"), // you must give a full email address for authentication 
                TargetName = "STARTTLS/smtp.office365.com", // Set to avoid MustIssueStartTlsFirst exception
                EnableSsl = true // Set to avoid secure connection exception
            })
            {
                MailMessage message = new MailMessage()
                {
                    From = new MailAddress("no-reply-elearning@outlook.com"), // sender must be a full email address
                    Subject = "Nuevo acceso - E-Learning",
                    IsBodyHtml = true,
                    //Body = "<h1>Bienvenido</h1>",
                    Body = "<p>Estimado(a):</p> " +
                    "<p> Se le ha dado acceso al sistema E-Learning, " +
                    "por lo que es necesario que ingrese al sistema con su correo electrónico y " +
                    "contraseña.</p>" +
                    "<p> Por favor no responder este correo,no nos hacemos reponsables del uso de terceros. </p>" +
                    "<p> Saludos cordiales, </p>" +
                    "<p> E-Learning. </p>" +
                    "<p> Atención: Si tiene algún problema o desea realizar una consulta, puede comunicarse con soporte técnico.</p>",
                    BodyEncoding = System.Text.Encoding.UTF8,
                    SubjectEncoding = System.Text.Encoding.UTF8,
                };
                message.To.Add(Email);
                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }



    }
}

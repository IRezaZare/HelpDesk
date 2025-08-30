using HelpDesk.Data;
using HelpDesk.Entities;
using HelpDesk.ViewModels.UsersDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HelpDesk.Controllers
{
    public class UsersController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        : AuthorizeBaseController
    {


        public async Task<IActionResult> Index()
        {
            var usere = await context.Users.ToListAsync();
            return View(usere);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto model)
        {
            return RedirectToAction("Index");
        }
        
    }
}

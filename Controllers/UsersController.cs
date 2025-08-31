using AutoMapper;
using HelpDesk.Data;
using HelpDesk.Entities;
using HelpDesk.ViewModels.UsersDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace HelpDesk.Controllers
{
    public class UsersController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context , IMapper mapper)
        : AuthorizeBaseController
    {


        public async Task<IActionResult> Index()
        {
            var users = await context.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            var roles = await context.Roles.ToListAsync();
            return View(roles);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            var role = new IdentityRole(model.Name);
            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Roles");
            }
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto model)
        {
            var user = mapper.Map<ApplicationUser>(model);
            var result = await userManager.CreateAsync(user , model.Password);
            if(result.Succeeded) return RedirectToAction("Index");

            if (result.Errors.Any())
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserRolesList()
        {
            var userRoles = await context.UserRoles.ToListAsync();
            
            //users
            var users = await context.Users.
                Where(u => userRoles.
                Select(r => r.UserId).
                Contains(u.Id)).
                ToListAsync();

            //roles
            var roles = await context.Roles.
                Where(r => userRoles.
                Select(rl => rl.RoleId).
                Contains(r.Id)).
                ToListAsync();
            //result
            var result = new List<UserRolesViewModel>();
            foreach (var user in users)
            {
                //UserRoles
                var myUserRoles = userRoles.Where(r => r.UserId == user.Id)
                    .Select(r => r.RoleId);
                
                //Roles
                var myRoles = roles.Where(x => myUserRoles.Contains(x.Id));
                foreach (var role in myRoles)
                {
                    result.Add(new UserRolesViewModel()
                    {
                        RoleId = role.Id,
                        RoleName = role.Name,
                        UserId = user.Id,
                        UserName = user.UserName
                    });

                }
            }
            return View(result);
        }
        
        [HttpGet("{userId}/{roleId}")]
        public IActionResult DeleteUserRole(string userId , string roleId)
        {
            return View(new DeleteUserRoleViewModel()
            {
                RoleId = roleId,
                UserId = userId
            });
        }
        [HttpPost("{userId}/{roleId}")]
        public async Task<IActionResult> DeleteUserRole(DeleteUserRoleViewModel model , string userId , string roleId)
        {
            context.UserRoles.Remove(new IdentityUserRole<string>()
            {
                RoleId  = model.RoleId,
                UserId = model.UserId
            });
            if (await context.SaveChangesAsync() > 0)
            {
                return RedirectToAction("UserRolesList");
            }
            return View(model);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> CreateUserRole(string userId)
        {
            var user = await context.Users.FindAsync(userId);
            var selectListItem = await context.Roles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id
            }).ToListAsync();
            ViewData["Roles"] = new SelectList(selectListItem, "Value", "Text", 0);
            if (user == null) return NotFound();
            return View(new CreateUserRoleViewModel()
            {
                UserName = user.UserName,
                UserId = user.Id
            });
        }
        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateUserRole(CreateUserRoleViewModel model , string userId)
        {
            context.UserRoles.Add(new IdentityUserRole<string>()
            {
                RoleId = model.RoleId,
                UserId = model.UserId
            });
            if(await context.SaveChangesAsync()> 0)
            {
                return RedirectToAction("UserRolesList");
            }
            return View(model);
        }
        
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(ApplicationDbContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
        }

        private List<selectDesignation> GetAllDesignations()
        {
            return _context.Designations
                .Select(x => new selectDesignation { id = x.id, name = x.name })
                .ToList();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddUser()
        {
            var AllUsers = _context.Users
                .Include(m => m.Designation)
                .ToList();
            ViewBag.AllUsers = AllUsers;

            var selectallDesignations = GetAllDesignations();
            ViewBag.Designations = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectallDesignations, "id", "name");

            return View();
        }

        public bool TryValidateModel(User auser)
        {
            if (!string.IsNullOrEmpty(auser.email) && 
                !string.IsNullOrEmpty(auser.password) && 
                !string.IsNullOrEmpty(auser.name))
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(User aUser)
        {
            ViewBag.message = null;
            
            if (!TryValidateModel(aUser))
            {
                ViewBag.message = "Validation Error";
                return View(aUser);
            }

            try
            {
                _context.Users.Add(aUser);
                _context.SaveChanges();
                ViewBag.message = "Insertion Successful";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user");
                ViewBag.message = "Error adding user";
            }

            var selectallDesignations = GetAllDesignations();
            ViewBag.Designations = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectallDesignations, "id", "name");

            var AllUsers = _context.Users
                .Include(m => m.Designation)
                .ToList();
            ViewBag.AllUsers = AllUsers;

            return View();
        }

        public async Task<IActionResult> ResetDefault(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            User auser = await _context.Users.FindAsync(id);
            if (auser == null)
            {
                return NotFound();
            }
            
            auser.password = auser.email + "123";
            _context.Entry(auser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction("AddUser", "User");
        }

        public async Task<IActionResult> UpdateUser(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            
            User aUser = await _context.Users.FindAsync(id);
            if (aUser == null)
            {
                return NotFound();
            }

            var AllUsers = _context.Users
                .Include(m => m.Designation)
                .ToList();
            ViewBag.AllUsers = AllUsers;

            var selectallDesignations = GetAllDesignations();
            ViewBag.Designations = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectallDesignations, "id", "name");

            return View(aUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser([Bind("id,name,email,password,status,designationId")] User aUser)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(aUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                ViewBag.message = "Updated Successfully";
            }

            var selectallDesignations = GetAllDesignations();
            ViewBag.Designations = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectallDesignations, "id", "name");
            return View(aUser);
        }

        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            
            User aUser = await _context.Users.FindAsync(id);
            if (aUser == null)
            {
                return NotFound();
            }

            return View(aUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(int id)
        {
            User aUser = await _context.Users.FindAsync(id);
            _context.Users.Remove(aUser);
            await _context.SaveChangesAsync();
            ViewBag.message = "deletion successful";

            return View(aUser);
        }

        public async Task<IActionResult> ViewUsers()
        {
            var inventories = _context.Users.Include("Users");
            return View(await inventories.ToListAsync());
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Models;
using System.Security.Claims;

namespace ProjectManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        private LoginViewModel GetCurrentUser()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    var user = _context.Users
                        .Include(u => u.Designation)
                        .FirstOrDefault(u => u.id == userId);
                    
                    if (user != null)
                    {
                        return new LoginViewModel
                        {
                            id = user.id,
                            Email = user.email,
                            PersonalName = user.name,
                            designationName = user.Designation?.name
                        };
                    }
                }
            }
            return null;
        }

        private List<SelectProject> GetAllProjects()
        {
            return _context.Projects
                .Select(x => new SelectProject { id = x.id, Projectname = x.name })
                .ToList();
        }

        private List<SelectEmployee> GetAllEmployees()
        {
            return _context.Users
                .Where(y => y.designationId != 1)
                .Select(x => new SelectEmployee { id = x.id, empName = x.name })
                .ToList();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = "Simple Project Management Tool's description page.";
            return View();
        }

        public IActionResult AddProject()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProject(Project aProject)
        {
            ViewBag.message = null;

            if (!ModelState.IsValid)
            {
                ViewBag.message = "Validation Error";
                return View(aProject);
            }

            try
            {
                _context.Projects.Add(aProject);
                _context.SaveChanges();
                ViewBag.message = "Insertion Successful";
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding project");
                ViewBag.message = "Error adding project";
                return View(aProject);
            }
        }

        public IActionResult ProjectManagerHome()
        {
            var currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                return RedirectToAction("Login");
            }

            var normalEmpProjects = _context.Assignments
                .Include(m => m.Project)
                .Where(m => m.userId == currentUser.id)
                .Select(X => new ProjectView
                {
                    id = X.projectId,
                    name = X.Project.name,
                    shortName = X.Project.shortName,
                    statusname = _context.ProjectStatuses
                        .Where(o => o.id == X.Project.statusId)
                        .Select(o => o.name)
                        .FirstOrDefault() ?? "Unknown",
                    memberCount = _context.Assignments
                        .Where(o => o.projectId == X.Project.id)
                        .Count(),
                    taskCount = _context.Tasks
                        .Where(s => s.projectId == X.Project.id)
                        .Count(),
                }).ToList();

            ViewBag.YourProjects = normalEmpProjects;
            return View();
        }

        public IActionResult UpdateProject()
        {
            var allfiles = _context.AllFiles.ToList();
            ViewBag.AllFiles = allfiles;

            List<YourProjectView> AllProjects = _context.Projects
                .Include(m => m.AllFiles)
                .Include(m => m.ProjectStatus)
                .Select(X => new YourProjectView
                {
                    id = X.id,
                    name = X.name,
                    codeName = X.name,
                    description = X.description,
                    status = X.ProjectStatus != null ? X.ProjectStatus.name : null,
                    startDate = X.startDate,
                    endDate = X.endDate,
                    duration = X.duration,
                    statusname = X.ProjectStatus != null ? X.ProjectStatus.name : "Unknown",
                }).ToList();

            ViewBag.AllProjects = AllProjects;
            return View();
        }

        public IActionResult AssignEmployee()
        {
            var selectallProjects = GetAllProjects();
            ViewBag.selectProjects = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectallProjects, "id", "Projectname");

            var selectallEmployees = GetAllEmployees();
            ViewBag.selectEmployees = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectallEmployees, "id", "empName");

            List<Assignment> allAssignment = _context.Assignments
                .Include(m => m.Project)
                .Include(m => m.User)
                .ToList();

            ViewBag.allAssignment = allAssignment;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignEmployee(Assignment oneAssignment)
        {
            var selectallProjects = GetAllProjects();
            ViewBag.selectProjects = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectallProjects, "id", "Projectname");

            var selectallEmployees = GetAllEmployees();
            ViewBag.selectEmployees = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectallEmployees, "id", "empName");

            if (TryValidateModelAssignment(oneAssignment))
            {
                var duplicateAssingment = _context.Assignments
                    .Where(m => m.projectId == oneAssignment.projectId && m.userId == oneAssignment.userId)
                    .Select(m => m.id)
                    .FirstOrDefault();

                if (duplicateAssingment == 0)
                {
                    _context.Assignments.Add(oneAssignment);
                    _context.SaveChanges();
                    ViewBag.message = "Assignment Successful";
                }
                else
                {
                    ViewBag.message = "User Already Assigned to this project";
                }
            }

            return View();
        }

        public bool TryValidateModelAssignment(Assignment oneAssignment)
        {
            try
            {
                int projectid = (int)oneAssignment.projectId;
                int userid = (int)oneAssignment.userId;
                return true;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        public async Task<IActionResult> ProjectDetailUpdate(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Project aProject = await _context.Projects.FindAsync(id);
            if (aProject == null)
            {
                return NotFound();
            }

            return View(aProject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProjectDetailUpdate(Project aProject)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(aProject).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                ViewBag.message = "Updated Successfully";
            }

            return View(aProject);
        }

        public IActionResult ProjectDetail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            List<ProjectDetail> projectDetail = _context.Projects
                .Where(m => m.id == id)
                .Include(m => m.ProjectStatus)
                .Select(x => new ProjectDetail
                {
                    id = x.id,
                    name = x.name,
                    shortName = x.shortName,
                    statusName = x.ProjectStatus.name,
                    startDate = x.startDate,
                    endDate = x.endDate,
                    duration = x.duration,
                    description = x.description,
                    codeName = x.codeName == string.Empty ? "None" : x.codeName,
                }).ToList();

            if (!projectDetail.Any())
            {
                return NotFound();
            }

            ProjectDetail OurprojectDetail = projectDetail.First();

            var uploadedFiles = _context.AllFiles
                .Where(a => a.projectId == id)
                .Select(a => a.fname)
                .ToList();
            ViewBag.UploadedFiles = uploadedFiles;

            var AssignedMembers = _context.Assignments
                .Where(m => m.projectId == id)
                .Include(m => m.User)
                .ThenInclude(u => u.Designation)
                .OrderByDescending(m => m.id)
                .Select(X => new AssignView
                {
                    userName = X.User.name,
                    designation = X.User.Designation.name,
                    id = X.id,
                }).ToList();

            List<ProjectSpecificTaskView> tasksbyProject = _context.Tasks
                .Where(m => m.projectId == id)
                .Select(X => new ProjectSpecificTaskView
                {
                    id = X.id,
                    assigndbyName = _context.Users
                        .Where(u => u.id == X.assignedbyId)
                        .Select(u => u.name)
                        .FirstOrDefault() ?? "Unknown",
                    assigneeName = _context.Users
                        .Where(u => u.id == X.assigneeid)
                        .Select(u => u.name)
                        .FirstOrDefault() ?? "Unknown",
                    description = X.description,
                    priorityName = _context.Priorities
                        .Where(p => p.id == X.priorityId)
                        .Select(p => p.priorityName)
                        .FirstOrDefault() ?? "Unknown",
                    dueDate = X.dueDate,
                    taskHeading = X.taskHeading,
                }).ToList();

            ViewBag.tasksbyProject = tasksbyProject;
            ViewBag.AssignedMembers = AssignedMembers;

            return View(OurprojectDetail);
        }

        public IActionResult UploadFiles(int? id)
        {
            Project tempProject = new Project();
            tempProject.id = (int)id;
            return View(tempProject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadFiles(int id, IFormFile file)
        {
            string path = "";
            Project oneProject = await _context.Projects.FindAsync(id);
            if (oneProject == null)
            {
                return NotFound();
            }

            var projectName = oneProject.name;
            int fileId = 0;

            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Assets", fileName);
                
                // Ensure directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                AllFile onefile = new AllFile();
                onefile.fname = fileName;
                onefile.projectId = id;
                _context.AllFiles.Add(onefile);
                await _context.SaveChangesAsync();
                fileId = onefile.id;
            }

            if (fileId != 0)
            {
                ViewBag.message = "file add successful";
                return View();
            }

            ViewBag.message = "file add unsuccessful";
            return View();
        }

        public IActionResult UsersProject()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public IActionResult UsersProjectedit()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.ReturnUrl = "/Home/Index";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            
            
            
            // _logger.LogError("model ${model}");
            // _logger.LogError(returnUrl, "returnUrl");
            
            // Simple information logging
            _logger.LogInformation("Home page accessed at {Time}", model);

            
            ViewBag.message = null;
            if (!ModelState.IsValid)
            {
                ViewBag.message = "Validation Error";
                return View(model);
            }

            var aUser = _context.Users
                .Include("Designation")
                .Where(U => U.email == model.Email && U.password == model.Password)
                .ToList();

            if (!aUser.Any())
            {
                ViewBag.message = "User Not found";
                return View();
            }

            User ourUser = aUser.FirstOrDefault();
            
            // Create claims for authentication
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, ourUser.name),
                new Claim(ClaimTypes.Email, ourUser.email),
                new Claim(ClaimTypes.NameIdentifier, ourUser.id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
            {
                IsPersistent = false
            };

            await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity), authProperties);

            // Store user info in session for compatibility
            HttpContext.Session.SetString("UserName", ourUser.name);
            HttpContext.Session.SetString("UserEmail", ourUser.email);
            HttpContext.Session.SetInt32("UserId", ourUser.id);
            HttpContext.Session.SetString("DesignationName", ourUser.Designation?.name);

            // Redirect based on designation
            switch (ourUser.Designation?.name)
            {
                case "It Admin":
                    return RedirectToAction("AddUser", "User");
                case "Project Manager":
                    return RedirectToAction("ProjectManagerHome", "Home");
                case "Team Lead":
                    return RedirectToAction("TeamLead", "Home");
                case "Developer":
                    return RedirectToAction("Developer", "Home");
                case "QA Engineer":
                    return RedirectToAction("QAEngineer", "Home");
                case "UX Engineer":
                    return RedirectToAction("UXEngineer", "Home");
                default:
                    return RedirectToLocal(returnUrl);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync("Cookies");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult TeamLead()
        {
            var currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                return RedirectToAction("Login");
            }

            var normalEmpProjects = _context.Assignments
                .Include(m => m.Project)
                .Where(m => m.userId == currentUser.id)
                .Select(X => new ProjectView
                {
                    id = X.projectId,
                    name = X.Project.name,
                    shortName = X.Project.shortName,
                    statusname = _context.ProjectStatuses
                        .Where(o => o.id == X.Project.statusId)
                        .Select(o => o.name)
                        .FirstOrDefault() ?? "Unknown",
                    memberCount = _context.Assignments
                        .Where(o => o.projectId == X.Project.id)
                        .Count(),
                    taskCount = _context.Tasks
                        .Where(s => s.projectId == X.Project.id)
                        .Count(),
                }).ToList();

            ViewBag.YourProjects = normalEmpProjects;
            return View();
        }

        public IActionResult Developer()
        {
            var currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                return RedirectToAction("Login");
            }

            var normalEmpProjects = _context.Assignments
                .Include(m => m.Project)
                .Where(m => m.userId == currentUser.id)
                .Select(X => new ProjectView
                {
                    id = X.projectId,
                    name = X.Project.name,
                    shortName = X.Project.shortName,
                    statusname = _context.ProjectStatuses
                        .Where(o => o.id == X.Project.statusId)
                        .Select(o => o.name)
                        .FirstOrDefault() ?? "Unknown",
                    memberCount = _context.Assignments
                        .Where(o => o.projectId == X.Project.id)
                        .Count(),
                    taskCount = _context.Tasks
                        .Where(s => s.projectId == X.Project.id)
                        .Count(),
                }).ToList();

            ViewBag.YourProjects = normalEmpProjects;
            return View();
        }

        public IActionResult QAEngineer()
        {
            var currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                return RedirectToAction("Login");
            }

            var normalEmpProjects = _context.Assignments
                .Include(m => m.Project)
                .Where(m => m.userId == currentUser.id)
                .Select(X => new ProjectView
                {
                    id = X.projectId,
                    name = X.Project.name,
                    shortName = X.Project.shortName,
                    statusname = _context.ProjectStatuses
                        .Where(o => o.id == X.Project.statusId)
                        .Select(o => o.name)
                        .FirstOrDefault() ?? "Unknown",
                    memberCount = _context.Assignments
                        .Where(o => o.projectId == X.Project.id)
                        .Count(),
                    taskCount = _context.Tasks
                        .Where(s => s.projectId == X.Project.id)
                        .Count(),
                }).ToList();

            ViewBag.YourProjects = normalEmpProjects;
            return View();
        }

        public IActionResult UXEngineer()
        {
            var currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                return RedirectToAction("Login");
            }

            var normalEmpProjects = _context.Assignments
                .Include(m => m.Project)
                .Where(m => m.userId == currentUser.id)
                .Select(X => new ProjectView
                {
                    id = X.projectId,
                    name = X.Project.name,
                    shortName = X.Project.shortName,
                    statusname = _context.ProjectStatuses
                        .Where(o => o.id == X.Project.statusId)
                        .Select(o => o.name)
                        .FirstOrDefault() ?? "Unknown",
                    memberCount = _context.Assignments
                        .Where(o => o.projectId == X.Project.id)
                        .Count(),
                    taskCount = _context.Tasks
                        .Where(s => s.projectId == X.Project.id)
                        .Count(),
                }).ToList();

            ViewBag.YourProjects = normalEmpProjects;
            return View();
        }
    }
}

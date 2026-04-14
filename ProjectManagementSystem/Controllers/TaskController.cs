using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Models;
using System.Security.Claims;

namespace ProjectManagementSystem.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ApplicationDbContext context, ILogger<TaskController> logger)
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

        [HttpGet]
        public JsonResult GetEmployeeByProject(int projectId, int LoggerId)
        {
            var response_data = GetEmployeesAssignedToProject(projectId);
            return Json(response_data);
        }

        [HttpGet]
        public JsonResult GetTasksByProject(int projectId, int LoggerId)
        {
            var response_data = GetProjectSpecificTask(projectId);
            return Json(response_data);
        }

        public List<SelectAssignee> GetEmployeesAssignedToProject(int projectId)
        {
            return _context.Assignments
                .Where(m => m.projectId == projectId)
                .Select(X => new SelectAssignee
                {
                    userid = X.userId,
                    assignee = _context.Users
                        .Where(u => u.id == X.userId)
                        .Select(u => u.name)
                        .FirstOrDefault() ?? "Unknown",
                }).ToList();
        }

        private List<selectPriority> GetAllPriorities()
        {
            return _context.Priorities
                .Select(x => new selectPriority { id = x.id, priorityName = x.priorityName })
                .ToList();
        }

        private List<SelectTask> GetProjectSpecificTask(int projectId)
        {
            return _context.Tasks
                .Where(m => m.projectId == projectId)
                .Select(x => new SelectTask { id = x.id, TaskHeading = x.taskHeading })
                .ToList();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddTask()
        {
            int tempProjectId = 1;

            var selectallProjects = GetAllProjects();
            ViewBag.selectProjects = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectallProjects, "id", "Projectname");

            var selectEmployeesAssignedToProject = GetEmployeesAssignedToProject(tempProjectId);
            ViewBag.selectEmployeesAssignedToProject = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectEmployeesAssignedToProject, "userid", "assignee");

            var selectallPriorities = GetAllPriorities();
            ViewBag.selectallPriorities = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectallPriorities, "id", "priorityName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTask(Models.Task aTask)
        {
            var currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Home");
            }

            aTask.assignedbyId = currentUser.id;

            ViewBag.message = null;

            if (!ModelState.IsValid)
            {
                ViewBag.message = "Validation Error";
                return View(aTask);
            }

            try
            {
                _context.Tasks.Add(aTask);
                _context.SaveChanges();
                ViewBag.message = "Task Assignment Successful";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding task");
                ViewBag.message = ex.Message;
                return View(aTask);
            }

            var selectallProjects = GetAllProjects();
            ViewBag.selectProjects = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectallProjects, "id", "Projectname");

            var selectEmployeesAssignedToProject = GetEmployeesAssignedToProject(aTask.projectId);
            ViewBag.selectEmployeesAssignedToProject = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectEmployeesAssignedToProject, "userid", "assignee");

            var selectallPriorities = GetAllPriorities();
            ViewBag.selectallPriorities = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectallPriorities, "id", "priorityName");

            return View(aTask);
        }

        public IActionResult AddComment()
        {
            int tempProjectId = 1;

            var selectallProjects = GetAllProjects();
            ViewBag.selectProjects = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectallProjects, "id", "Projectname");

            var selectTasksbyProject = GetProjectSpecificTask(tempProjectId);
            ViewBag.selectTasksbyProject = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectTasksbyProject, "id", "TaskHeading");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(Comment aComment)
        {
            var currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Home");
            }

            aComment.commenterid = currentUser.id;

            ViewBag.message = null;

            if (!ModelState.IsValid)
            {
                ViewBag.message = "Validation Error";
                return View(aComment);
            }

            try
            {
                _context.Comments.Add(aComment);
                aComment.commentDate = DateTime.Now;
                _context.SaveChanges();
                ViewBag.message = "Comment Created Successful";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding comment");
                ViewBag.message = ex.Message;
                return View(aComment);
            }

            var selectallProjects = GetAllProjects();
            ViewBag.selectProjects = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectallProjects, "id", "Projectname");

            int projectId = _context.Tasks
                .Where(t => t.id == aComment.taskId)
                .Select(t => t.projectId)
                .FirstOrDefault();

            var selectTasksbyProject = GetProjectSpecificTask(projectId);
            ViewBag.selectTasksbyProject = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectTasksbyProject, "id", "TaskHeading");

            return View();
        }

        public IActionResult TaskDetailComments(int? taskid)
        {
            if (taskid == null)
            {
                return BadRequest();
            }

            int projectId = _context.Tasks
                .Where(t => t.id == taskid)
                .Select(t => t.projectId)
                .FirstOrDefault();

            string projectName = _context.Projects
                .Where(p => p.id == projectId)
                .Select(p => p.name)
                .FirstOrDefault() ?? "Unknown";
            
            string taskname = _context.Tasks
                .Where(t => t.id == taskid)
                .Select(t => t.taskHeading)
                .FirstOrDefault() ?? "Unknown";

            ViewBag.projectName = projectName;
            ViewBag.taskname = taskname;

            List<CommentView> CommentsByTaskid = _context.Comments
                .Where(t => t.taskId == taskid)
                .Select(X => new CommentView
                {
                    id = X.id,
                    taskId = X.taskId,
                    comment = X.comment,
                    commenterName = _context.Users
                        .Where(u => u.id == X.commenterid)
                        .Select(u => u.name)
                        .FirstOrDefault() ?? "Unknown",
                    commentDate = X.commentDate,
                }).ToList();

            ViewBag.CommentsByTaskid = CommentsByTaskid;
            ViewBag.commentCount = CommentsByTaskid.Count();

            return View();
        }

        public async System.Threading.Tasks.Task<IActionResult> DeleteTask(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Models.Task aTask = await _context.Tasks.FindAsync(id);
            if (aTask == null)
            {
                return NotFound();
            }
            return View(aTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> DeleteTaskConfirmed(int id)
        {
            Models.Task aTask = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(aTask);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

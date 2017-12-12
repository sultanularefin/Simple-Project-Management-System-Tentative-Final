using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Smpl_prjct_mng_mnt_tol.EF;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Smpl_prjct_mng_mnt_tol.Controllers
{
    public class TaskController : Controller
    {

        EF3 context = new EF3();
        LoginViewModel aLoginViewModel = new LoginViewModel();

        private List<SelectProject> GetAllProjects()
        {
            List<SelectProject> AllProjects = new List<SelectProject>();
            //List<selectCourseCategory> AllCourseCategories = new List<selectCourseCategory>();

            AllProjects = context.Projects.Select(x => new SelectProject { id = x.id, Projectname = x.name }).ToList();

            //AllDesignations = context.Designations.ToList();
            return AllProjects;

        }


        public JsonResult getemployeeByProject(int projectId, int LoggerId)
        {

            var response_data = GetEmployeesAssignedToProject(projectId);

            var contentType = "application/json";

            return Json(response_data, contentType);

        }

        public JsonResult getTasksByProject(int projectId, int LoggerId)
        {

            var response_data = GetProjectSpecificTask(projectId);

            var contentType = "application/json";

            return Json(response_data, contentType);

        }

        public List<SelectAssignee> GetEmployeesAssignedToProject(int projectId)
        {
            List<SelectAssignee> AllEmps = new List<SelectAssignee>();

            AllEmps = context.Assignments.Where(m => m.projectId == projectId).Select(X => new SelectAssignee
            {
                userid = X.userId,
                assignee = context.Users.Where(u => u.id == X.userId).Select(u => u.name).FirstOrDefault().ToString(),
            }).ToList();

            return AllEmps;
        }

        private List<selectPriority> GetAllPriorities()
        {
            List<selectPriority> AllPriorities = new List<selectPriority>();
            //List<selectCourseCategory> AllCourseCategories = new List<selectCourseCategory>();

            AllPriorities = context.Priorities.Select(x => new selectPriority { id = x.id, priorityName = x.priorityName }).ToList();

            //AllDesignations = context.Designations.ToList();
            return AllPriorities;

        }
        private List<SelectTask> GetProjectSpecificTask(int projectId)
        {
            List<SelectTask> AllTasksToThisProject = new List<SelectTask>();
            //List<selectCourseCategory> AllCourseCategories = new List<selectCourseCategory>();

            AllTasksToThisProject = context.Tasks.Where(m => m.projectId == projectId).Select(x => new SelectTask { id = x.id, TaskHeading = x.taskHeading }).ToList();

            //AllDesignations = context.Designations.ToList();
            return AllTasksToThisProject;

        }

        // GET: ProjectDetailTask
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddTask()
        {
            int tempProjectId = 1;

            var selectallProjects = GetAllProjects();
            ViewBag.selectProjects = new SelectList(selectallProjects, "id", "Projectname");

            var selectEmployeesAssignedToProject = GetEmployeesAssignedToProject(tempProjectId);
            ViewBag.selectEmployeesAssignedToProject = new SelectList(selectEmployeesAssignedToProject, "userid", "assignee");

            var selectallPriorities = GetAllPriorities();
            ViewBag.selectallPriorities = new SelectList(selectallPriorities, "id", "priorityName");


            return View();
        }

        [HttpPost]
        public ActionResult AddTask(EF.Task aTask)
        {

            LoginViewModel a = new LoginViewModel();
            a = (LoginViewModel)Session["logger"];

            aTask.assignedbyId = a.id;

            ViewBag.message = null;

            if (!ModelState.IsValid)
            {
                ViewBag.message = "Validation Error";
                return View(aTask);

            }

            //int duplicateTaskAdding = 0;
            //duplicateTaskAdding = Convert.ToInt32(context.Tasks.Where(m => (m.assigneeid == aTask.assigneeid) && (m.assignedbyId == aTask.assignedbyId)).Select(m => m.id).FirstOrDefault().ToString());

            //if (duplicateTaskAdding == 0)
            // {
            try
            {
                context.Tasks.Add(aTask);

                context.SaveChanges();
            }
            catch(Exception e)
            {
                ViewBag.message = e.Message;
                return View();

            }
           // }

            //if (duplicateTaskAdding != 0)
          //  {
                ViewBag.message = "Task assigned";
           // }

            ViewBag.message = "Task Assignment Successfull";


            var selectallProjects = GetAllProjects();
            ViewBag.selectProjects = new SelectList(selectallProjects, "id", "Projectname");

            //var selectallEmployees = GetEmployeesAssignedToProject(aTask.projectId);
            //ViewBag.selectEmployees = new SelectList(selectallEmployees, "userid", "assignee");

            var selectEmployeesAssignedToProject = GetEmployeesAssignedToProject(aTask.projectId);
            ViewBag.selectEmployeesAssignedToProject = new SelectList(selectEmployeesAssignedToProject, "userid", "assignee");

            var selectallPriorities = GetAllPriorities();
            ViewBag.selectallPriorities = new SelectList(selectallPriorities, "id", "priorityName");

            return View(aTask);
        }


        public ActionResult AddComment()
        {


            int tempProjectId = 1;

            var selectallProjects = GetAllProjects();
            ViewBag.selectProjects = new SelectList(selectallProjects, "id", "Projectname");

            var selectTasksbyProject = GetProjectSpecificTask(tempProjectId);
            ViewBag.selectTasksbyProject = new SelectList(selectTasksbyProject, "id", "TaskHeading");


            return View();

        }


        [HttpPost]
        public ActionResult AddComment(Comment aComment)
        {
            LoginViewModel a = new LoginViewModel();
            a = (LoginViewModel)Session["logger"];

            aComment.commenterid = a.id;

            ViewBag.message = null;

            if (!ModelState.IsValid)
            {
                ViewBag.message = "Validation Error";
                return View(aComment);

            }

            //int error = 0;
            try
            {
                context.Comments.Add(aComment);
                aComment.commentDate = DateTime.Now;

                context.SaveChanges();
            }

            catch (Exception e)
            {
                ViewBag.message = e.Message;
                return View();

            }
            ViewBag.message = "Comment Created Successfull";

            var selectallProjects = GetAllProjects();
            ViewBag.selectProjects = new SelectList(selectallProjects, "id", "Projectname");

            //int pid = new int();
            int projectId = context.Tasks.Where(t => t.id == aComment.taskId).Select(t => t.projectId).FirstOrDefault();
                 

            //var selectTasksbyProject = GetProjectSpecificTask(projectId);

            var selectTasksbyProject = GetProjectSpecificTask(projectId);
            ViewBag.selectTasksbyProject = new SelectList(selectTasksbyProject, "id", "TaskHeading");

            return View();

        }

        //Create([Bind(Include = "Id,ModelName,BrandId,BrandName,Quantity")] mobile mobile)
        public ActionResult TaskDetailComments(int? taskid)
        {
            int projectId = context.Tasks.Where(t => t.id == taskid).Select(t => t.projectId).FirstOrDefault();

            string projectName = context.Projects.Where(p => p.id == projectId).Select(p => p.name).FirstOrDefault().ToString();
            string taskname = context.Tasks.Where(t => t.id == taskid).Select(t => t.taskHeading).FirstOrDefault().ToString();

            ViewBag.projectName = projectName;
            ViewBag.taskname = taskname;

            List<CommentView> CommentsByTaskid = new List<CommentView>();

            CommentsByTaskid = context.Comments.Where(t => t.taskId == taskid).Select(X => new CommentView
            {
                id = X.id,
                taskId = X.taskId,
                comment = X.comment,
                commenterName = context.Users.Where(u => u.id == X.commenterid).Select(u => u.name).FirstOrDefault().ToString(),
                commentDate=X.commentDate,
            }).ToList();

            ViewBag.CommentsByTaskid = CommentsByTaskid;
            ViewBag.commentCount = CommentsByTaskid.Count();

            return View();
        }

        public async Task<ActionResult> DeleteTask(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EF.Task aTask = await context.Tasks.FindAsync(id);
            if (aTask == null)
            {
                return HttpNotFound();
            }
            return View(aTask);
        }

        // POST: mobiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTaskConfirmed(int id)
        {
            EF.Task aTask = await context.Tasks.FindAsync(id);
            context.Tasks.Remove(aTask);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
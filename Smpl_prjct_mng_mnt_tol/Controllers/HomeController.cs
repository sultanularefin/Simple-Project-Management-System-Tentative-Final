using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Smpl_prjct_mng_mnt_tol.EF;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Data.Entity;
using Smpl_prjct_mng_mnt_tol.Repo;


namespace Smpl_prjct_mng_mnt_tol.Controllers
{
    public class HomeController : Controller
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

        private List<SelectEmployee> GetAllEmployees()
        {
            List<SelectEmployee> AllEmps = new List<SelectEmployee>();
            //List<selectCourseCategory> AllCourseCategories = new List<selectCourseCategory>();

            AllEmps = context.Users.Where(y=>y.designationId!=1).Select(x => new SelectEmployee { id = x.id, empName = x.name }).ToList();

            //AllDesignations = context.Designations.ToList();
            return AllEmps;

        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Simple Project Management Tool's description page.";

            return View();
        }


        public ActionResult AddProject()
        {

            //ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult AddProject(Project aProject)
        {
            ViewBag.message = null;

            if (!ModelState.IsValid)
            {
                ViewBag.message = "Validation Error";
                return View(aProject);

            }
            else
            {

                using (var repo = new ProjectRepo())
                {
                    repo.Add(aProject);
                }


                //    context.Projects.Add(aProject);

                //context.SaveChanges();

                ViewBag.message = "Insertion Successful";


                return View();

            }

        }


        // create / add project // home page // project manager
        public ActionResult ProjectManagerHome()
        {

            LoginViewModel a = new LoginViewModel();
            a = (LoginViewModel)Session["logger"];

         
            var normalEmpProjects = context.Assignments.Include(m => m.Project).Where(m => m.userId == a.id).Select(X => new ProjectView
            {

                id = X.projectId,
                name = X.Project.name,
                shortName = X.Project.shortName,
                statusname = context.ProjectStatuses.Where(o => o.id == X.Project.id).Select(o => o.name).FirstOrDefault().ToString(),
                memberCount = context.Assignments.Where(o => o.projectId == X.Project.id).Count(),
                taskCount = context.Tasks.Where(s => s.projectId == X.Project.id).Count(),

            }).ToList();

            ViewBag.YourProjects = normalEmpProjects;

            return View();
        }


        public  ActionResult UpdateProject()
        {

            var allfiles = context.AllFiles.ToList();
            ViewBag.AllFiles = allfiles;

            //var logFile = new StreamWriter("E:log5.txt");
            //context.Database.Log = message => logFile.WriteLine(message);
            //logFile.Close();   //arefin vvi

            //list<User> users = Dbcontext.Users.Include(p => p.Company).Where(P => !P.IsDetected).ToList();

            List<YourProjectView> AllProjects = context.Projects.Include(m => m.AllFiles).Include(m => m.ProjectStatus).Select(X => new YourProjectView
            {
                id=X.id,
                name= X.name,
                codeName =X.name,
                description=X.description,
                status=X.ProjectStatus.name,
                startDate=X.startDate,
                endDate= X.endDate,
                duration =X.duration,


            }).ToList();

           

            ViewBag.AllProjects = AllProjects;

            //logFile.Close();   //arefin vvi
            return View();

        }

        public ActionResult AssignEmployee()
        {

             
            var selectallProjects = GetAllProjects();
            ViewBag.selectProjects = new SelectList(selectallProjects, "id", "Projectname");

            var selectallEmployees = GetAllEmployees();
            ViewBag.selectEmployees = new SelectList(selectallEmployees, "id", "empName");

            List<Assignment> allAssignment = new List<Assignment>();

            allAssignment = context.Assignments.Include(m => m.Project).Include(m => m.User).ToList();

            ViewBag.allAssignment = allAssignment;
            return View();
        
        }


        [HttpPost]

        public ActionResult AssignEmployee(Assignment oneAssignment)
        {


            var selectallProjects = GetAllProjects();
            ViewBag.selectProjects = new SelectList(selectallProjects, "id", "Projectname");

            var selectallEmployees = GetAllEmployees();
            ViewBag.selectEmployees = new SelectList(selectallEmployees, "id", "empName");

            if (TryValidateModelAssignment(oneAssignment))
            {

              
                int duplicateAssingment = 0;
                duplicateAssingment = Convert.ToInt32(context.Assignments.Where(m => (m.projectId == oneAssignment.projectId)&& (m.userId == oneAssignment.userId)).Select(m => m.id).FirstOrDefault().ToString());

                if (duplicateAssingment == 0) { 
                context.Assignments.Add(oneAssignment);

                context.SaveChanges();
                }

                if (duplicateAssingment!=0)
                {
                    ViewBag.message = "User Already Assigned to this project";
                }

                ViewBag.message = "Assignment Successfull";

            }


            //var Assignpage = context.Assignments.Include(m => m.projectId).Include(m => m.userId).ToList();

            return View();

        }

        public bool TryValidateModelAssignment(Assignment oneAssignment)
        {
            try{ 
            int projectid = (int)oneAssignment.projectId;
            int userid = (int)oneAssignment.userId;
            }
            catch(InvalidCastException )
            {
                
                return false;
            }

            //if (oneAssignment.projectId)  && (oneAssignment.userId != null))
            //{
            //    return true;
            //}
            return true;
        }



        public async Task<ActionResult> ProjectDetailUpdate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project aProject = await context.Projects.FindAsync(id);
            if (aProject == null)
            {
                return HttpNotFound();
            }


            return View(aProject);
        }

        //update project detail like projects detail will be updated
        [HttpPost]
        public async Task<ActionResult> ProjectDetailUpdate(Project aProject)
        {
            if (ModelState.IsValid)
            {
                context.Entry(aProject).State = EntityState.Modified;
                await context.SaveChangesAsync();
                //return RedirectToAction("AddUser");

            }

            ViewBag.message = "Updated Successfully";
            //var selectallDesignations = GetAllDesignations();
            //ViewBag.Designations = new SelectList(selectallDesignations, "id", "name");
            return View(aProject);
        }



        public bool TryValidateCreateProject(Assignment oneAssignment)
        {
            try
            {
                int projectid = (int)oneAssignment.projectId;
                int userid = (int)oneAssignment.userId;
            }
            catch (InvalidCastException)
            {

                return false;
            }

            //if (oneAssignment.projectId)  && (oneAssignment.userId != null))
            //{
            //    return true;
            //}
            return true;
        }
        //shows only project details
        public ActionResult ProjectDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Project aProject = await context.Projects.FindAsync(id);
            //if (aProject == null)
            //{
            //    return HttpNotFound();
            //}

            List<ProjectDetail> projectDetail = context.Projects.Where(m => m.id == id).Include(m => m.ProjectStatus).
               Select(x => new ProjectDetail
               {
                   id = x.id,
                   name = x.name,
                   shortName = x.shortName,
                   statusName = x.ProjectStatus.name,
                   startDate = x.startDate,
                   endDate = x.endDate,
                   duration = x.duration,
                   description = x.description,
                   codeName=x.codeName==string.Empty?"None":x.codeName,
                   

               }).ToList();

            ProjectDetail OurprojectDetail = projectDetail.First();


            var uploadedFiles = context.AllFiles.Where(a => a.projectId == id).Select(a => a.fname).ToList();
            ViewBag.UploadedFiles = uploadedFiles;

            //var logFile = new StreamWriter("E:log5.txt");
            //context.Database.Log = message => logFile.WriteLine(message);
            
            var AssignedMembers = context.Assignments.Where(m => m.projectId == id).Include(m=>m.userId).OrderByDescending(m=>m.id).Select(X => new AssignView
            {
                userName = X.User.name,
                designation = X.User.Designation.name,
                id=X.id,

            }).ToList();

            List<ProjectSpecificTaskView> tasksbyProject = new List<ProjectSpecificTaskView>();
            //tasksbyProject = context.Tasks.Where(m => m.projectId == id).Include(m=>m.assigneeid).ToList();
            //tasksbyProject = context.Tasks.Include(m=>m.assigneeid).Where(m => m.projectId == id).ToList();

            tasksbyProject = context.Tasks.Where(m => m.projectId == id).Select(X => new ProjectSpecificTaskView
            {
                id = X.id,
                assigndbyName = context.Users.Where(u => u.id == X.assignedbyId).Select(u => u.name).FirstOrDefault().ToString(),
                assigneeName = context.Users.Where(u => u.id == X.assigneeid).Select(u => u.name).FirstOrDefault().ToString(),
                description = X.description,
                priorityName = context.Priorities.Where(p => p.id == X.projectId).Select(p => p.priorityName).FirstOrDefault().ToString(),
                dueDate = X.dueDate,
                taskHeading = X.taskHeading,
                
            }).ToList();

            //context.Projects.Where(m => m.id == id).Include(m => m.Tasks).ToList();

            ViewBag.tasksbyProject = tasksbyProject;
            ViewBag.AssignedMembers = AssignedMembers;

            

            //logFile.Close();   //arefin vvi
            return View(OurprojectDetail);


        }


        //add file in ProjectManagerHome home add not update
        //glyphicon upload files
        public ActionResult UploadFiles(int? id)
        {
            Project tempProject = new Project();

            tempProject.id = (int)id;
            //tempProject.name = name;

            //ViewBag.project = tempProject;

            //ViewBag.message = "s";
            return View(tempProject);
        }

        // add files for the first time
        //public ActionResult UploadFilesAdd(HttpPostedFileBase file, int? Id)

        [HttpPost]
        public ActionResult UploadFiles(int id, HttpPostedFileBase file)
        {
            
            string path = "";
            //int maxSize = 1000000;
            Project oneProject = context.Projects.Find(id);

            var projectName = oneProject.name;
            int fileId=0;
            if (file != null && file.ContentLength > 0)
            {
                //int maxImageSize = 1000000;
                // using System.IO;
                var fileName = Path.GetFileName(file.FileName);
                //System.IO.Directory.CreateDirectory(Server.MapPath("~/temp/"));


                path = Path.Combine(Server.MapPath("~/Assets/"), fileName);
                file.SaveAs(path);
                AllFile onefile = new EF.AllFile();
                onefile.fname = fileName;
                onefile.projectId = id;
                context.AllFiles.Add(onefile);

                context.SaveChanges();
                fileId = onefile.id;



            }


            if (fileId != 0)
            {
                ViewBag.message = "file add successfull";
                return View();
            }


            ViewBag.message = "file add Unsuccessfull";
            return View();
        }

        public ActionResult UsersProject()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UsersProjectedit()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }






        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.ReturnUrl = "/Home/Index";

            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            ViewBag.message = null;
            if (!ModelState.IsValid)
            {
                ViewBag.message = "Validation Error";
                return View(model);
            }


            //var AllCategories = context.Categories.Include("Courses").ToList();



            else
            {

                Session["logger"] = null;


                //var normalEmpProjects = context.Assignments.Include(m => m.Project).Where(m => m.userId == a.id).Select(X => new ProjectView
                //{

                //    id = X.projectId,
                //    name = X.Project.name,
                //    shortName = X.Project.shortName,
                //    statusname = context.ProjectStatuses.Where(o => o.id == X.Project.id).Select(o => o.name).FirstOrDefault().ToString(),
                //    memberCount = context.Assignments.Where(o => o.projectId == X.Project.id).Count(),
                //    taskCount = context.Tasks.Where(s => s.projectId == X.Project.id).Count(),

                //}).ToList();


                //var statusname = context.ProjectStatuses.Where(o => o.id == 2).Select(o => o.name).FirstOrDefault().ToString();

                //Where(m => (m.projectId == oneAssignment.projectId) && (m.userId == oneAssignment.userId))

                //.Select(m => m.id).FirstOrDefault().ToString());

                //var logFile2 = new StreamWriter("E:log6.txt");
                //context.Database.Log = message => logFile2.WriteLine(message);


                //User theUser = context.Users.Find(3);

                User aUser = context.Users.Include(m=>m.Designation).Where(m => (m.email == model.Email) && (m.password == model.Password)).FirstOrDefault();


                //var aUser = from U in context.Users.Include("Designations")
                //            where ((U.email == model.Email) && (U.password == model.Password))
                //            select U;

              /*  logFile2.Close(); */  //arefin vvi


                if (aUser == null)
                {
                    ViewBag.message = "User Not found";
                    return View();

                }



                User ourUser = new EF.User();

                ourUser = aUser;

                //ourUser = aUser;
                //Session["loggedinEmail"] = ourUser.email;


                aLoginViewModel.id = ourUser.id;
                aLoginViewModel.Email = ourUser.email;
                aLoginViewModel.Password = ourUser.password;
                aLoginViewModel.PersonalName = ourUser.name;
                aLoginViewModel.designationName = ourUser.Designation.name;

                Session["logger"] = aLoginViewModel;


                //return View(aLoginViewModel);

                if (String.Compare(aLoginViewModel.designationName, "It Admin", true) == 0)
                {
                    return RedirectToAction("AddUser", "User");
                }

                else if (aLoginViewModel.designationName == "Project Manager")
                {
                    return RedirectToAction("ProjectManagerHome", "Home");
                }

                else if (aLoginViewModel.designationName == "Team Lead")
                {
                    return RedirectToAction("TeamLead", "Home");
                }

                else if (aLoginViewModel.designationName == "Developer")
                {
                    return RedirectToAction("Developer", "Home");
                }

                if (aLoginViewModel.designationName == "QA Engineer")
                {
                    return RedirectToAction("QAEngineer", "Home");
                }
                else if (aLoginViewModel.designationName == "UX Engineer")
                {
                    return RedirectToAction("UXEngineer", "Home");
                }
                else return RedirectToLocal(returnUrl);

                
            }

        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {

            Session["logger"] = null;

            //return Content("<script language='javascript' type='text/javascript'>alert('Hello world!');</script>");
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


        //public ActionResult AdminPage()
        //{
        //    return View();
        //}

        //public ActionResult ProjectManager()
        //{
        //    return View();
        //}




        public ActionResult TeamLead()
        {
            LoginViewModel a = new LoginViewModel();
            a = (LoginViewModel)Session["logger"];

            //var statusname = context.ProjectStatuses.Where(o => o.id == 2).Select(o => o.name).FirstOrDefault().ToString();

            // var findEmployee = context.Users.Include(m => m.Designation).Where(m => m.Designation.name == "Team Lead");
            // normal employee
            var normalEmpProjects = context.Assignments.Include(m => m.Project).Where(m => m.userId == a.id).Select(X => new ProjectView
            {

                id = X.projectId,
                name = X.Project.name,
                shortName = X.Project.shortName,
                statusname = context.ProjectStatuses.Where(o => o.id == X.Project.id).Select(o => o.name).FirstOrDefault().ToString(),
                memberCount = context.Assignments.Where(o => o.projectId == X.Project.id).Count(),
                taskCount = context.Tasks.Where(s => s.projectId == X.Project.id).Count(),

            }).ToList();

            ViewBag.YourProjects = normalEmpProjects;

            return View();
        
        }

        public ActionResult Developer()
        {

            LoginViewModel a = new LoginViewModel();
            a = (LoginViewModel)Session["logger"];

            //var statusname = context.ProjectStatuses.Where(o => o.id == 2).Select(o => o.name).FirstOrDefault().ToString();

            // var findEmployee = context.Users.Include(m => m.Designation).Where(m => m.Designation.name == "Team Lead");
            // normal employee
            var normalEmpProjects = context.Assignments.Include(m => m.Project).Where(m => m.userId == a.id).Select(X => new ProjectView
            {

                id = X.projectId,
                name = X.Project.name,
                shortName = X.Project.shortName,
                statusname = context.ProjectStatuses.Where(o => o.id == X.Project.id).Select(o => o.name).FirstOrDefault().ToString(),
                memberCount = context.Assignments.Where(o => o.projectId == X.Project.id).Count(),
                taskCount = context.Tasks.Where(s => s.projectId == X.Project.id).Count(),

            }).ToList();


            ViewBag.YourProjects = normalEmpProjects;

            return View();
          
        }

        public ActionResult QAEngineer()
        {

            LoginViewModel a = new LoginViewModel();
            a = (LoginViewModel)Session["logger"];

            //var statusname = context.ProjectStatuses.Where(o => o.id == 2).Select(o => o.name).FirstOrDefault().ToString();

            // var findEmployee = context.Users.Include(m => m.Designation).Where(m => m.Designation.name == "Team Lead");
            // normal employee
            var normalEmpProjects = context.Assignments.Include(m => m.Project).Where(m => m.userId == a.id).Select(X => new ProjectView
            {

                id = X.projectId,
                name = X.Project.name,
                shortName = X.Project.shortName,
                statusname = context.ProjectStatuses.Where(o => o.id == X.Project.id).Select(o => o.name).FirstOrDefault().ToString(),
                memberCount = context.Assignments.Where(o => o.projectId == X.Project.id).Count(),
                taskCount = context.Tasks.Where(s => s.projectId == X.Project.id).Count(),

            }).ToList();


            ViewBag.YourProjects = normalEmpProjects;

            return View();
            //return View();
        }


        public ActionResult UXEngineer()
        {
            LoginViewModel a = new LoginViewModel();
            a = (LoginViewModel)Session["logger"];

            //var statusname = context.ProjectStatuses.Where(o => o.id == 2).Select(o => o.name).FirstOrDefault().ToString();

            // var findEmployee = context.Users.Include(m => m.Designation).Where(m => m.Designation.name == "Team Lead");
            // normal employee
            var normalEmpProjects = context.Assignments.Include(m => m.Project).Where(m => m.userId == a.id).Select(X => new ProjectView
            {

                id = X.projectId,
                name = X.Project.name,
                shortName = X.Project.shortName,
                statusname = context.ProjectStatuses.Where(o => o.id == X.Project.id).Select(o => o.name).FirstOrDefault().ToString(),
                memberCount = context.Assignments.Where(o => o.projectId == X.Project.id).Count(),
                taskCount = context.Tasks.Where(s => s.projectId == X.Project.id).Count(),

            }).ToList();


            ViewBag.YourProjects = normalEmpProjects;

            return View();
            //return View();
        }
    }
}
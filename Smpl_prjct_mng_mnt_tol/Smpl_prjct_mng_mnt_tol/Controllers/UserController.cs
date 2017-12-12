using Smpl_prjct_mng_mnt_tol.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Smpl_prjct_mng_mnt_tol.Controllers
{
    public class UserController : Controller
    {
        EF3 context = new EF3();

        private List<selectDesignation> GetAllDesignations()
        {
            List<selectDesignation> AllDesignations = new List<selectDesignation>();
            //List<selectCourseCategory> AllCourseCategories = new List<selectCourseCategory>();

            AllDesignations = context.Designations.Select(x => new selectDesignation { id = x.id, name = x.name }).ToList();

            //AllDesignations = context.Designations.ToList();
            return AllDesignations;

        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AddUser()
        {

            var AllUsers = context.Users.Include(m => m.Designation).ToList(); ;
            ViewBag.AllUsers = AllUsers;

           

            var selectallDesignations = GetAllDesignations();
            ViewBag.Designations = new SelectList(selectallDesignations, "id", "name");


            return View();
        }

        public bool TryValidateModel(User auser)
        {

            //if ((auser.email != string.Empty) && (auser.password != string.Empty) && (auser.name != string.Empty) && (auser.status != null) && (auser.designationId != null))

                if ((auser.email != string.Empty) && (auser.password != string.Empty) && (auser.name != string.Empty))
            {
                return true;
            }
            return false;



        }
        [HttpPost]

        public ActionResult AddUser(User aUser)
        {

            ViewBag.message = null;
            if (!TryValidateModel(aUser))
            {


                ViewBag.message = "Validation Error";
                return View(aUser);
            }


            //var AllCategories = context.Categories.Include("Courses").ToList();

            else
            {
                context.Users.Add(aUser);

                context.SaveChanges();

                ViewBag.message = "Insertion Successful";


                var selectallDesignations = GetAllDesignations();
                ViewBag.Designations = new SelectList(selectallDesignations, "id", "name");

                var AllUsers = context.Users.Include(m => m.Designation).ToList(); ;
                ViewBag.AllUsers = AllUsers;

                return View();

            }


        }


        //[HttpPost]

        //public ActionResult AddUser(int Coursetimeselected, int selectedcoursecategory, int selectedcoursecountry)
        //{
        //    var allCourseTimes = GetAllCourseTime();

        //    var a = from t in allCourseTimes where t.Id == Coursetimeselected select new selectcourseTime { Id = t.Id, CourseTimeString = t.CourseTimeString };

        //    string Coursetime = a.FirstOrDefault().CourseTimeString;
        //    var AllCourses = context.Courses.Where(x => x.CategoryID == selectedcoursecategory || x.CourseTime2 == Coursetime).ToList();


        //    return View(AllCourses);
        //}


        public async Task<ActionResult> ResetDefault(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            User auser = await context.Users.FindAsync(id);
            if (auser == null)
            {
                return HttpNotFound();
            }
            auser.password = auser.email + "123";



            return RedirectToAction("AddUser", "User");
        }


        public async Task<ActionResult> UpdateUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User aUser = await context.Users.FindAsync(id);
            if (aUser == null)
            {
                return HttpNotFound();
            }

            var AllUsers = context.Users.Include(m => m.Designation).ToList(); ;
            ViewBag.AllUsers = AllUsers;



            var selectallDesignations = GetAllDesignations();
            ViewBag.Designations = new SelectList(selectallDesignations, "id", "name");

            return View(aUser);

        }

        [HttpPost]
        //public async Task<ActionResult> UpdateUser([Bind(Include = "Id,ModelName,BrandId,BrandName,Quantity")] User aUser)
        public async Task<ActionResult> UpdateUser([Bind(Include = "id,name,email,password,status,designationId")]User aUser)
        {
            if (ModelState.IsValid)
            {

                context.Entry(aUser).State = EntityState.Modified;
                await context.SaveChangesAsync();
                //return RedirectToAction("AddUser");
            }

            ViewBag.message = "Updated Successfully";
            var selectallDesignations = GetAllDesignations();
            ViewBag.Designations = new SelectList(selectallDesignations, "id", "name");
            return View(aUser);
        }

        public async Task<ActionResult> DeleteUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User aUser = await context.Users.FindAsync(id);
            if (aUser == null)
            {
                return HttpNotFound();
            }

        
            

           return View(aUser);
        }

      

        // POST: mobiles/Delete/5
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUserConfirmed(int id)
        {
            User aUser = await context.Users.FindAsync(id);
            context.Users.Remove(aUser);
            await context.SaveChangesAsync();
            ViewBag.message = "deletion successful";
            
            //return RedirectToAction("AddUser");

            return View(aUser);
        }



        public async Task<ActionResult> ViewUsers()

        {

            var inventories = context.Users.Include("Users");

            return View(await inventories.ToListAsync());
            //return View();
        }


    }
}
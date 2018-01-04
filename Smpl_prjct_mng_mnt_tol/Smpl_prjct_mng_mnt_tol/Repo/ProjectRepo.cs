using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Smpl_prjct_mng_mnt_tol.EF;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Smpl_prjct_mng_mnt_tol.Repo
{
    public class ProjectRepo : BaseRepo<Project>, IRepo<Project>
    {
        public ProjectRepo()
        {
            Table = Context.Projects;
        }

        public int Delete(int id)
        {
            Context.Entry(new Project() { id = id }).State = EntityState.Deleted;
            return SaveChanges();
           // throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            Context.Entry(new Project() { id = id }).State = EntityState.Deleted;
            return SaveChangesAsync();
            //throw new NotImplementedException();
        }
    }
}
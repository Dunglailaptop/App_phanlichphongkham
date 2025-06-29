using Phanlichphongkham.Data;
using Phanlichphongkham.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Presenters
{
    public class ExaminationPresenter
    {
        private readonly AppDbContext appDbContext;

        public ExaminationPresenter()
        {
            this.appDbContext = new AppDbContext();
        }
        public async Task<List<Model.Examination>> GetListAsync()
        {
            try
            {
                List<Model.Examination> listzone = appDbContext.Examinations.ToList();
                return listzone;
            }
            catch (Exception ex)
            {
                return new List<Model.Examination>();
            }
        }
        public bool inesrt(List<Examination> examination)
        {
            try
            {
                appDbContext.Examinations.AddRange(examination);
                appDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex) { 
             return false;
            }
        }
    }
}

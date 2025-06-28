using Phanlichphongkham.Data;
using Phanlichphongkham.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Controller
{
    public class DepartmentAppointmentSchedulingPresenter
    {
        private readonly AppDbContext _appDbContext;

        public DepartmentAppointmentSchedulingPresenter()
        {
            _appDbContext = new AppDbContext();
        }
        //public List<DepartmentalAppointmentScheduling> getList()
        //{

        //    try
        //    {
        //        var result = _appDbContext.DepartmentalAppointmentSchedulings.ToList();
        //        if (result.Count > 0) {
        //            return result;
        //        }else
        //        {
        //            return new List<DepartmentalAppointmentScheduling>();
        //        }
        //    }
        //    catch (Exception ex) { 
        //      return new List<DepartmentalAppointmentScheduling>();
        //    }
        //}
    }
}

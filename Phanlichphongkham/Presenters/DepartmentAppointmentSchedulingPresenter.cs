using HospitalDataLibrarys.Services;
using Phanlichphongkham.Data;
using Phanlichphongkham.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Controller
{
    public class DepartmentAppointmentSchedulingPresenter
    {
        private readonly AppDbContext _appDbContext;

        private readonly Service_DepartmentalAppointmentScheduling  _DepartmentalAppointmentScheduling;

        public DepartmentAppointmentSchedulingPresenter()
        {
            _appDbContext = new AppDbContext();
            _DepartmentalAppointmentScheduling = new Service_DepartmentalAppointmentScheduling();
        }
        public async Task<List<Model.DepartmentalAppointmentScheduling>> GetListAsync()
        {
            try
            {
                List<Model.DepartmentalAppointmentScheduling> listzone = _appDbContext.DepartmentalAppointmentSchedulings.ToList();
                return listzone;
            }
            catch (Exception ex)
            {
                return new List<Model.DepartmentalAppointmentScheduling>();
            }
        }
        public bool AddNew(BindingList<Model.DepartmentalAppointmentScheduling> DepartmentalAppointmentSchedulings)
        {
            try
            {
                _appDbContext.DepartmentalAppointmentSchedulings.AddRangeAsync(DepartmentalAppointmentSchedulings);
                _appDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

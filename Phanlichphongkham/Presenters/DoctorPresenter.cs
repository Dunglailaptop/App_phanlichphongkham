using HospitalDataLibrarys.Services;
using Microsoft.EntityFrameworkCore;
using Phanlichphongkham.Data;
using Phanlichphongkham.Helper;
using Phanlichphongkham.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Presenters
{
    public class DoctorPresenter
    {
        private readonly AppDbContext appDbContext;
        private readonly Service_DepartmentalAppointmentScheduling _DepartmentalAppointmentScheduling;

        public DoctorPresenter()
        {
            appDbContext = new AppDbContext();
            _DepartmentalAppointmentScheduling = new Service_DepartmentalAppointmentScheduling();
        }
        public async Task<bool> UpdateDatabasePostgresqlWithLogging()
        {
            try
            {
                appDbContext.ChangeTracker.Clear();
                var result = await _DepartmentalAppointmentScheduling.GetListDoctor();
                var rooms = new List<Model.Doctor>();



                foreach (var item in result)
                {

                    var roomnew = new Model.Doctor();
                    roomnew.Name = item.tennhanvien;
                    roomnew.DepartmentHospital_Id = 1; // Đảm bảo đây là ID đúng
                    roomnew.Doctor_Id_progres = item.tennhanvien;
                    roomnew.Doctor_Code = item.manhanvien;
                    roomnew.DateUpdate = DateTime.Now;
                    roomnew.DateCreate = DateTime.Now;
                    roomnew.Enable = true;


                    rooms.Add(roomnew);

                }

                Console.WriteLine($"Sẽ thêm {rooms.Count} rooms vào database");

                if (rooms.Count > 0)
                {
                    // Sửa lỗi AddRangeAsync
                    appDbContext.Doctors.AddRange(rooms);
                    await appDbContext.SaveChangesAsync();
                    Console.WriteLine($"Đã thêm {rooms.Count} rooms thành công");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return false;
            }
        }
        public async Task<List<Model.Doctor>> GetListAsync()
        {
            try
            {
                List<Model.Doctor> listzone = appDbContext.Doctors.ToList();
                return listzone;
            }
            catch (Exception ex)
            {
                return new List <Model.Doctor>();
            }
        }


    }
}

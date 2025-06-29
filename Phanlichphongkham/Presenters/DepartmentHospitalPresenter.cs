using HospitalDataLibrarys.Services;
using Phanlichphongkham.Data;
using Phanlichphongkham.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Presenters
{
    public class DepartmentHospitalPresenter
    {
        private readonly AppDbContext _appDbContext;

        private readonly Service_DepartmentalAppointmentScheduling service_DepartmentalAppointmentScheduling;

        public DepartmentHospitalPresenter()
        {
            _appDbContext = new AppDbContext();
            service_DepartmentalAppointmentScheduling = new Service_DepartmentalAppointmentScheduling();
        }
        public async Task<List<Model.DepartmentHospital>> GetListAsync()
        {
            try
            {
                List<Model.DepartmentHospital> listzone = _appDbContext.DepartmentHospitals.ToList();
                return listzone;
            }
            catch (Exception ex)
            {
                return new List<Model.DepartmentHospital>();
            }
        }
        public async Task<bool> UpdateDatabasePostgresqlWithLogging()
        {
            try
            {
                _appDbContext.ChangeTracker.Clear();
                var result = await service_DepartmentalAppointmentScheduling.GetListDepartMentHospital();
                var rooms = new List<Model.DepartmentHospital>();

            

                // Debug: In ra zones tìm được
               

                foreach (var item in result)
                {
                  
                      
                        var roomnew = new Model.DepartmentHospital();
                        roomnew.Name = item.tenkhoaphong;
                        roomnew.DepartmentHospital_code = Contants.RemoveVietnameseAndSpaces(item.tenkhoaphong); // Đảm bảo đây là ID đúng
                        roomnew.DepartmentHospital_id_posgres = item.idkhoaphong;
                        roomnew.DateUpdate = DateTime.Now;
                        roomnew.DateCreate = DateTime.Now;
                        roomnew.Enable = true;

                      
                        rooms.Add(roomnew);
                
                }

                Console.WriteLine($"Sẽ thêm {rooms.Count} rooms vào database");

                if (rooms.Count > 0)
                {
                    // Sửa lỗi AddRangeAsync
                    _appDbContext.DepartmentHospitals.AddRange(rooms);
                    await _appDbContext.SaveChangesAsync();
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
    }
}

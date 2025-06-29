using HospitalDataLibrarys.Services;
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
    public class SpecialtyPresenter
    {
        private readonly AppDbContext _appDbContext;

        private readonly Service_DepartmentalAppointmentScheduling _DepartmentalAppointmentScheduling;

        public SpecialtyPresenter()
        {
            _appDbContext = new AppDbContext();
            _DepartmentalAppointmentScheduling = new Service_DepartmentalAppointmentScheduling();
        }
        public async Task<bool> UpdateDatabasePostgresqlWithLogging()
        {
            try
            {
                _appDbContext.ChangeTracker.Clear();
                var result = await _DepartmentalAppointmentScheduling.GetListSpecialty();
                var rooms = new List<Model.Sepicalty>();

              

                // Debug: In ra zones tìm được
                
                foreach (var item in result)
                {
                   
                    
                        var roomnew = new Model.Sepicalty();
                        roomnew.Name = item.tennhomphongkham;
                        roomnew.Sepicalty_id_posgres = item.idnhomphongkham; // Đảm bảo đây là ID đúng
                        roomnew.Sepicalty_code = Contants.RemoveVietnameseAndSpaces(item.tennhomphongkham);
                        roomnew.DateUpdate = DateTime.Now;
                        roomnew.DateCreate = DateTime.Now;
                        roomnew.Enable = true;

                        Console.WriteLine($"Tạo room với Zone_Id: {roomnew.Sepicalty_Id}");
                        rooms.Add(roomnew);
                  
                }

                Console.WriteLine($"Sẽ thêm {rooms.Count} rooms vào database");

                if (rooms.Count > 0)
                {
                    // Sửa lỗi AddRangeAsync
                    _appDbContext.Sepicaltys.AddRange(rooms);
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
        public async Task<List<Model.Sepicalty>> GetListAsync()
        {
            try
            {
                List<Model.Sepicalty> listzone = _appDbContext.Sepicaltys.ToList();
                return listzone;
            }
            catch (Exception ex)
            {
                return new List<Model.Sepicalty>();
            }
        }
    }
}

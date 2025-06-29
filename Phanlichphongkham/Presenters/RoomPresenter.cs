using HospitalDataLibrarys.Models;
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
    public class RoomPresenter
    {
        private readonly AppDbContext _appDbContext;
        private readonly Service_DepartmentalAppointmentScheduling service_DepartmentalAppointmentScheduling;

        public RoomPresenter()
        {
            service_DepartmentalAppointmentScheduling = new Service_DepartmentalAppointmentScheduling();
            _appDbContext = new AppDbContext();
        }
        // Phiên bản với logging tốt hơn
        public async Task<bool> UpdateDatabasePostgresqlWithLogging()
        {
            try
            {
                _appDbContext.ChangeTracker.Clear();
                var result = await service_DepartmentalAppointmentScheduling.GetListRoom();
                var rooms = new List<Model.Room>();

                // Tạo dictionary để map idkhu với zone
                var zoneMapping = new Dictionary<int, Model.Zone>();

                // Sửa lỗi dấu *
                var zone66 = await _appDbContext.Zones.FirstOrDefaultAsync(x => x.Zone_Id_posgres == 66);
                var zone68 = await _appDbContext.Zones.FirstOrDefaultAsync(x => x.Zone_Id_posgres == 68);
                var zone65 = await _appDbContext.Zones.FirstOrDefaultAsync(x => x.Zone_Id_posgres == 65);

                if (zone65 != null) zoneMapping[65] = zone65;
                if (zone68 != null) zoneMapping[68] = zone68;
                if (zone66 != null) zoneMapping[66] = zone66;

                Console.WriteLine($"Tìm thấy {zoneMapping.Count} zones");
                Console.WriteLine($"Tổng số rooms từ API: {result.Count}");

                // Debug: In ra zones tìm được
                foreach (var zone in zoneMapping)
                {
                    Console.WriteLine($"Zone {zone.Key}: ID={zone.Value.Zone_Id}, Name={zone.Value.Name}");
                }

                foreach (var item in result)
                {
                    Console.WriteLine($"Đang xử lý room: {item.tenkhoaphong}, idkhu: {item.idkhu}");

                    if (zoneMapping.ContainsKey(item.idkhu))
                    {
                        var selectedZone = zoneMapping[item.idkhu];
                        var roomnew = new Model.Room();
                        roomnew.Name = item.tenkhoaphong;
                        roomnew.Zone_Id = selectedZone.Zone_Id; // Đảm bảo đây là ID đúng
                        roomnew.Room_IdZone_posgres = item.idkhu;
                        roomnew.Room_code = Contants.RemoveVietnameseAndSpaces(item.tenkhoaphong);
                        roomnew.DateUpdate = DateTime.Now;
                        roomnew.DateCreate = DateTime.Now;
                        roomnew.Enable = true;

                        Console.WriteLine($"Tạo room với Zone_Id: {roomnew.Zone_Id}");
                        rooms.Add(roomnew);
                    }
                    else
                    {
                        Console.WriteLine($"Không tìm thấy zone cho idkhu: {item.idkhu}");
                    }
                }

                Console.WriteLine($"Sẽ thêm {rooms.Count} rooms vào database");

                if (rooms.Count > 0)
                {
                    // Sửa lỗi AddRangeAsync
                    _appDbContext.Rooms.AddRange(rooms);
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
        public async Task<List<Model.Room>> GetListAsync()
        {
            try
            {
                List<Model.Room> listzone = _appDbContext.Rooms.ToList();
                return listzone;
            }
            catch (Exception ex)
            {
                return new List<Model.Room>();
            }
        }
        public async Task<List<Model.Room>> GetListAsyncWithId(int Id)
        {
            try
            {
                List<Model.Room> listzone = _appDbContext.Rooms.Where(x=>x.Zone_Id == Id).ToList();
                return listzone;
            }
            catch (Exception ex)
            {
                return new List<Model.Room>();
            }
        }
    }
}

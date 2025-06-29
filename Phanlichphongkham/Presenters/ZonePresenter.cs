using Phanlichphongkham.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Phanlichphongkham.Model;

namespace Phanlichphongkham.Presenters
{
    public class ZonePresenter
    {
        private readonly AppDbContext appDbContext;
        public ZonePresenter() {
            appDbContext = new AppDbContext();
        }
        public async Task<List<Model.Zone>> GetListAsync()
        {
            try
            {
                List<Model.Zone> listzone = appDbContext.Zones.ToList();
                return listzone;
            }
            catch (Exception ex)
            {
                return new List<Model.Zone>();
            }
        }
        public bool AddNew(Model.Zone zone)
        {
            try
            {
                appDbContext.Zones.Add(zone);
                appDbContext.SaveChanges();
                return true;
            }catch (Exception ex){
              return false;
            }
        }
    }
}

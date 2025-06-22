using Phanlichphongkham.Data;
using Phanlichphongkham.Model;
using Phanlichphongkham.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Controller
{
    public  class SlotTimeController
    {
        private readonly AppDbContext _appDbContext;

        public SlotTimeController()
        {
            _appDbContext = new AppDbContext();
        }

        public List<Examination> getlistExmination()
        {
            try
            {
                var result = _appDbContext.Examinations.ToList();
                return result;
            }
            catch (Exception ex) { 
               return new List<Examination>();
            }
        
        }

        public bool InsertExaminations(List<Examination> examinations)
        {
            try
            {
                if (examinations == null || examinations.Count == 0)
                    return false;

                _appDbContext.Examinations.AddRange(examinations);
                _appDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine("Lỗi insert: " + ex.Message);
                return false;
            }
        }

    }
}

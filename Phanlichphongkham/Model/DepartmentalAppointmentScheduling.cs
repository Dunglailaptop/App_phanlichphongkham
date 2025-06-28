using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Model
{
    public class DepartmentalAppointmentScheduling
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentalAppointmentScheduling_Id { get; set; }
        public string DepartmentalAppointmentScheduling_Code { get; set; }
        public int Year { get; set; }
        public int Week { get; set; }
        public string DayInWeek { get; set; } // nvarchar(20)
        public DateTime DateInWeek { get; set; }
        public int Total { get; set; }
        public bool Status { get; set; }

        public int Specialty_id { get; set; }
        public int Room_id { get; set; }
        public int Examination_Id { get; set; }
        public int Doctor_id { get; set; }
        public int DepartmentHospital_Id { get; set; }

        public string Username { get; set; } // nvarchar(50)

        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }

        // Các mối quan hệ khác có thể thêm sau
        public Doctor Doctor { get; set; }
        public Examination Examination { get; set; }
        public DepartmentHospital DepartmentHospital { get; set; }

        public Room Room { get; set; }
        
        public Sepicalty Sepicalty { get; set; }
    }
   
}

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
        [Browsable(false)]
        public int DepartmentalAppointmentScheduling_Id { get; set; }
        [Browsable(false)]
        public string DepartmentalAppointmentScheduling_Code { get; set; } = "LICHKHAM";
        [DisplayName("Năm")]
        public int Year { get; set; }
        [DisplayName("Tuần")]
        public int Week { get; set; }
        [DisplayName("Thứ")]
        public string DayInWeek { get; set; } // nvarchar(20)
        [DisplayName("Ngày")]
        public DateTime DateInWeek { get; set; }
        [DisplayName("Số lượng")]
        public int Total { get; set; }
        [DisplayName("Trạng thái")]
        public bool Status { get; set; }
        [DisplayName("Chuyên khoa")]
        public int Specialty_id { get; set; }
        [DisplayName("Danh sách phòng")]
        public int Room_Id { get; set; }
        [DisplayName("Ca khám")]
        public int Examination_Id { get; set; }
        [DisplayName("Bác sĩ")]
        public int Doctor_Id { get; set; }
        [DisplayName("Khoa phòng")]
        public int DepartmentHospital_Id { get; set; }
        [Browsable(false)]
        public string Username { get; set; } = "Admin"; // nvarchar(50)
        [Browsable(false)]
        public int TotalOrder { get; set; }
        [Browsable(false)]
        public bool Lock {  get; set; }

        [Browsable(false)]
        public DateTime DateCreate { get; set; } = DateTime.Now;
        [Browsable(false)]
        public DateTime DateUpdate { get; set; } = DateTime.Now;

        // Các mối quan hệ khác có thể thêm sau
        [Browsable(false)]
        public Doctor Doctor { get; set; }
        [Browsable(false)]
        public Examination Examination { get; set; }
        [Browsable(false)]
        public DepartmentHospital DepartmentHospital { get; set; }
        [Browsable(false)]
        public Room Room { get; set; }
        [Browsable(false)]
        public Sepicalty Sepicalty { get; set; }
    }
   
}

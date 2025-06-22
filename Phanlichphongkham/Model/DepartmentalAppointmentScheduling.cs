using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DisplayName("Mã phòng khám")]
        public int Room_Id { get; set; }
        [DisplayName("Mã chuyên khoa")]
        public int Specialty_Id { get; set; }
        [DisplayName("Mã khoa phòng")]
        public int DepartmentHospital_Id { get; set; }
        [DisplayName("Ngày trong tuần")]
        public DateTime DateInWeek { get; set; }
        [DisplayName("Tuần")]
        public int Week { get; set; }
        [DisplayName("Năm")]
        public int Year { get; set; }
        [Browsable(false)]
        public int DayOfTheWeek_Id { get; set; }
        [Browsable(false)]
        public int Examination_Id { get; set; }
        [DisplayName("Trạng thái")]
        public int Status { get; set; }
        [DisplayName("Mã Bác sĩ")]
        public int Doctor_Id { get; set; }

        // Navigation properties (nếu cần)
        [Browsable(false)]
        public DayOfTheWeek DayOfTheWeek { get; set; }
        [Browsable(false)]
        public Examination Examination { get; set; }
   
        // Các mối quan hệ khác có thể thêm sau
    }
}

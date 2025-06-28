using DevExpress.XtraScheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Model
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Doctor_Id { get; set; }
        public string Doctor_Code { get; set; }
        public string Name { get; set; } // nvarchar(20)
        public int Doctor_Id_progres { get; set; }
        public int DepartmentHospital_Id { get; set; }
        public bool Enable { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        //khoa ngoại
        public DepartmentHospital DepartmentHospital { get; set; }
        public ICollection<DepartmentalAppointmentScheduling> DepartmentalAppointmentScheduling { get; set; }


    }
}

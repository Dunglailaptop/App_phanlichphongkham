using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Model
{
    public class DepartmentHospital
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentHospital_Id { get; set; }
        public string DepartmentHospital_code { get; set; }
        public string Name { get; set; }
        public string DepartmentHospital_id_posgres { get; set; }
        public bool Enable { get; set; }
        public DateTime DateUpdate { get; set; }
        public DateTime DateCreate { get; set; }
        //khoa ngoại
        public ICollection<Doctor> Doctor { get; set; }
        public ICollection<DepartmentalAppointmentScheduling> DepartmentalAppointmentScheduling { get; set; }
    }
}

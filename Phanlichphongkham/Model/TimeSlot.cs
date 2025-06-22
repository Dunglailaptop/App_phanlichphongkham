using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Model
{
    public class TimeSlot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TimeSlot_Id { get; set; }
        public string TimeSlot_code { get; set; }
        public int Examination_Id { get; set; }
        public string Name { get; set; } // nvarchar(20)

        // Navigation property
        public Examination Examination { get; set; }
        public ICollection<DepartmentalAppointmentScheduling> AppointmentSchedules { get; set; }
    }
}

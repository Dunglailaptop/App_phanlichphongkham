using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Model
{
    public class Examination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Examination_Id { get; set; }
        public string Examination_Code { get; set; }
        public string Name { get; set; } // nvarchar(20)
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Total { get; set; }

        // Navigation property
        public ICollection<DepartmentalAppointmentScheduling> AppointmentSchedules { get; set; }
        public ICollection<TimeSlot> TimeSlots { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Model
{
    public class DayOfTheWeek
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DayOfTheWeek_Id { get; set; }
        public int DayOfTheWeek_Code { get; set; }
        public string Name { get; set; } // nvarchar(50)

        // Navigation property
        public ICollection<DepartmentalAppointmentScheduling> AppointmentSchedules { get; set; }
    }
}

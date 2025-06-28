using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Model
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Room_Id { get; set; }
        public string? Room_code { get; set; }
        public int? Zone_id { get; set; }
        public string? Name { get; set; }
        public string? Room_id_posgres { get; set; }
        public bool Enable { get; set; }
        public DateTime? DateUpdate { get; set; }
        public DateTime? DateCreate { get; set; }
        //khoa ngoại
        public Zone Zone { get; set; }
        public ICollection<DepartmentalAppointmentScheduling> DepartmentalAppointmentScheduling { get; set; }
    }

}

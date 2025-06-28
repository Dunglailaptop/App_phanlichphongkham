using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Model
{
    public class Sepicalty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Sepicalty_Id { get; set; }
        public string? Sepicalty_code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ListType { get; set; }
        public int? Sepicalty_id_posgres { get; set; }
        public bool Enable { get; set; }
        public DateTime? DateUpdate { get; set; }
        public DateTime? DateCreate { get; set; }
        //khoa ngoại
        public ICollection<DepartmentalAppointmentScheduling> DepartmentalAppointmentScheduling { get; set; }
        public ICollection<SepcialtyJoinZone> SepcialtyJoinZone { get; set; }
    }

}

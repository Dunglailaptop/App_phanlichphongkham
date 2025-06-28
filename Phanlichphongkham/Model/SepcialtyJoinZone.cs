using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Model
{
    public class SepcialtyJoinZone
    {
        [Key]
        public int Zone_Id { get; set; }
        public int Specialty_id { get; set; }
        public bool Status { get; set; }
        public DateTime DateUpdate { get; set; }
        public DateTime DateCreate { get; set; }

        //khoa ngoại
        public Zone Zone { get; set; }
        public Sepicalty Sepicalty { get; set; }
    }
}

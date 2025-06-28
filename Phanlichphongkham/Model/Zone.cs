using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Model
{
    public class Zone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Zone_Id { get; set; }
        public string Zone_code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Zone_Id_posgres { get; set; }
        public bool Enable { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        //khoa ngoại
        public ICollection<Room> Room { get; set; }
        public ICollection<ServicePrice> ServicePrice { get; set; }
        public ICollection<SepcialtyJoinZone> SepcialtyJoinZone { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanlichphongkham.Model
{
    public class ServicePrice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServicePrice_Id { get; set; }
        public string ServicePrice_Code { get; set; }
        public int Zone_Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ServicePrice_Id_progres { get; set; }
        public bool Enable { get; set; }
        public DateTime DateUpdate { get; set; }
        public DateTime DateCreate { get; set; }
        //khoa ngoại
        public Zone Zone { get; set; }
    }
}

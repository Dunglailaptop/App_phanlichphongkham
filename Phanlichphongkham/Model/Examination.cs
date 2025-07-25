﻿using System;
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
        public bool Enable { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        //khoa ngoại
        public ICollection<DepartmentalAppointmentScheduling> DepartmentalAppointmentScheduling { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Phanlichphongkham.Model;
using System.Configuration;


namespace Phanlichphongkham.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<DepartmentalAppointmentScheduling> DepartmentalAppointmentSchedulings { get; set; }
        public DbSet<DayOfTheWeek> DayOfTheWeeks { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        
            var connectionString = ConfigurationManager
                .ConnectionStrings["DefaultConnection"].ConnectionString;

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmentalAppointmentScheduling>()
                .HasOne(d => d.DayOfTheWeek)
                .WithMany(d => d.AppointmentSchedules)
                .HasForeignKey(d => d.DayOfTheWeek_Id);

            modelBuilder.Entity<DepartmentalAppointmentScheduling>()
                .HasOne(d => d.Examination)
                .WithMany(e => e.AppointmentSchedules)
                .HasForeignKey(d => d.Examination_Id);

 
            modelBuilder.Entity<TimeSlot>()
                .HasOne(t => t.Examination)
                .WithMany(e => e.TimeSlots)
                .HasForeignKey(t => t.Examination_Id);
        }
    }
}

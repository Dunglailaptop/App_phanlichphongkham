using Microsoft.EntityFrameworkCore;
using Phanlichphongkham.Model;
using System.Configuration;


namespace Phanlichphongkham.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<DepartmentalAppointmentScheduling> DepartmentalAppointmentSchedulings { get; set; }
      
        public DbSet<Examination> Examinations { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Zone> Zones { get; set; }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Sepicalty> Sepicaltys{ get; set; }
        public DbSet<DepartmentHospital> DepartmentHospitals { get; set; }

        public DbSet<SepcialtyJoinZone> SepcialtyJoinZones { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        
            var connectionString = ConfigurationManager
                .ConnectionStrings["DefaultConnection"].ConnectionString;

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmentalAppointmentScheduling>()
                .HasOne(d => d.Doctor)
                .WithMany(d => d.DepartmentalAppointmentScheduling)
                .HasForeignKey(d => d.Doctor_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DepartmentalAppointmentScheduling>()
                .HasOne(d => d.Examination)
                .WithMany(e => e.DepartmentalAppointmentScheduling)
                .HasForeignKey(d => d.Examination_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DepartmentalAppointmentScheduling>()
                .HasOne(d => d.Room)
                .WithMany(e => e.DepartmentalAppointmentScheduling)
                .HasForeignKey(d => d.Room_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DepartmentalAppointmentScheduling>()
                .HasOne(d => d.DepartmentHospital)
                .WithMany(e => e.DepartmentalAppointmentScheduling)
                .HasForeignKey(d => d.DepartmentHospital_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DepartmentalAppointmentScheduling>()
                .HasOne(d => d.Sepicalty)
                .WithMany(e => e.DepartmentalAppointmentScheduling)
                .HasForeignKey(d => d.Specialty_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>()
                .HasOne(t => t.Zone)
                .WithMany(e => e.Room)
                .HasForeignKey(t => t.Zone_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Doctor>()
                .HasOne(t => t.DepartmentHospital)
                .WithMany(e => e.Doctor)
                .HasForeignKey(t => t.DepartmentHospital_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServicePrice>()
                .HasOne(t => t.Zone)
                .WithMany(e => e.ServicePrice)
                .HasForeignKey(t => t.Zone_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SepcialtyJoinZone>()
                .HasOne(t => t.Zone)
                .WithMany(e => e.SepcialtyJoinZone)
                .HasForeignKey(t => t.Zone_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SepcialtyJoinZone>()
                .HasOne(t => t.Sepicalty)
                .WithMany(e => e.SepcialtyJoinZone)
                .HasForeignKey(t => t.Specialty_id)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}

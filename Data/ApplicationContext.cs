using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using medicalAppointmentsApplication.Models;

namespace medicalAppointmentsApplication.ContextApplication
{
    public  class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }
        public DbSet<Usuario> Users { get; set; }
        public DbSet<Paciente> Patients { get; set; }
        public DbSet<Medico> Medics { get; set; }
        public DbSet<Cita> Appointment { get; set; }
        public DbSet<Diagnostico> Diagnosis { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasKey(p => p.getKey());
            modelBuilder.Entity<Paciente>();
            modelBuilder.Entity<Medico>();
            modelBuilder.Entity<Cita>().HasKey(p => p.getAttribute11());
            ModelBuilder.Entity<Diagnostico>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }*/
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecord.Data
{
    public class MedicalRecordContext : DbContext
    {
        public MedicalRecordContext() 
            : this("DefaultConnection")
        { }

        public MedicalRecordContext(string connectionString)
            :base(connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MedicalRecordContext, Migrations.Configuration>("DefaultConnection"));
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Tooth> Teeth { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<MedicalProcedure> MedicalProcedures { get; set; }
        public DbSet<ToothState> ToothStates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

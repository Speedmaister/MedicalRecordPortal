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
        public static string ConnectionStringName
        {
            get
            {
                string connectionStringAliasKey = "SQLSERVER_CONNECTION_STRING_ALIAS";
                var connectionStringAlias = ConfigurationManager.AppSettings[connectionStringAliasKey];
                if (connectionStringAlias == null)
                {
                    connectionStringAlias = "DefaultConnection";
                }

                return connectionStringAlias;
            }
        }

        public MedicalRecordContext()
            : this(ConnectionStringName)
        {
        }

        public MedicalRecordContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MedicalRecordContext, Migrations.Configuration>(ConnectionStringName));
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Tooth> Teeth { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<MedicalProcedure> MedicalProcedures { get; set; }
        public DbSet<ToothState> ToothStates { get; set; }
        public DbSet<Log> Log { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

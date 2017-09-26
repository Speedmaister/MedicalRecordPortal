namespace MedicalRecord.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MedicalRecordContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MedicalRecordContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var statesTable = context.ToothStates;
            statesTable.AddOrUpdate(new ToothState() { Code = "C", Name = "Кариес" },
                new ToothState() { Code = "P", Name = "Пулпит" },
                new ToothState() { Code = "G", Name = "Периодонтит" },
                new ToothState() { Code = "R", Name = "Корен" },
                new ToothState() { Code = "O", Name = "Обтурация" },
                new ToothState() { Code = "O2", Name = "Обтурация от друг лекар" },
                new ToothState() { Code = "E", Name = "Липсващ" },
                new ToothState() { Code = "K", Name = "Корона" },
                new ToothState() { Code = "X", Name = "Изкуствен" },
                new ToothState() { Code = "Pa", Name = "Парадонтит" },
                new ToothState() { Code = "F", Name = "Фрактура" },
                new ToothState() { Code = "N", Name = "None" });
        }
    }
}

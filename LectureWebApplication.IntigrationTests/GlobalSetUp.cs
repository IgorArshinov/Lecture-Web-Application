using LectureWebApplication.Core.Models;
using LectureWebApplication.Persistence;
using NUnit.Framework;
using System.Data.Entity.Migrations;
using System.Linq;

namespace LectureWebApplication.IntigrationTests
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            MigrateDbToLatestVersion();

            Seed();
        }

        private static void MigrateDbToLatestVersion()
        {
            var configuration = new LectureWebApplication.Persistence.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();

            if (context.Users.Any() && context.Departments.Any())
            {
                return;
            }

            if (!context.Users.Any())
            {
                context.Users.Add(new ApplicationUser { UserName = "testRobot", Name = "testRobot", Email = "x", PasswordHash = "x" });
                context.Users.Add(new ApplicationUser { UserName = "testRobot2", Name = "testRobot2", Email = "x", PasswordHash = "x" });
            }

            if (!context.Departments.Any())
            {
                context.Departments.Add(new Department { Name = "Science" });
                context.Departments.Add(new Department { Name = "Art" });
            }

            context.SaveChanges();
        }
    }
}
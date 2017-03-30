namespace LexiconLMS.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LexiconLMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LexiconLMS.Models.ApplicationDbContext context)
        {

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var courses = new[]
            {
                new Course {Name="NET",  Description="Expert påbyggnadsutbildning",
                    StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00") },

                new Course {Name="Java", Description="IT påbyggnadsutbildning",
                    StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00")},

                new Course {Name="Nätverk Teknik", Description="fyra månaders kurs till nätverk tekniker",
                    StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00")},

            };

            context.Courses.AddOrUpdate(courses);
            context.SaveChanges();

            var Users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                  UserName = "teacher1@lexicon.se",
                  Email ="teacher1@lexicon.se",
                  FirstName ="Kalle1",
                  LastName ="Hans1" },

                 new ApplicationUser
                {
                  UserName = "teacher2@lexicon.se",
                  Email ="teacher2@lexicon.se",
                  FirstName ="Kalle2",
                  LastName ="Hans2" },

                  new ApplicationUser
                {
                  UserName = "teacher3@lexicon.se",
                  Email ="teacher3@lexicon.se",
                  FirstName ="Kalle3",
                  LastName ="Hans3"},
                    new ApplicationUser
                {
                  UserName = "teacher4@lexicon.se",
                  Email ="teacher4@lexicon.se",
                  FirstName ="Kalle4",
                  LastName ="Hans4" },

                      new ApplicationUser
                {
                  UserName = "teacher5@lexicon.se",
                  Email ="teacher5@lexicon.se",
                  FirstName ="Kalle5",
                  LastName ="Hans5"},

                new ApplicationUser
                {
                  UserName = "student1@lexicon.se",
                  Email ="student1@lexicon.se",
                  FirstName ="Jan1",
                  LastName ="Adam1",
                 CourseId = context.Courses.FirstOrDefault(c => c.Name == "Java").CourseID },

                  new ApplicationUser
                {
                  UserName = "student2@lexicon.se",
                  Email ="student2@lexicon.se",
                  FirstName ="Jan2",
                  LastName ="Adam2",
                 CourseId = context.Courses.FirstOrDefault(c => c.Name == "Java").CourseID },

                    new ApplicationUser
                {
                  UserName = "student3@lexicon.se",
                  Email ="student3@lexicon.se",
                  FirstName ="Jan3",
                  LastName ="Adam3",
                 CourseId = context.Courses.FirstOrDefault(c => c.Name == "Java").CourseID },

                      new ApplicationUser
                {
                  UserName = "student4@lexicon.se",
                  Email ="student4@lexicon.se",
                  FirstName ="Jan4",
                  LastName ="Adam4",
                 CourseId = context.Courses.FirstOrDefault(c => c.Name == "Java").CourseID },
                        new ApplicationUser
                {
                  UserName = "student5@lexicon.se",
                  Email ="student5@lexicon.se",
                  FirstName ="Jan5",
                  LastName ="Adam5",
                 CourseId = context.Courses.FirstOrDefault(c => c.Name == "Java").CourseID },
            };
            context.SaveChanges();

            var modules = new[]
            {
                new Module { Name="C#", Description="Grund läggande", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), CourseId=1 },
                new Module { Name="Netbeans", Description="AGrund läggande", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), CourseId=2 },
                new Module { Name="Angular", Description="BGrund läggande", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), CourseId=1 }
            };
            context.Modules.AddOrUpdate(modules);
            context.SaveChanges();

            var ActivityType = new[]
           {
                new ActivityType { TypeName="Föreläsning"},
                new ActivityType { TypeName="E-learning" },
                new ActivityType { TypeName="Övning"},
                new ActivityType { TypeName="Övrigt"}
            };
            context.ActivityTypes.AddOrUpdate(ActivityType);
            context.SaveChanges();
            var activity = new[]
            {
                new Activity { Name="OOP", Description="grundläggande", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), ModuleId=1,ActivityTypeID=1},
                new Activity { Name="C# Core", Description="grundläggande", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), ModuleId=2,ActivityTypeID=2},
                new Activity { Name="HTML", Description="Garage first", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), ModuleId=1 ,ActivityTypeID=2},
                new Activity { Name="CSS", Description="Garage first", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), ModuleId=1 ,ActivityTypeID=1},
                new Activity { Name="Garage 1.0", Description="Garage first", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), ModuleId=1 ,ActivityTypeID=1 }
            };
            context.Activities.AddOrUpdate(activity);
            context.SaveChanges();


            // Get Users Teacher or Student
            foreach (var user in Users)
            {
                var result = userManager.Create(user, "foobar");
            }

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            // 
            var roleNames = new[] { "Teacher", "Student" };

            foreach (var roleName in roleNames)
            {
                if (!context.Roles.Any(r => r.Name == roleName))
                {
                    var role = new IdentityRole { Name = roleName };
                    var result = roleManager.Create(role);
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                }
            }


            // Add roles
            var teachers = new[] { "teacher1@lexicon.se", "teacher2@lexicon.se", "teacher3@lexicon.se", "teacher4@lexicon.se", "teacher5@lexicon.se" };
            foreach (var item in teachers)
            {
                var teacherUser = userManager.FindByName(item);
                userManager.AddToRole(teacherUser.Id, "Teacher");
            }

            var students = new[] { "student1@lexicon.se", "student2@lexicon.se", "student3@lexicon.se", "student4@lexicon.se", "student5@lexicon.se" };
            foreach (var item in students)
            {
                var studentUser = userManager.FindByName(item);
                userManager.AddToRole(studentUser.Id, "Student");
            }

        }
    }
}

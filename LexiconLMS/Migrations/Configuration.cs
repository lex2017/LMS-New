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
                new Course {Name=".NET",  Description="Expert p�byggnadsutbildning",
                    StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00") },

                new Course {Name="Java", Description="IT p�byggnadsutbildning",
                    StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00")},

                new Course {Name="N�tverk Teknik", Description="fyra m�naders kurs till n�tverk tekniker",
                    StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00")},

                new Course {Name=".NET NF16",  Description="IT p�byggnadsutbildning",
                    StartDate = DateTime.Parse("2016-12-19 09:00:00"), EndDate=DateTime.Parse("2017-04-11 09:00:00") },
            };

            context.Courses.AddOrUpdate(courses);
            context.SaveChanges();

            var Users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                  UserName = "teacher1@lexicon.se",
                  Email ="teacher1@lexicon.se",
                  FirstName ="Karl",
                  LastName ="Hanssson",
                  CourseId = context.Courses.FirstOrDefault(c => c.Name == "Java").CourseID},

                new ApplicationUser
                {
                  UserName = "teacher2@lexicon.se",
                  Email ="teacher2@lexicon.se",
                  FirstName ="Per",
                  LastName ="Olsson" ,
                  CourseId = context.Courses.FirstOrDefault(c => c.Name == "Java").CourseID},

                new ApplicationUser
                {
                  UserName = "teacher3@lexicon.se",
                  Email ="teacher3@lexicon.se",
                  FirstName ="Nils",
                  LastName ="Persson",
                  CourseId = context.Courses.FirstOrDefault(c => c.Name == "Java").CourseID},

                new ApplicationUser
                {
                  UserName = "teacher4@lexicon.se",
                  Email ="teacher4@lexicon.se",
                  FirstName ="Eva",
                  LastName ="Larsson",
                  CourseId = context.Courses.FirstOrDefault(c => c.Name == "Java").CourseID},

                new ApplicationUser
                {
                  UserName = "teacher5@lexicon.se",
                  Email ="teacher5@lexicon.se",
                  FirstName ="Mattias",
                  LastName ="Jansson",
                  CourseId = context.Courses.FirstOrDefault(c => c.Name == "Java").CourseID},

                new ApplicationUser
                {
                  UserName = "student1@lexicon.se",
                  Email ="student1@lexicon.se",
                  FirstName ="Kurt",
                  LastName ="Olsson",
                  CourseId = context.Courses.FirstOrDefault(c => c.Name == "Java").CourseID },

                new ApplicationUser
                {
                  UserName = "student2@lexicon.se",
                  Email ="student2@lexicon.se",
                  FirstName ="Martin",
                  LastName ="Eriksson",
                  CourseId = context.Courses.FirstOrDefault(c => c.Name == "Java").CourseID },

                new ApplicationUser
                {
                  UserName = "student3@lexicon.se",
                  Email ="student3@lexicon.se",
                  FirstName ="Sven",
                  LastName ="Svensson",
                  CourseId = context.Courses.FirstOrDefault(c => c.Name == "Java").CourseID },

                new ApplicationUser
                {
                  UserName = "student4@lexicon.se",
                  Email ="student4@lexicon.se",
                  FirstName ="P�l",
                  LastName ="Karlsson",
                  CourseId = context.Courses.FirstOrDefault(c => c.Name == "Java").CourseID },

                new ApplicationUser
                {
                  UserName = "student5@lexicon.se",
                  Email ="student5@lexicon.se",
                  FirstName ="Jan",
                  LastName ="Johansson",
                  CourseId = context.Courses.FirstOrDefault(c => c.Name == "Java").CourseID },

                new ApplicationUser
                {
                  UserName = "student6@lexicon.se",
                  Email ="student6@lexicon.se",
                  FirstName ="Karl",
                  LastName ="Karlsson",
                  CourseId = context.Courses.FirstOrDefault(c => c.Name == ".NET NF16").CourseID },

                new ApplicationUser
                {
                  UserName = "student7@lexicon.se",
                  Email ="student7@lexicon.se",
                  FirstName ="Kurt",
                  LastName ="Olsson",
                  CourseId = context.Courses.FirstOrDefault(c => c.Name == ".NET NF16").CourseID },
            };
            context.SaveChanges();

            var modules = new[]
            {
                new Module { Name="C#", Description="Grundl�ggande", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), CourseId=1 },
                new Module { Name="Netbeans", Description="AGrundl�ggande", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), CourseId=2 },
                new Module { Name="Angular", Description="BGrundl�ggande", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), CourseId=1 },

                new Module { Name="C#", Description="Introduktion", StartDate = DateTime.Parse("2016-12-19 09:00:00"), EndDate=DateTime.Parse("2017-01-17 09:00:00"), CourseId=4 },
                new Module { Name="Webb", Description="Introduktion", StartDate = DateTime.Parse("2017-01-18 09:00:00"), EndDate=DateTime.Parse("2017-01-31 09:00:00"), CourseId=4 },
                new Module { Name="MVC", Description="Introduktion", StartDate = DateTime.Parse("2017-02-01 09:00:00"), EndDate=DateTime.Parse("2017-02-14 09:00:00"), CourseId=4 },
                new Module { Name="Databas", Description="Introduktion", StartDate = DateTime.Parse("2017-02-15 09:00:00"), EndDate=DateTime.Parse("2017-02-21 09:00:00"), CourseId=4 },
                new Module { Name="Testning", Description="Introduktion", StartDate = DateTime.Parse("2017-02-22 09:00:00"), EndDate=DateTime.Parse("2017-03-01 09:00:00"), CourseId=4 },
                new Module { Name="App.utv", Description="Introduktion", StartDate = DateTime.Parse("2017-03-02 09:00:00"), EndDate=DateTime.Parse("2017-03-08 09:00:00"), CourseId=4 },
                new Module { Name="MVC f�rdj", Description="Introduktion", StartDate = DateTime.Parse("2017-03-09 09:00:00"), EndDate=DateTime.Parse("2017-04-11 09:00:00"), CourseId=4 },
            };
            context.Modules.AddOrUpdate(modules);
            context.SaveChanges();

            var ActivityType = new[]
           {
                new ActivityType { TypeName="F�rel�sning"},
                new ActivityType { TypeName="E-learning" },
                new ActivityType { TypeName="�vning"},
                new ActivityType { TypeName="�vrigt"}
            };
            context.ActivityTypes.AddOrUpdate(ActivityType);
            context.SaveChanges();
            var activity = new[]
            {
                new Activity { Name="OOP", Description="grundl�ggande", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), ModuleId=1,ActivityTypeID=1},
                new Activity { Name="C# Core", Description="grundl�ggande", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), ModuleId=2,ActivityTypeID=2},
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

            var students = new[] { "student1@lexicon.se", "student2@lexicon.se", "student3@lexicon.se", "student4@lexicon.se", "student5@lexicon.se", "student6@lexicon.se", "student7@lexicon.se" };
            foreach (var item in students)
            {
                var studentUser = userManager.FindByName(item);
                userManager.AddToRole(studentUser.Id, "Student");
            }

        }
    }
}

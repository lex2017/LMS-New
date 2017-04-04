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
                new Course {Name=".NET",  Description="Expert påbyggnadsutbildning",
                    StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00") },

                new Course {Name="Java", Description="IT påbyggnadsutbildning",
                    StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00")},

                new Course {Name="Nätverk Teknik", Description="fyra månaders kurs till nätverk tekniker",
                    StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00")},

                new Course {Name=".NET NF16",  Description="IT påbyggnadsutbildning",
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

                //TEST
                new ApplicationUser
                {
                  UserName = "teacher6@lexicon.se",
                  Email ="teacher6@lexicon.se",
                  FirstName ="Harald",
                  LastName ="Nilsson",
                  CourseId = context.Courses.FirstOrDefault(c => c.Name == ".NET NF16").CourseID},

                //TEST
                new ApplicationUser
                {
                  UserName = "teacher7@lexicon.se",
                  Email ="teacher7@lexicon.se",
                  FirstName ="Pia",
                  LastName ="Jansson",
                  CourseId = context.Courses.FirstOrDefault(c => c.Name == ".NET NF16").CourseID},

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
                  FirstName ="Pål",
                  LastName ="Karlsson",
                  CourseId = context.Courses.FirstOrDefault(c => c.Name == "Java").CourseID },

                new ApplicationUser
                {
                  UserName = "student5@lexicon.se",
                  Email ="student5@lexicon.se",
                  FirstName ="Jan",
                  LastName ="Johansson",
                  CourseId = context.Courses.FirstOrDefault(c => c.Name == "Java").CourseID },
                //TEST
                new ApplicationUser
                {
                  UserName = "student6@lexicon.se",
                  Email ="student6@lexicon.se",
                  FirstName ="Karl",
                  LastName ="Karlsson",
                  CourseId = context.Courses.FirstOrDefault(c => c.Name == ".NET NF16").CourseID },
                //TEST
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
                new Module { Name="C#", Description="Grundläggande", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), CourseId=1 },
                new Module { Name="Netbeans", Description="AGrundläggande", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), CourseId=2 },
                new Module { Name="Angular", Description="BGrundläggande", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), CourseId=1 },

                //TEST
                new Module { Name="C#", Description="Introduktion", StartDate = DateTime.Parse("2016-12-19 09:00:00"), EndDate=DateTime.Parse("2017-01-17 09:00:00"), CourseId=4 },
                new Module { Name="Webb", Description="Introduktion", StartDate = DateTime.Parse("2017-01-18 09:00:00"), EndDate=DateTime.Parse("2017-01-31 09:00:00"), CourseId=4 },
                new Module { Name="MVC", Description="Introduktion", StartDate = DateTime.Parse("2017-02-01 09:00:00"), EndDate=DateTime.Parse("2017-02-14 09:00:00"), CourseId=4 },
                new Module { Name="Databas", Description="Introduktion", StartDate = DateTime.Parse("2017-02-15 09:00:00"), EndDate=DateTime.Parse("2017-02-21 09:00:00"), CourseId=4 },
                new Module { Name="Testning", Description="Introduktion", StartDate = DateTime.Parse("2017-02-22 09:00:00"), EndDate=DateTime.Parse("2017-03-01 09:00:00"), CourseId=4 },
                new Module { Name="App.utv", Description="Introduktion", StartDate = DateTime.Parse("2017-03-02 09:00:00"), EndDate=DateTime.Parse("2017-03-08 09:00:00"), CourseId=4 },
                new Module { Name="MVC fördj", Description="Introduktion", StartDate = DateTime.Parse("2017-03-09 09:00:00"), EndDate=DateTime.Parse("2017-04-11 09:00:00"), CourseId=4 },
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

            // var activity = new[]
            // {
            //     new Activity { Name="OOP", Description="grundläggande", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), ModuleId=1,ActivityTypeID=1},
            //     new Activity { Name="C# Core", Description="grundläggande", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), ModuleId=2,ActivityTypeID=2},
            //     new Activity { Name="HTML", Description="Garage first", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), ModuleId=1 ,ActivityTypeID=2},
            //     new Activity { Name="CSS", Description="Garage first", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), ModuleId=1 ,ActivityTypeID=1},
            //     new Activity { Name="Garage 1.0", Description="Garage first", StartDate = DateTime.Parse("2017-04-01 09:00:00"), EndDate=DateTime.Parse("2017-08-31 09:00:00"), ModuleId=1 ,ActivityTypeID=1 }
            // };
            // context.Activities.AddOrUpdate(activity);
            //TEST
            var activity = new[]
            {
                //Modul C# (ModuleId=4)
                new Activity {Name="C# Intro", Description="Extern föreläsare", StartDate = DateTime.Parse("2016-12-20 09:00:00"), EndDate=DateTime.Parse("2016-12-20 13:00:00"), ModuleId=4, ActivityTypeID=1},//Föreläsning
                new Activity {Name="1.3", Description="C# Fundamental with Visual Studio 2015", StartDate = DateTime.Parse("2016-12-21 09:00:00"), EndDate=DateTime.Parse("2016-12-21 12:00:00"), ModuleId=4, ActivityTypeID=2},//E-learning
                new Activity {Name="1.4 + 1.5", Description="C# Fundamental with Visual Studio 2015", StartDate = DateTime.Parse("2016-12-21 13:00:00"), EndDate=DateTime.Parse("2016-12-21 17:00:00"), ModuleId=4, ActivityTypeID=2},//E-learning
                new Activity {Name="C# Grund", Description="Självstudier", StartDate = DateTime.Parse("2016-12-22 09:00:00"), EndDate=DateTime.Parse("2016-12-22 17:00:00"), ModuleId=4, ActivityTypeID=1},//Föreläsning
                new Activity {Name="2", Description="C# övning", StartDate = DateTime.Parse("2016-12-23 09:00:00"), EndDate=DateTime.Parse("2016-12-23 17:00:00"), ModuleId=4, ActivityTypeID=3},//Övning
                new Activity {Name="1.6 + 1.7", Description="C# Fundamentals with Visual Studio", StartDate = DateTime.Parse("2016-12-27 09:00:00"), EndDate=DateTime.Parse("2016-12-27 17:00:00"), ModuleId=4, ActivityTypeID=2},//E-learning
                //Modul Webb (ModuleId=5)
                new Activity {Name="HTML, CSS", Description="Extern föreläsare", StartDate = DateTime.Parse("2017-01-18 09:00:00"), EndDate=DateTime.Parse("2017-01-18 13:00:00"), ModuleId=5, ActivityTypeID=1},
                new Activity {Name="3 + 4.1-4.3", Description="Självstudier", StartDate = DateTime.Parse("2017-01-19 09:00:00"), EndDate=DateTime.Parse("2017-01-19 13:00:00"), ModuleId=5, ActivityTypeID=2},
                //Modul MVC (ModuleId=6)
                new Activity {Name="ASP.NET MVC", Description="Extern föreläsare", StartDate = DateTime.Parse("2017-02-01 09:00:00"), EndDate=DateTime.Parse("2017-02-01 13:00:00"), ModuleId=6, ActivityTypeID=1},
                new Activity {Name="7.1 - 7.3", Description="Självstudier", StartDate = DateTime.Parse("2017-02-02 09:00:00"), EndDate=DateTime.Parse("2017-02-02 13:00:00"), ModuleId=6, ActivityTypeID=2},
                //Modul Databas (ModuleId=7)
                new Activity {Name="Datamodellering", Description="Extern föreläsare", StartDate = DateTime.Parse("2017-02-15 09:00:00"), EndDate=DateTime.Parse("2017-02-15 13:00:00"), ModuleId=7, ActivityTypeID=1},
                new Activity {Name="Övning13", Description="Datamodellering", StartDate = DateTime.Parse("2017-02-16 09:00:00"), EndDate=DateTime.Parse("2017-02-16 13:00:00"), ModuleId=7, ActivityTypeID=3},
                //Modul Testning (ModuleId=8)
                new Activity {Name="ISTQB", Description="Extern föreläsare", StartDate = DateTime.Parse("2017-02-22 09:00:00"), EndDate=DateTime.Parse("2017-02-22 13:00:00"), ModuleId=8, ActivityTypeID=1},
                new Activity {Name="ISTQB", Description="Extern föreläsare", StartDate = DateTime.Parse("2017-02-23 09:00:00"), EndDate=DateTime.Parse("2017-02-23 13:00:00"), ModuleId=8, ActivityTypeID=1},
                //Modul App.Utv (ModuleId=9)
                new Activity {Name="11", Description="AngularJS", StartDate = DateTime.Parse("2017-03-02 09:00:00"), EndDate=DateTime.Parse("2017-03-02 13:00:00"), ModuleId=9, ActivityTypeID=2},
                new Activity {Name="11", Description="AngularJS", StartDate = DateTime.Parse("2017-03-03 09:00:00"), EndDate=DateTime.Parse("2017-03-03 13:00:00"), ModuleId=9, ActivityTypeID=2},
                //Modul MVC fördj (ModuleId=10)
                new Activity {Name="12", Description="MVC", StartDate = DateTime.Parse("2017-03-09 09:00:00"), EndDate=DateTime.Parse("2017-03-09 13:00:00"), ModuleId=10, ActivityTypeID=2},
                new Activity {Name="12", Description="MVC", StartDate = DateTime.Parse("2017-03-09 09:00:00"), EndDate=DateTime.Parse("2017-03-09 13:00:00"), ModuleId=10, ActivityTypeID=2},
            };
            context.Activities.AddOrUpdate(activity);
            context.SaveChanges();

            //var documents = new[]
            //{
            //    //new Document {FileName="", FilePath="", Description="", TimeStamp=DateTime.Parse("2017-03-09 09:00:00"), DeadlineDate=DateTime.Parse("2017-04-09 09:00:00"), UserId =  }
            //};
            //context.Documents.AddOrUpdate(documents);           
            //context.SaveChanges();

        

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
            var teachers = new[] { "teacher1@lexicon.se", "teacher2@lexicon.se", "teacher3@lexicon.se", "teacher4@lexicon.se", "teacher5@lexicon.se", "teacher6@lexicon.se", "teacher7@lexicon.se" };
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

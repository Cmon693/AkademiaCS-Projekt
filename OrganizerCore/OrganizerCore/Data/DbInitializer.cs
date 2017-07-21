using System;
using System.Collections.Generic;
using System.Linq;
using Organizer.Models;

namespace Organizer.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TaskContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Tasks.Any())
            {
                return;   // DB has been seeded
            }

            var tasks = new Task[]
            {
                new Task{Name="posprzątać", Group="dom", Priority="5", Date=DateTime.Parse("2017-05-20")},
                new Task{Name="zakupy", Group="dom", Priority = "2", Date=DateTime.Parse("2017-05-18")},
                new Task{Name="lekcje", Group="szkola", Priority = "8", Date=DateTime.Parse("2017-05-15")},
            };

            foreach (Task t in tasks)
            {
                context.Tasks.Add(t);
            }
            context.SaveChanges();



            var subtasks = new Subtask[]
            {
                new Subtask{TaskID=2, Name="ser", Group="dom", Priority="5", Date=DateTime.Parse("2017-05-18")},
                new Subtask{TaskID=2, Name="szynka", Group="dom", Priority = "2", Date=DateTime.Parse("2017-05-18")},
                new Subtask{TaskID=3, Name="matma", Group="szkola", Priority = "8", Date=DateTime.Parse("2017-05-15")},
                new Subtask{TaskID=3, Name="polski", Group="szkola", Priority = "8", Date=DateTime.Parse("2017-05-15")}
            };

            foreach (Subtask s in subtasks)
            {
                context.Subtasks.Add(s);
            }
            context.SaveChanges();

        }
    }
}

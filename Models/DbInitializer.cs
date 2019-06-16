using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace laba2.Models
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationContext db)
        {
            if (db.Classes.Any())
            {
                return;   // База данных инициализирована
            }

            string[] markings = { "1","2","3","4" };

            for (int i = 0; i < markings.Length; i++)
            {
                db.ClassTypes.Add(new ClassType { Name = markings[i], Description = Char.ConvertFromUtf32(Char.ConvertToUtf32("F", 0) + i) });
            }

            db.SaveChanges();

            for (int i = 0; i < markings.Length; i++)
            {
                db.Classes.Add(new Class { ClassLead = Char.ConvertFromUtf32(Char.ConvertToUtf32("А", 0) + i),Count = i + 1, Date = DateTime.Now, ClassTypeId = i + 1 });
            }

            db.SaveChanges();

            for (int i = 0; i < markings.Length; i++)
            {
                db.Subjects.Add(new Subject { Name = Char.ConvertFromUtf32(Char.ConvertToUtf32("А", 0) + i), Description = Char.ConvertFromUtf32(Char.ConvertToUtf32("",0) + i) });
            }

            db.SaveChanges();

            for (int i = 0; i < markings.Length; i++)
            {
                db.Schedules.Add(
                    new Schedule { Date = DateTime.Now, ClassId = i + 1, SubjectId = i + 1 }
                    );
            }

            db.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using laba2.Models;

namespace laba2.Services
{
    public class DbService
    {
        private ApplicationContext _db;

        public DbService(ApplicationContext context)
        {
            _db = context;
        }

        public List<PriceClass> GetPriceFuels()
        {
            var priceFuels = from ClassType in _db.ClassTypes
                             join Class in _db.Classes
                             on ClassType.Id equals Class.ClassTypeId
                             select new { ClassType = ClassType.Name, Cla = Class.ClassLead, Class = Class.Count, date = Class.Date };
            List<PriceClass> list = new List<PriceClass>();
            foreach (var pf in priceFuels) list.Add(new PriceClass(pf.ClassType, pf.Cla , pf.Class, pf.date ));

            return list;
        }

        public void AddPriceFuels(PriceClass priceFuels)
        {
            //ClassType classType = new ClassType { Name = Char.ConvertFromUtf32(Char.ConvertToUtf32("о", 0)) , Description = "Стандартный" };
            //_db.ClassTypes.Add(classType);
            //_db.SaveChanges();

            Class newClass = new Class { ClassLead = priceFuels.ClassLead, Count = priceFuels.Count , Date = DateTime.Now, ClassTypeId = priceFuels.ClassTypeId };
            _db.Classes.Add(newClass);
            _db.SaveChanges();
        }
    }
}

namespace DentalStudio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using DentalStudio.Data.Models.Enumerations;

    internal class ProceduresSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            if (dbContext.Procedures.Any())
            {
                return;
            }

            await dbContext.Procedures.AddAsync(new Models.Procedure { Name = "Обстоен профилактичен преглед със снемане на зъбен статус и изготвяне на амбулаторен лист", Code = Code.HundredOne, Price = 10 });
            await dbContext.Procedures.AddAsync(new Models.Procedure { Name = "Обтурация с амалгама или химичен композит", Code = Code.ThreeHundredOne, Price = 35 });
            await dbContext.Procedures.AddAsync(new Models.Procedure { Name = "Екстракция на временен зъб с анестезия", Code = Code.FiveHundredEight, Price = 13 });
            await dbContext.Procedures.AddAsync(new Models.Procedure { Name = "Екстракция на постоянен зъб с анестезия", Code = Code.FiveHundredNine, Price = 35 });
            await dbContext.Procedures.AddAsync(new Models.Procedure { Name = "Лечение на пулпит или периодонтит на временен зъб", Code = Code.ThreeHundredThirtyTwo, Price = 25 });
            await dbContext.Procedures.AddAsync(new Models.Procedure { Name = "Лечение на пулпит или периодонтит на постоянен зъб", Code = Code.ThreeHundredThirtyThree, Price = 80 });
        }
    }
}

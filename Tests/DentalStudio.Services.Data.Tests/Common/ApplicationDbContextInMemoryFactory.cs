namespace DentalStudio.Services.Data.Tests.Common
{
    using System;

    using DentalStudio.Data;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContextInMemoryFactory
    {
        public static ApplicationDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            return new ApplicationDbContext(options);
        }
    }
}

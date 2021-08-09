namespace DentalStudio.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using DentalStudio.Data.Common.Repositories;
    using DentalStudio.Data.Models;
    using DentalStudio.Services.Mapping;

    public class SettingsService : ISettingsService
    {
        private readonly IDeletableEntityRepository<Setting> settingsRepository;

        public SettingsService(IDeletableEntityRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public int GetCount()
        {
            return this.settingsRepository.All().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.settingsRepository.All().To<T>().ToList();
        }
    }
}

namespace DentalStudio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DentalStudio.Data.Common.Repositories;
    using DentalStudio.Data.Models;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using Microsoft.EntityFrameworkCore;

    public class ProceduresService : IProceduresService
    {
        private readonly IDeletableEntityRepository<Procedure> proceduresRepository;

        public ProceduresService(IDeletableEntityRepository<Procedure> proceduresRepository)
        {
            this.proceduresRepository = proceduresRepository;
        }

        public async Task<bool> Create(ProcedureServiceModel procedureServiceModel)
        {
            Procedure procedure = new Procedure
            {
                Name = procedureServiceModel.Name,
                Code = procedureServiceModel.Code,
                Price = procedureServiceModel.Price,
            };

            await this.proceduresRepository.AddAsync(procedure);
            var result = await this.proceduresRepository.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> Delete(ProcedureServiceModel procedureServiceModel)
        {
            Procedure procedure = await this.proceduresRepository.All().FirstOrDefaultAsync(a => a.Id == procedureServiceModel.Id);

            if (procedure == null)
            {
                throw new ArgumentNullException(nameof(procedure));
            }

            this.proceduresRepository.Delete(procedure);

            var result = await this.proceduresRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Edit(int id, ProcedureServiceModel procedureServiceModel)
        {
            Procedure procedure = this.proceduresRepository.All().FirstOrDefault(p => p.Id == id);

            if (procedure == null)
            {
                throw new ArgumentNullException(nameof(procedure));
            }

            procedure.Name = procedureServiceModel.Name;
            procedure.Code = procedureServiceModel.Code;
            procedure.Price = procedureServiceModel.Price;

            this.proceduresRepository.Update(procedure);
            int result = await this.proceduresRepository.SaveChangesAsync();

            return result > 0;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<ProcedureServiceModel> query = this.proceduresRepository.All().To<ProcedureServiceModel>();

            return query.To<T>().ToList();
        }

        public async Task<T> GetByName<T>(string name)
        {
            var procedure = await this.proceduresRepository
             .All()
             .Where(d => d.Name == name).To<ProcedureServiceModel>()
             .FirstOrDefaultAsync();

            if (procedure == null)
            {
                throw new ArgumentNullException(
                    string.Format(nameof(procedure)));
            }

            var procedureServiceModel = AutoMapperConfig.MapperInstance.Map<T>(procedure);

            return procedureServiceModel;
        }

        public async Task<T> GetById<T>(int id)
        {
            var procedure = await this.proceduresRepository
              .All()
              .Where(a => a.Id == id).To<ProcedureServiceModel>()
              .FirstOrDefaultAsync();

            var procedureServiceModel = AutoMapperConfig.MapperInstance.Map<T>(procedure);

            return procedureServiceModel;
        }

        public int GetCount()
        {
            return this.proceduresRepository.All().Count();
        }
    }
}

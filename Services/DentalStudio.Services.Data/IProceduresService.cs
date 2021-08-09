namespace DentalStudio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DentalStudio.Services.Models;

    public interface IProceduresService
    {
        Task<bool> Create(ProcedureServiceModel procedure);

        IEnumerable<T> GetAll<T>();

        Task<T> GetByName<T>(string name);

        int GetCount();

        Task<T> GetById<T>(int id);

        Task<bool> Delete(ProcedureServiceModel procedure);

        Task<bool> Edit(int id, ProcedureServiceModel procedure);
    }
}

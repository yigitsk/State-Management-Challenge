using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Business.Interfaces
{
    public interface ICrudService<T> where T : class
    {
        Task<Guid> Create(T model);
        Task<Guid> Update(T model);
        Task<bool> Delete(Guid id);
        Task<T> Get(Guid id);
        Task<IEnumerable<T>> GetAll();
    }
}

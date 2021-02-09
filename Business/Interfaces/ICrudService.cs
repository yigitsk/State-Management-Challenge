using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Business.Interfaces
{
    public interface ICrudService<T>
    {
        public Task<Guid> Create(T model);
        public Task<Guid> Update(T model);
        public Task<bool> Delete(T model);
        public Task<T> Get(Guid id);
    }
}

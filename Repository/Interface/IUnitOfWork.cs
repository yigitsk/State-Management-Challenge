using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Repository.Interface
{
    public interface IUnitOfWork
    {
        ITaskRepository Tasks { get; }
        IFlowRepository Flows { get; }
        IStateRepository States { get; }
        ITaskHistoryRepository TaskHistories { get; }


    }
}

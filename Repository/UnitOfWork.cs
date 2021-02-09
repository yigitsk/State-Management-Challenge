using ProceedCase.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ITaskRepository Tasks { get; }

        public IFlowRepository Flows { get; }

        public IStateRepository States { get; }
        public ITaskHistoryRepository TaskHistories { get; }

        public UnitOfWork(ITaskRepository taskRepository, IFlowRepository flowRepository, IStateRepository stateRepository, ITaskHistoryRepository taskHistoryRepository)
        {
            Tasks = taskRepository;
            Flows = flowRepository;
            States = stateRepository;
            TaskHistories = taskHistoryRepository;
        }

    }
}

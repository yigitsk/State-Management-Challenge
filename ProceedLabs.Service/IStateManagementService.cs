using ProceedCase.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProceedLabs.Service
{
    public interface IStateManagementService
    {
        Task<bool> AssignFlowToTask(Guid taskId, Guid flowId);
        Task<TaskAggregate> ChangeTaskState(Guid taskId, int order);
        Task<TaskAggregate> GetTaskAggregate(Guid taskId);
        Task<bool> TaskNextState(Guid taskId);
        Task<bool> TaskPrevState(Guid taskId);
    }
}

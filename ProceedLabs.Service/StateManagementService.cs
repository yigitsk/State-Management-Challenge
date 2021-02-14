using Microsoft.Extensions.Configuration;
using MoreLinq;
using ProceedCase.Entities;
using ProceedCase.Models.BusinessModels;
using ProceedCase.Repository.Interface;
using ProceedLabs.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceedLabs.Service
{
    public class StateManagementService: IStateManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;


        public StateManagementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AssignFlowToTask(Guid taskId , Guid flowId)
        {

            var task = await _unitOfWork.Tasks.Get(taskId);
            if (task.ActiveStateId != null && task.FlowId != null)
                return false;
            var flow = await _unitOfWork.Flows.Get(flowId);
            if (!flow.FlowStates.Any())
                return false;
            var firstState = flow.FlowStates.Find(x => x.Order == 1);
            task.FlowId = flow.Id;
            task.ActiveStateId = firstState.Id;
           

            var historyEntity = new TaskHistoryEntity();
            historyEntity.Order = 1;
            historyEntity.TaskId = taskId;
            historyEntity.FlowId = flowId;
            historyEntity.StateId = firstState.Id;
            
            var result = await _unitOfWork.Tasks.Update(task);
            if (result == 0)
                return false;
            var result2 = await _unitOfWork.TaskHistories.Add(historyEntity);
            if (result2 == 0)
                return false;
            _unitOfWork.Commit();

            return true;
        }

        public async Task<bool> TaskNextState(Guid taskId)
        {

            var taskAggregate = await PrepareTaskAggregate(taskId);
            taskAggregate.Next();
            var taskHistory = new TaskHistoryEntity();
            taskHistory.Order = taskAggregate.ActiveState.Order;
            taskHistory.StateId = taskAggregate.ActiveState.StateId;
            taskHistory.TaskId = taskAggregate.Id;
            taskHistory.FlowId = taskAggregate.Flow.Id;
            await _unitOfWork.TaskHistories.Add(taskHistory);
            _unitOfWork.Commit();
            return true;
        }

        public async Task<bool> TaskPrevState(Guid taskId)
        {

            var taskAggregate = await PrepareTaskAggregate(taskId);
            taskAggregate.Previous();
            var taskHistory = new TaskHistoryEntity();
            taskHistory.Order = taskAggregate.ActiveState.Order;
            taskHistory.StateId = taskAggregate.ActiveState.StateId;
            taskHistory.TaskId = taskAggregate.Id;
            taskHistory.FlowId = taskAggregate.Flow.Id;
            await _unitOfWork.TaskHistories.Add(taskHistory);
            _unitOfWork.Commit();
            return true;
        }

        public async Task<TaskAggregate> GetTaskAggregate(Guid taskId)
        {

            var taskAggregate = await PrepareTaskAggregate(taskId);
            return taskAggregate;
        }

        public async Task<TaskAggregate> ChangeTaskState(Guid taskId,int order)
        {

            var taskAggregate = await PrepareTaskAggregate(taskId);
            var newState = taskAggregate.TaskHistory.Where(x => x.Order == order).FirstOrDefault();
            if (newState == null)
                return null;
            taskAggregate.ActiveState = newState;
            var taskEntity = new TaskEntity();
            taskEntity.Id = taskAggregate.Id;
            taskEntity.Name = taskAggregate.Name;
            taskEntity.ActiveStateId = taskAggregate.ActiveState.StateId;
            taskEntity.FlowId = taskAggregate.Flow.Id;

            var taskHistoryEntity = new TaskHistoryEntity();
            taskHistoryEntity.FlowId = taskAggregate.Flow.Id;
            taskHistoryEntity.Order = taskAggregate.ActiveState.Order;
            taskHistoryEntity.StateId = taskAggregate.ActiveState.StateId;

            await _unitOfWork.Tasks.Update(taskEntity);
            await _unitOfWork.TaskHistories.Add(taskHistoryEntity);
            _unitOfWork.Commit();
            return taskAggregate;
        }

        private async Task<TaskAggregate> PrepareTaskAggregate(Guid taskId)
        {
            var task = await _unitOfWork.Tasks.Get(taskId);
            if (task.FlowId == null)
                return null;
            var aggregate = new TaskAggregate();
            aggregate.Id = taskId;
            aggregate.Name = task.Name;

            var flow = await _unitOfWork.Flows.Get((Guid)task.FlowId);
            var flowModel = new FlowModel { Id = flow.Id,Name=flow.Name };
            flowModel.States = new List<FlowStateModel>();
            var stateEntities = await _unitOfWork.FlowStates.GetStatesByFlowId(flow.Id);
            foreach(var entity in stateEntities)
            {
               var state = await _unitOfWork.States.Get(entity.StateId);
                var flowstate = new FlowStateModel { Name = state.Name, Order = entity.Order, StateId = state.Id };
                flowModel.States.Add(flowstate);
            }
            aggregate.Flow = flowModel;
            
            aggregate.TaskHistory = new List<FlowStateModel>();
            var historyEntity = await _unitOfWork.TaskHistories.GetByTaskId(taskId);
            
            foreach(var entity in historyEntity)
            {
                var state = await _unitOfWork.States.Get(entity.StateId);
                var flowstate = new FlowStateModel { Name = state.Name, Order = entity.Order, StateId = state.Id };
                aggregate.TaskHistory.Add(flowstate);
            }

            if(aggregate.TaskHistory.Any())
            {
                aggregate.ActiveState = aggregate.TaskHistory.MaxBy(x => x.Order).First();
            }

            return aggregate;
        }
    }
}

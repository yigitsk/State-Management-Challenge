using ProceedCase.Business.Interfaces;
using ProceedCase.Entities;
using ProceedCase.Models.BusinessModels;
using ProceedCase.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Business
{
    public class TaskService : ICrudService<TaskModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Create(TaskModel model)
        {
            var entity = new TaskEntity();
            entity.Name = model.Name;
            entity.Id = Guid.NewGuid();
            var result = await _unitOfWork.Tasks.Add(entity);
            _unitOfWork.Commit();
            if (result == 1)
                return entity.Id;
            else
                return Guid.Empty;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _unitOfWork.Tasks.Delete(id);
            _unitOfWork.Commit();
            if (result > 0)
                return true;
            else
                return false;
        }

        public async Task<TaskModel> Get(Guid id)
        {
            var result = await _unitOfWork.Tasks.Get(id);
            var model = new TaskModel();
            model.Id = result.Id;
            model.Name = result.Name;
            var historyResult = await _unitOfWork.TaskHistories.GetByTaskId(id);
            foreach(var state in historyResult.OrderBy(x=>x.Order))
            {
              //  model.TaskHistory.Add(new StateModel { Id = state.Id,Name = state.Order, });
            }
            return model;
        }

        public async Task<IEnumerable<TaskModel>> GetAll()
        {
            var result = await _unitOfWork.Tasks.GetAll();
            var taskList = new List<TaskModel>();
            foreach (var entity in result)
            {
                taskList.Add(new TaskModel
                {
                    Id = entity.Id,
                    Name = entity.Name
                });
            }
            return taskList;
        }

        public async Task<Guid> Update(TaskModel model)
        {
            var entity = new TaskEntity();
            entity.Name = model.Name;
            var result = await _unitOfWork.Tasks.Update(entity);
            _unitOfWork.Commit();
            if (result > 0)
                return entity.Id;
            else
                return Guid.Empty;
        }
    }
}

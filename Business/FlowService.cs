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
    public class FlowService : ICrudService<FlowModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public FlowService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Create(FlowModel model)
        {
            var entity = new FlowEntity();
            entity.Name = model.Name;
            entity.Id = Guid.NewGuid();
            entity.States = new List<Guid>();
            foreach (var state in model.States)
            {
                entity.States.Add(state.Id);
            }
            var result = await _unitOfWork.Flows.Add(entity);
            if (result == 1)
                return entity.Id;
            else
                return Guid.Empty;
        }

        public async Task<bool> Delete(FlowModel model)
        {
            var result = await _unitOfWork.Flows.Delete(model.Id);
            if (result > 0)
                return true;
            else
                return false;
        }

        public async Task<FlowModel> Get(Guid id)
        {
            var result = await _unitOfWork.Flows.Get(id);
            var model = new FlowModel();
            model.Id = result.Id;
            model.Name = result.Name;
            model.States = new List<StateModel>();
            foreach(var state in result.States)
            {
               var stateEntity= await _unitOfWork.States.Get(state);
                var stateModel = new StateModel();
                stateModel.Id = stateEntity.Id;
                stateModel.Name = stateEntity.Name;
                model.States.Add(stateModel);
            }
            return model;
        }

        public async Task<Guid> Update(FlowModel model)
        {
            throw new NotImplementedException();
        }
    }
}

using ProceedCase.Business.Interfaces;
using ProceedCase.Entities;
using ProceedCase.Models.BusinessModels;
using ProceedCase.Repository.Interface;
using ProceedLabs.Models.BusinessModels;
using ProceedLabs.Models.Entities;
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
            var result = await _unitOfWork.Flows.Add(entity);

            foreach (var state in model.States)
            {
                var stateEntity = new FlowStatesEntity { FlowId = entity.Id, Id = Guid.NewGuid(), Order = state.Order, StateId = state.StateId, CreatedOn = DateTime.Now };
                await _unitOfWork.FlowStates.Add(stateEntity);
            }
            _unitOfWork.Commit();
            
            return entity.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _unitOfWork.Flows.Delete(id);
            _unitOfWork.Commit();
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
            model.States = new List<FlowStateModel>();
            var flowStates = await _unitOfWork.FlowStates.GetStatesByFlowId(model.Id);
            foreach(var flowState in flowStates)
            {
                var state = await _unitOfWork.States.Get(flowState.StateId);
                var stateModel = new FlowStateModel();
                stateModel.StateId = state.Id;
                stateModel.Order = flowState.Order;
                stateModel.Name = state.Name;
                model.States.Add(stateModel);
            }
            return model;
        }

        public async Task<IEnumerable<FlowModel>> GetAll()
        {
            var result = await _unitOfWork.Flows.GetAll();
            var flowList = new List<FlowModel>();
            foreach(var entity in result)
            {
                var model = new FlowModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.States = new List<FlowStateModel>();
                var flowStates = await _unitOfWork.FlowStates.GetStatesByFlowId(model.Id);
                foreach (var flowState in flowStates)
                {
                    var state = await _unitOfWork.States.Get(flowState.StateId);
                    var stateModel = new FlowStateModel();
                    stateModel.StateId = state.Id;
                    stateModel.Order = flowState.Order;
                    stateModel.Name = state.Name;
                    model.States.Add(stateModel);
                }
                flowList.Add(model);
            }
            return flowList;
        }

        public async Task<Guid> Update(FlowModel model)
        {
            throw new NotImplementedException();
        }
    }
}

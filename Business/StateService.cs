﻿using ProceedCase.Business.Interfaces;
using ProceedCase.Entities;
using ProceedCase.Models.BusinessModels;
using ProceedCase.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Business
{
    public class StateService:ICrudService<StateModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public StateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Create(StateModel model)
        {
            var entity = new StateEntity();
            entity.Name = model.Name;
            entity.Id = Guid.NewGuid();
            var result = await _unitOfWork.States.Add(entity);
            if (result == 1)
                return entity.Id;
            else
                return Guid.Empty;
                  
        }

        public async Task<bool> Delete(StateModel model)
        {
            var result = await _unitOfWork.States.Delete(model.Id);
            if (result > 0)
                return true;
            else
                return false;
        }

        public async Task<StateModel> Get(Guid id)
        {
            var result = await _unitOfWork.States.Get(id);
            var model = new StateModel();
            model.Id = result.Id;
            model.Name = result.Name;
            return model;
        }

        public async Task<Guid> Update(StateModel model)
        {
            var entity = new StateEntity();
            entity.Name = model.Name;
            entity.Id = model.Id;
            var result = await _unitOfWork.States.Update(entity);
            if (result > 0)
                return entity.Id;
            else
                return Guid.Empty;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ProceedCase.Business.Interfaces;
using ProceedCase.Models.BusinessModels;
using ProceedCase.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Controllers
{
    [ApiController]
    [Route("api/State")]
    public class StateController:ControllerBase
    {

        private ICrudService<StateModel> _stateService;
        public StateController(ICrudService<StateModel> stateService)
        {
            _stateService = stateService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateState([FromBody] CreateStateRequest request)
        {
            try
            {
                var stateModel = new StateModel();
                stateModel.Name = request.StateName;
                var id = await _stateService.Create(stateModel);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _stateService.GetAll();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("byId")]
        public async Task<IActionResult> Get([FromQuery]Guid id)
        {
            try
            {
                var state = await _stateService.Get(id);
                return Ok(state);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            try
            {
                var state = await _stateService.Delete(id);
                return Ok(state);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProceedCase.Business.Interfaces;
using ProceedCase.Models.BusinessModels;
using ProceedLabs.Models.ApiModels.Requests;
using ProceedLabs.Models.ApiModels.Validators;
using ProceedLabs.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Controllers
{
    [ApiController]
    [Route("api/v1/Flow")]
    public class FlowController : Controller
    {
        private ICrudService<FlowModel> _flowService;
        public FlowController(ICrudService<FlowModel> flowService)
        {
            _flowService = flowService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlow([FromBody] CreateFlowRequest request)
        {
            try
            {
                var validator = new CreateFlowRequestValidator();
                var validation = validator.Validate(request);
                if (!validation.IsValid)
                    return BadRequest(validation.Errors);
                
                var flowModel = new FlowModel();
                flowModel.Name = request.Name;
                flowModel.States = new List<FlowStateModel>();
                foreach(var state in request.States)
                {
                    flowModel.States.Add(new FlowStateModel
                    {
                        StateId = state.StateId,
                        Order = state.Order
                    });
                }
                var id = await _flowService.Create(flowModel);
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
                var list = await _flowService.GetAll();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("byId")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            try
            {
                var state = await _flowService.Get(id);
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
                var state = await _flowService.Delete(id);
                return Ok(state);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

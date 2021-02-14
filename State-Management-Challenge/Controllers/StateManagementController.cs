using Microsoft.AspNetCore.Mvc;
using ProceedCase.Models.Requests;
using ProceedLabs.Models.ApiModels.Requests;
using ProceedLabs.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Controllers
{

    [ApiController]
    [Route("api/v1/StateManagement")]
    public class StateManagementController:ControllerBase
    {
        private IStateManagementService _service;
        public StateManagementController(IStateManagementService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AssignFlowToTask([FromBody] AssignFlowToTaskRequest request)
        {
            try
            {
                var result = await _service.AssignFlowToTask(request.TaskId, request.FlowId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskStatus([FromQuery] Guid taskId)
        {
            try
            {
                var task = await _service.GetTaskAggregate(taskId);
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("next")]
        public async Task<IActionResult> NextState([FromQuery] Guid taskId)
        {
            try
            {
                var result = await _service.TaskNextState(taskId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("prev")]
        public async Task<IActionResult> PreviousState([FromQuery] Guid taskId)
        {
            try
            {
                var result = await _service.TaskPrevState(taskId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("rewind")]
        public async Task<IActionResult> TaskStateRewind([FromBody] TaskRewindRequest request)
        {
            try
            {
                var result = await _service.ChangeTaskState(request.TaskId,request.Order);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

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
    [Route("api/v1/Task")]
    public class TaskController : ControllerBase
    {

        private ICrudService<TaskModel> _taskService;
        public TaskController(ICrudService<TaskModel> taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateState([FromBody] CreateTaskRequest request)
        {
            try
            {
                var taskModel = new TaskModel();
                taskModel.Name = request.Name;
                var id = await _taskService.Create(taskModel);
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
                var list = await _taskService.GetAll();
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
                var state = await _taskService.Get(id);
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
                var state = await _taskService.Delete(id);
                return Ok(state);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

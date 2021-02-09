using Microsoft.AspNetCore.Mvc;
using ProceedCase.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Controllers
{
    [ApiController]
    [Route("[Task]")]
    public class TaskController : ControllerBase
    {

        public TaskController()
        {

        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

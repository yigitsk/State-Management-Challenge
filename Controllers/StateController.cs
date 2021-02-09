using Microsoft.AspNetCore.Mvc;
using ProceedCase.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Controllers
{
    public class StateController:ControllerBase
    {

        public StateController()
        {

        }

        [HttpPost]
        public async Task<IActionResult> CreateState([FromBody] CreateStateRequest request)
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

using System;
using System.Collections.Generic;
using System.Text;

namespace ProceedLabs.Models.ApiModels.Requests
{
    public class AssignFlowToTaskRequest
    {
        public Guid TaskId { get; set; }
        public Guid FlowId { get; set; }
    }
}

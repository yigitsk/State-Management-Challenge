using System;
using System.Collections.Generic;
using System.Text;

namespace ProceedLabs.Models.ApiModels.Requests
{
    public class CreateFlowRequest
    {
        public string Name { get; set; }
        public List<FlowStateRequest> States { get; set; }
    }

    public class FlowStateRequest
    {
        public Guid StateId { get; set; }
        public int Order { get; set; }
    }
}

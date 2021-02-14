using System;
using System.Collections.Generic;
using System.Text;

namespace ProceedLabs.Models.ApiModels.Requests
{
    public class TaskRewindRequest
    {
        public Guid TaskId { get; set; }
        public int Order { get; set; }
    }
}

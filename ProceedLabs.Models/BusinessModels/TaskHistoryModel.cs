using System;
using System.Collections.Generic;
using System.Text;

namespace ProceedLabs.Models.BusinessModels
{
    public class TaskHistoryModel
    {
        public Guid TaskId { get; set; }
        public Guid StateId { get; set; }

        public Guid FlowId { get; set; }
        public int Order { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ProceedLabs.Models.BusinessModels
{
    public class FlowStateModel
    {
        public Guid StateId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}

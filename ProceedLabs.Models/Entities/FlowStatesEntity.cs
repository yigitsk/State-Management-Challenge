using ProceedCase.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProceedLabs.Models.Entities
{
    public class FlowStatesEntity : BaseEntity
    {
        public Guid FlowId {get;set;}
        public Guid StateId { get; set; }
        public int Order { get; set; }

    }
}

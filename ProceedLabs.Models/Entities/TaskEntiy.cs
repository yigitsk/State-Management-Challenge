using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Entities
{
    public class TaskEntity : BaseEntity
    {
        public string Name { get; set; }
        public Guid? ActiveStateId { get; set; }
        public Guid? FlowId { get; set; }
    }
}

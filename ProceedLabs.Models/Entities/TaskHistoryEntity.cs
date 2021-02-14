using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Entities
{
    public class TaskHistoryEntity:BaseEntity
    {
        public Guid TaskId { get; set; }
        public Guid StateId { get; set; }

        public Guid FlowId { get; set; }
        public int Order { get; set; }
        
    }
}

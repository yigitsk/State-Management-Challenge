using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Models.BusinessModels
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ActiveStateId { get; set; }
        public Guid FlowId { get; set; }
        public List<StateModel> TaskHistory { get; set; }
    }
}

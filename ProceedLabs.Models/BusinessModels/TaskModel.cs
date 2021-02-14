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
        public StateModel ActiveState { get; internal set; }
        public FlowModel Flow { get;internal set; }

    }
}

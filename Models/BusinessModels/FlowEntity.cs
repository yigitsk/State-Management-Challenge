using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Models.BusinessModels
{
    public class FlowModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<StateModel> States { get; set; }
    }
}

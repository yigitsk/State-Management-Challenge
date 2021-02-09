using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Entities
{
    public class FlowEntity:BaseEntity
    {
        public string Name { get; set; }
        public List<Guid> States { get; set; }
    }
}

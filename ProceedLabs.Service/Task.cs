using ProceedLabs.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Models.BusinessModels
{
    public class TaskAggregate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public FlowStateModel ActiveState { get; internal set; }
        public FlowModel Flow { get;internal set; }
        public List<FlowStateModel> TaskHistory { get;internal set; }

        public TaskAggregate()
        {
            this.TaskHistory = new List<FlowStateModel>();
        }

        public void Next()
        {
            var activeState = Flow.States.Find(x => x.StateId == ActiveState.StateId);
            var nextState = Flow.States.Find(x => x.Order == activeState.Order + 1);
            this.ActiveState = nextState;
            AddHistory(nextState);
        }

        public void Previous()
        {
            var activeState = Flow.States.Find(x => x.StateId == ActiveState.StateId);
            var previuosState = Flow.States.Find(x => x.Order == activeState.Order -1 );
            this.ActiveState = previuosState;
            AddHistory(previuosState);
        }

        private void AddHistory(FlowStateModel state)
        {
            TaskHistory.Add(state);
        }
        
        public void AssignFlow(FlowModel assignedToBe)
        {
            this.Flow = assignedToBe;
            var startingState = this.Flow.States.Find(x => x.Order == 1);
            this.ActiveState = startingState;
            TaskHistory.Add(startingState);
        }
    }
}

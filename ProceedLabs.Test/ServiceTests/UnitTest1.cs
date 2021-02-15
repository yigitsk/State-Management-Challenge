using Moq;
using ProceedCase.Entities;
using ProceedCase.Models.BusinessModels;
using ProceedCase.Repository.Interface;
using ProceedLabs.Models.BusinessModels;
using ProceedLabs.Models.Entities;
using ProceedLabs.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ProceedLabs.Test
{
    public class UnitTest1
    {
        [Fact]
        public async void TaskAssignTest()
        {
            var taskAggregate = new TaskAggregate();
            taskAggregate.Id = Guid.NewGuid();
            taskAggregate.Name = "Test";

            var testFlow = new FlowModel { Id = Guid.NewGuid(), Name = "TestFlow" };
            testFlow.States = new List<FlowStateModel>();
            var state1 = new FlowStateModel();
            state1.Name = "State1";
            state1.Order = 1;
            state1.StateId = Guid.NewGuid();
            testFlow.States.Add(state1);
            var state2 = new FlowStateModel();
            state2.Name = "State2";
            state2.Order = 2;
            state2.StateId = Guid.NewGuid();
            testFlow.States.Add(state2);
            var state3 = new FlowStateModel();
            state3.Name = "State3";
            state3.Order = 3;
            state3.StateId = Guid.NewGuid();
            testFlow.States.Add(state3);
            taskAggregate.AssignFlow(testFlow);
            Assert.Same(taskAggregate.ActiveState, state1);
        }

        [Fact]
        public async void TaskNext()
        {
            var taskAggregate = new TaskAggregate();
            taskAggregate.Id = Guid.NewGuid();
            taskAggregate.Name = "Test";

            var testFlow = new FlowModel { Id = Guid.NewGuid(), Name = "TestFlow" };
            testFlow.States = new List<FlowStateModel>();
            var state1 = new FlowStateModel();
            state1.Name = "State1";
            state1.Order = 1;
            state1.StateId = Guid.NewGuid();
            testFlow.States.Add(state1);
            var state2 = new FlowStateModel();
            state2.Name = "State2";
            state2.Order = 2;
            state2.StateId = Guid.NewGuid();
            testFlow.States.Add(state2);
            var state3 = new FlowStateModel();
            state3.Name = "State3";
            state3.Order = 3;
            state3.StateId = Guid.NewGuid();
            testFlow.States.Add(state3);
            taskAggregate.AssignFlow(testFlow);
            taskAggregate.Next();
            Assert.Same(taskAggregate.ActiveState, state2);
        }

        [Fact]
        public async void TaskPrev()
        {
            var taskAggregate = new TaskAggregate();
            taskAggregate.Id = Guid.NewGuid();
            taskAggregate.Name = "Test";

            var testFlow = new FlowModel { Id = Guid.NewGuid(), Name = "TestFlow" };
            testFlow.States = new List<FlowStateModel>();
            var state1 = new FlowStateModel();
            state1.Name = "State1";
            state1.Order = 1;
            state1.StateId = Guid.NewGuid();
            testFlow.States.Add(state1);
            var state2 = new FlowStateModel();
            state2.Name = "State2";
            state2.Order = 2;
            state2.StateId = Guid.NewGuid();
            testFlow.States.Add(state2);
            var state3 = new FlowStateModel();
            state3.Name = "State3";
            state3.Order = 3;
            state3.StateId = Guid.NewGuid();
            testFlow.States.Add(state3);
            taskAggregate.AssignFlow(testFlow);
            taskAggregate.Next();
            taskAggregate.Next();
            taskAggregate.Previous();
            Assert.Same(taskAggregate.ActiveState, state2);
        }

        [Fact]
        public async void TaskPrepareAggregate()
        {
            var mockUOW = new Mock<IUnitOfWork>();
            var service = new StateManagementService(mockUOW.Object);
            var taskId = Guid.NewGuid();
            var flowId = Guid.NewGuid();
            var state1Id = Guid.NewGuid();
            var state2Id = Guid.NewGuid();
            var state3Id = Guid.NewGuid();
            mockUOW.Setup(x => x.Tasks.Get(taskId)).Returns(Task.FromResult(
                new TaskEntity
                {
                    Id = taskId,
                    ActiveStateId = state1Id,
                    FlowId = flowId,
                    Name = "TestTask",
                }));

            mockUOW.Setup(x => x.Flows.Get(flowId)).Returns(Task.FromResult(
               new FlowEntity
               {
                   Id = flowId,
                   Name = "TestFlow"
               }));

            mockUOW.Setup(x => x.FlowStates.GetStatesByFlowId(flowId)).Returns(Task.FromResult<IEnumerable<FlowStatesEntity>>(
               new List<FlowStatesEntity> {
                    new FlowStatesEntity
                    {
                        Id = Guid.NewGuid(),
                        FlowId = flowId,
                        Order=1,
                        StateId = state1Id
                    },
                     new FlowStatesEntity
                    {
                        Id = Guid.NewGuid(),
                        FlowId = flowId,
                        Order=2,
                        StateId = state2Id
                    },
                      new FlowStatesEntity
                    {
                        Id = Guid.NewGuid(),
                        FlowId = flowId,
                        Order=3,
                        StateId = state3Id
                    }
               }));

            mockUOW.Setup(x => x.States.Get(state1Id)).Returns(Task.FromResult(
                new StateEntity
                {
                    Id = state1Id,
                    Name = "state1"
                }
                ));
            mockUOW.Setup(x => x.States.Get(state2Id)).Returns(Task.FromResult(
                new StateEntity
                {
                    Id = state2Id,
                    Name = "state2"
                }
                ));
            mockUOW.Setup(x => x.States.Get(state3Id)).Returns(Task.FromResult(
                new StateEntity
                {
                    Id = state3Id,
                    Name = "state3"
                }
                ));

            mockUOW.Setup(x => x.TaskHistories.GetByTaskId(taskId)).Returns(Task.FromResult<IEnumerable<TaskHistoryEntity>>(
                new List<TaskHistoryEntity> { 
               new TaskHistoryEntity
               {
                   Id = Guid.NewGuid(),
                   FlowId = flowId,
                   Order=1,
                   StateId = state1Id,
                   TaskId = taskId
               }}
               ));



            var taskAggregate = await service.GetTaskAggregate(taskId);
            Assert.Equal(taskAggregate.ActiveState.StateId, state1Id);
        }

        [Fact]
        public async void TaskRewind()
        {
            var mockUOW = new Mock<IUnitOfWork>();
            var service = new StateManagementService(mockUOW.Object);
            var taskId = Guid.NewGuid();
            var flowId = Guid.NewGuid();
            var state1Id = Guid.NewGuid();
            var state2Id = Guid.NewGuid();
            var state3Id = Guid.NewGuid();
            mockUOW.Setup(x => x.Tasks.Get(taskId)).Returns(Task.FromResult(
                new TaskEntity
                {
                    Id = taskId,
                    ActiveStateId = state1Id,
                    FlowId = flowId,
                    Name = "TestTask",
                }));

            mockUOW.Setup(x => x.Flows.Get(flowId)).Returns(Task.FromResult(
               new FlowEntity
               {
                   Id = flowId,
                   Name = "TestFlow"
               }));

            mockUOW.Setup(x => x.FlowStates.GetStatesByFlowId(flowId)).Returns(Task.FromResult<IEnumerable<FlowStatesEntity>>(
               new List<FlowStatesEntity> {
                    new FlowStatesEntity
                    {
                        Id = Guid.NewGuid(),
                        FlowId = flowId,
                        Order=1,
                        StateId = state1Id
                    },
                     new FlowStatesEntity
                    {
                        Id = Guid.NewGuid(),
                        FlowId = flowId,
                        Order=2,
                        StateId = state2Id
                    },
                      new FlowStatesEntity
                    {
                        Id = Guid.NewGuid(),
                        FlowId = flowId,
                        Order=3,
                        StateId = state3Id
                    }
               }));

            mockUOW.Setup(x => x.States.Get(state1Id)).Returns(Task.FromResult(
                new StateEntity
                {
                    Id = state1Id,
                    Name = "state1"
                }
                ));
            mockUOW.Setup(x => x.States.Get(state2Id)).Returns(Task.FromResult(
                new StateEntity
                {
                    Id = state2Id,
                    Name = "state2"
                }
                ));
            mockUOW.Setup(x => x.States.Get(state3Id)).Returns(Task.FromResult(
                new StateEntity
                {
                    Id = state3Id,
                    Name = "state3"
                }
                ));

            mockUOW.Setup(x => x.TaskHistories.GetByTaskId(taskId)).Returns(Task.FromResult<IEnumerable<TaskHistoryEntity>>(
                new List<TaskHistoryEntity> {
               new TaskHistoryEntity
               {
                   Id = Guid.NewGuid(),
                   FlowId = flowId,
                   Order=1,
                   StateId = state1Id,
                   TaskId = taskId
               },
                  new TaskHistoryEntity
               {
                   Id = Guid.NewGuid(),
                   FlowId = flowId,
                   Order=2,
                   StateId = state2Id,
                   TaskId = taskId
               },
                  new TaskHistoryEntity
               {
                   Id = Guid.NewGuid(),
                   FlowId = flowId,
                   Order=3,
                   StateId = state3Id,
                   TaskId = taskId
               }}
               ));

            var taskAggregate = await service.GetTaskAggregate(taskId);
            Assert.Equal(taskAggregate.ActiveState.StateId, state3Id);
            taskAggregate = await service.ChangeTaskState(taskId, 1);
            Assert.Equal(taskAggregate.ActiveState.StateId, state1Id);
        }
    }
}

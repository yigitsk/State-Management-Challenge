using Dapper;
using ProceedCase.Repository.Interface;
using ProceedLabs.Models.Entities;
using ProceedLabs.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceedCase.Repository
{
    public interface IFlowStateRepository : IRepository<FlowStatesEntity>
    {
        Task<IEnumerable<FlowStatesEntity>> GetStatesByFlowId(Guid flowid);
    }
    internal class FlowStateRepository : RepositoryBase, IFlowStateRepository
    {
        public FlowStateRepository(IDbTransaction transaction)
           : base(transaction)
        {
        }
        public async Task<int> Add(FlowStatesEntity entity)
        {
            entity.CreatedOn = DateTime.Now;
            var sql = "INSERT INTO FlowStates (Id, FlowId, StateId, Order, CreatedOn) Values (@Id, @FlowId, @StateId, @Order, @CreatedOn);";
            var affectedRows = await Connection.ExecuteAsync(sql, new { Id = entity.Id, FlowId = entity.FlowId,StateId = entity.StateId, Order = entity.Order,CreatedOn = entity.CreatedOn}, Transaction);
            return affectedRows;
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }*/
        }

        public async Task<int> Delete(Guid id)
        {
            var sql = "DELETE FROM FlowStates WHERE Id = @Id;";
            var affectedRows = await Connection.ExecuteAsync(sql, new { Id = id }, Transaction);
            return affectedRows;
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows;
            }*/
        }

        public async Task<FlowStatesEntity> Get(Guid id)
        {
            var sql = "SELECT * FROM FlowStates WHERE Id = @Id;";
            var result = await Connection.QueryAsync<FlowStatesEntity>(sql, new { Id = id }, Transaction);
            return result.FirstOrDefault();
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<StateEntity>(sql, new { Id = id });
                return result.FirstOrDefault();
            }*/
        }

        public async Task<IEnumerable<FlowStatesEntity>> GetAll()
        {
            var sql = "SELECT * FROM FlowStates;";
            var result = await Connection.QueryAsync<FlowStatesEntity>(sql, Transaction);
            return result;
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<StateEntity>(sql);
                return result;
            }*/
        }

        public async Task<IEnumerable<FlowStatesEntity>> GetStatesByFlowId(Guid flowId)
        {
            var sql = "SELECT * FROM FlowStates where Flowd = @FlowId;";
            var result = await Connection.QueryAsync<FlowStatesEntity>(sql, new { FlowId = flowId }, Transaction);
            return result;
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<StateEntity>(sql);
                return result;
            }*/
        }


        public async Task<int> Update(FlowStatesEntity entity)
        {
            throw new NotImplementedException();
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE FlowStates SET Order = @Order WHERE Id = @Id;";
            var affectedRows = await Connection.ExecuteAsync(sql, entity, Transaction);
            return affectedRows;
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }*/
        }
    }
}

using Dapper;
using Microsoft.Extensions.Configuration;
using ProceedCase.Entities;
using ProceedCase.Repository.Interface;
using ProceedLabs.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Repository
{
    public interface ITaskHistoryRepository : IRepository<TaskHistoryEntity>
    {
        Task<IEnumerable<TaskHistoryEntity>> GetByTaskId(Guid taskId);
    }
    internal class TaskHistoryRepository :RepositoryBase, ITaskHistoryRepository
    {

        public TaskHistoryRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }
        public async Task<int> Add(TaskHistoryEntity entity)
        {
            entity.CreatedOn = DateTime.Now;
            var sql = "INSERT INTO TaskHistory (Id, TaskId, StateId, FlowId, Order,CreatedOn) Values (@Id, @TaskId, @StateId, @FlowId, @Order,@CreatedOn);";
            var affectedRows = await Connection.ExecuteAsync(sql, new {Id = entity.Id,TaskId = entity.TaskId, StateId = entity.StateId,FlowId = entity.FlowId,Order=entity.Order,CreatedOn = entity.CreatedOn }, Transaction);
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
            var sql = "DELETE FROM TaskHistory WHERE Id = @Id;";
            var affectedRows = await Connection.ExecuteAsync(sql, new { Id = id }, Transaction);
            return affectedRows;
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows;
            }*/
        }

        public async Task<TaskHistoryEntity> Get(Guid id)
        {
            var sql = "SELECT * FROM TaskHistory WHERE Id = @Id;";
            var result = await Connection.QueryAsync<TaskHistoryEntity>(sql, new { Id = id }, Transaction);
            return result.FirstOrDefault();
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TaskHistoryEntity>(sql, new { Id = id });
                return result.FirstOrDefault();
            }*/
        }

        public async Task<IEnumerable<TaskHistoryEntity>> GetAll()
        {
            var sql = "SELECT * FROM TaskHistory;";
            var result = await Connection.QueryAsync<TaskHistoryEntity>(sql, Transaction);
            return result;
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TaskHistoryEntity>(sql);
                return result;
            }*/
        }

        public async Task<IEnumerable<TaskHistoryEntity>> GetByTaskId(Guid taskId)
        {
            var sql = "SELECT * FROM TaskHistory Where TaskId = @taskId ;";
            var result = await Connection.QueryAsync<TaskHistoryEntity>(sql, new { taskId = taskId }, Transaction);
            return result;
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TaskHistoryEntity>(sql, new { taskId = taskId });
                return result;
            }*/
        }

        public async Task<int> Update(TaskHistoryEntity entity)
        {
            throw new NotImplementedException();
            /*entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE TaskHistory SET Name = @Name, Description = @Description, Status = @Status, DueDate = @DueDate, DateModified = @DateModified WHERE Id = @Id;";
            var affectedRows = await Connection.ExecuteAsync(sql, entity, Transaction);
            return affectedRows;
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }*/
        }
    }
}

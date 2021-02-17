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
    public interface ITaskRepository : IRepository<TaskEntity>
    {
    }
    internal class TaskRepository : RepositoryBase, ITaskRepository
    {
        private readonly IConfiguration _configuration;

        public TaskRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }
        public async Task<int> Add(TaskEntity entity)
        {
            entity.CreatedOn = DateTime.Now;
            var sql = "INSERT INTO Tasks (Id, Name, CreatedOn) Values (@Id, @Name, @CreatedOn);";
            var affectedRows = await Connection.ExecuteAsync(sql, new { Id = entity.Id,Name = entity.Name, CreatedOn = entity.CreatedOn }, Transaction);
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
            var sql = "DELETE FROM Tasks WHERE Id = @Id;";
            var affectedRows = await Connection.ExecuteAsync(sql, new { Id = id }, Transaction);
            return affectedRows;
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows;
            }*/
        }

        public async Task<TaskEntity> Get(Guid id)
        {
            var sql = "SELECT * FROM Tasks WHERE Id = @Id;";
            var result = await Connection.QueryAsync<TaskEntity>(sql, new { Id = id },Transaction);
            return result.FirstOrDefault();
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TaskEntity>(sql, new { Id = id });
                return result.FirstOrDefault();
            }*/
        }

        public async Task<IEnumerable<TaskEntity>> GetAll()
        {
            var sql = "SELECT * FROM Tasks;";
            var result = await Connection.QueryAsync<TaskEntity>(sql, transaction: Transaction);
            return result;
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TaskEntity>(sql);
                return result;
            }*/
        }

        public async Task<int> Update(TaskEntity entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE Tasks SET Name = @Name, FlowId = @FlowId, ActiveStateId = @ActiveStateId, ModifiedOn = @ModifiedOn WHERE Id = @Id;";
            var affectedRows = await Connection.ExecuteAsync(sql, new { Name = entity.Name, FlowId = entity.FlowId, ActiveStateId = entity.ActiveStateId,ModifiedOn = entity.ModifiedOn, Id = entity.Id },Transaction);
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

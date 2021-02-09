using Dapper;
using Microsoft.Extensions.Configuration;
using ProceedCase.Entities;
using ProceedCase.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Repository
{
    public interface ITaskHistoryRepository : IRepository<TaskHistoryEntity>
    {
        Task<IEnumerable<TaskHistoryEntity>> GetByTaskId(Guid taskId);
    }
    public class TaskHistoryRepository : ITaskHistoryRepository
    {
        private readonly IConfiguration _configuration;

        public TaskHistoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> Add(TaskHistoryEntity entity)
        {
            entity.CreatedOn = DateTime.Now;
            var sql = "INSERT INTO Tasks (Name, Description, Status, DueDate, DateCreated) Values (@Name, @Description, @Status, @DueDate, @DateCreated);";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }

        public async Task<int> Delete(Guid id)
        {
            var sql = "DELETE FROM Tasks WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows;
            }
        }

        public async Task<TaskHistoryEntity> Get(Guid id)
        {
            var sql = "SELECT * FROM Tasks WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TaskHistoryEntity>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<TaskHistoryEntity>> GetAll()
        {
            var sql = "SELECT * FROM Tasks;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TaskHistoryEntity>(sql);
                return result;
            }
        }

        public async Task<IEnumerable<TaskHistoryEntity>> GetByTaskId(Guid taskId)
        {
            var sql = "SELECT * FROM TaskHistories Where TaskId = @taskId ;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TaskHistoryEntity>(sql, new { taskId = taskId });
                return result;
            }
        }

        public async Task<int> Update(TaskHistoryEntity entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE Tasks SET Name = @Name, Description = @Description, Status = @Status, DueDate = @DueDate, DateModified = @DateModified WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }
    }
}

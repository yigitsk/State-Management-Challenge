using Dapper;
using Microsoft.Extensions.Configuration;
using ProceedCase.Entities;
using ProceedCase.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProceedCase.Repository
{
    public interface IStateRepository : IRepository<StateEntity>
    {
        
    }
    public class StateRepository : IStateRepository
    {
        private readonly IConfiguration _configuration;

        public StateRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> Add(StateEntity entity)
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

        public async Task<StateEntity> Get(Guid id)
        {
            var sql = "SELECT * FROM Tasks WHERE Id = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<StateEntity>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<StateEntity>> GetAll()
        {
            var sql = "SELECT * FROM Tasks;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<StateEntity>(sql);
                return result;
            }
        }

        public async Task<int> Update(StateEntity entity)
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

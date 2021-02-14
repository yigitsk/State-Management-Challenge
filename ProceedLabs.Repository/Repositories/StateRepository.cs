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
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProceedCase.Repository
{
    public interface IStateRepository : IRepository<StateEntity>
    {
    }
    internal class StateRepository :RepositoryBase, IStateRepository
    {

        public StateRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }
        public async Task<int> Add(StateEntity entity)
        {
            entity.CreatedOn = DateTime.Now;
            var sql = "INSERT INTO States (Id, Name, CreatedOn) Values (@Id, @Name, @CreatedOn);";
            var affectedRows = await Connection.ExecuteAsync(sql, new { Id = entity.Id, Name = entity.Name, CreatedOn = entity.CreatedOn }, Transaction);
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
            var sql = "DELETE FROM States WHERE Id = @Id;";
            var affectedRows = await Connection.ExecuteAsync(sql, new { Id = id }, Transaction);
            return affectedRows;
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows;
            }*/
        }

        public async Task<StateEntity> Get(Guid id)
        {
            var sql = "SELECT * FROM States WHERE Id = @Id;";
            var result = await Connection.QueryAsync<StateEntity>(sql, new { Id = id }, Transaction);
            return result.FirstOrDefault();
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<StateEntity>(sql, new { Id = id });
                return result.FirstOrDefault();
            }*/
        }

        public async Task<IEnumerable<StateEntity>> GetAll()
        {
            var sql = "SELECT * FROM States;";
            var result = await Connection.QueryAsync<StateEntity>(sql, Transaction);
            return result;
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<StateEntity>(sql);
                return result;
            }*/
        }


        public async Task<int> Update(StateEntity entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE States SET Name = @Name, ModifiedOn = @ModifiedOn WHERE Id = @Id;";
            var affectedRows = await Connection.ExecuteAsync(sql, new { Name = entity.Name,ModifiedOn = entity.ModifiedOn,Id=entity.Id }, Transaction);
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

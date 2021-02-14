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
    public interface IFlowRepository : IRepository<FlowEntity>
    {

    }
    internal class FlowRepository : RepositoryBase, IFlowRepository
    {

        public FlowRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public async Task<int> Add(FlowEntity entity)
        {
            entity.CreatedOn = DateTime.Now;
            var sql = "INSERT INTO Flows (Id, Name, CreatedOn) Values (@Id,@Name, @CreatedOn);";
            var affectedRows = await Connection.ExecuteAsync(sql, new { Id = entity.Id, Name = entity.Name, CreatedOn = entity.CreatedOn },Transaction);
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
            var sql = "DELETE FROM Flows WHERE Id = @Id;";
            var affectedRows = await Connection.ExecuteAsync(sql, new { Id = id },Transaction);
            return affectedRows;
            /*
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                return affectedRows;
            }*/
        }

        public async Task<FlowEntity> Get(Guid id)
        {
            var sql = "SELECT * FROM Flows WHERE Id = @Id;";
            var result = await Connection.QueryAsync<FlowEntity>(sql, new { Id = id },Transaction);
            return result.FirstOrDefault();
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<FlowEntity>(sql, new { Id = id });
                return result.FirstOrDefault();
            }*/
        }

        public async Task<IEnumerable<FlowEntity>> GetAll()
        {
            var sql = "SELECT * FROM Flows;";
            var result = await Connection.QueryAsync<FlowEntity>(sql,Transaction);
            return result;
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<FlowEntity>(sql);
                return result;
            }*/
        }

        public async Task<int> Update(FlowEntity entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE Tasks SET Name = @Name, ModifiedOn = @DateModified WHERE Id = @Id;";
            var affectedRows = await Connection.ExecuteAsync(sql, new { Name = entity.Name,ModifiedOn = entity.ModifiedOn, Id = entity.Id },Transaction);
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

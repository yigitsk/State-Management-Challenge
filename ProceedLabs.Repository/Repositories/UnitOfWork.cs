using Microsoft.Extensions.Configuration;
using ProceedCase.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedCase.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IConfiguration _configuration;
       
        private ITaskRepository _tasksRepository;
        private IFlowRepository _flowRepository;
        private ITaskHistoryRepository _taskHistoryRepository;
        private IStateRepository _stateRepository;
        private IFlowStateRepository _flowstateRepository;


        private bool _disposed;


        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("localDb"));
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public ITaskRepository Tasks
        {
            get { return _tasksRepository ?? (_tasksRepository = new TaskRepository(_transaction)); }
        }

        public IFlowRepository Flows
        {
            get { return _flowRepository ?? (_flowRepository = new FlowRepository(_transaction)); }
        }

        public IStateRepository States
        {
            get { return _stateRepository ?? (_stateRepository = new StateRepository(_transaction)); }
        }

        public IFlowStateRepository FlowStates
        {
            get { return _flowstateRepository ?? (_flowstateRepository = new FlowStateRepository(_transaction)); }
        }

        public ITaskHistoryRepository TaskHistories
        {
            get { return _taskHistoryRepository ?? (_taskHistoryRepository = new TaskHistoryRepository(_transaction)); }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _tasksRepository = null;
            _flowRepository = null;
            _stateRepository = null;
            _taskHistoryRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}

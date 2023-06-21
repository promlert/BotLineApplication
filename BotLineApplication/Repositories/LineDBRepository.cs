using BotLineApplication.Configuration;
using BotLineApplication.Models;
using Dapper.Contrib.Extensions;
using Line;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BotLineApplication.Repositories
{
    public interface ILineDBRepository
    {
        Task<IEnumerable<SourceState>> GetAll();
        Task<SourceState> GetByUserName(string UserName);
        Task<SourceState> GetByEmail(string email);
        Task<int> Create(SourceState user);
        Task<bool> Update(SourceState user);
        Task<bool> Delete(SourceState user);
    }

    public class LineDBRepository : ILineDBRepository
    {
        ConnectionConfiguration _connection;
        public LineDBRepository(ConnectionConfiguration connection)
          
        {
          _connection = connection;
        }
        public async Task<int> Create(SourceState user)
        {
            using (IDbConnection db = new SqlConnection(_connection.Connection))
            {
                db.Open();
               return await db.InsertAsync<SourceState>(user);
            }
        }

        public async Task<bool> Delete(SourceState user)
        {
            using (IDbConnection db = new SqlConnection(_connection.Connection))
            {
                return await db.DeleteAsync<SourceState>(user);
            }
        }

        public Task<IEnumerable<SourceState>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<SourceState> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<SourceState> GetByUserName(string  UserName)
        {
            using (IDbConnection db = new SqlConnection(_connection.Connection))
            {
                return await db.GetAsync<SourceState>(UserName);
            }
        }

        public async Task<bool> Update(SourceState user)
        {
            using (IDbConnection db = new SqlConnection(""))
            {
                return await db.UpdateAsync<SourceState>(user);
            }
        }
    }
}

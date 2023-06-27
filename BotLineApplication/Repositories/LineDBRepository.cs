using BotLineApplication.Configuration;
using BotLineApplication.Models;
using Dapper;
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
        Task<SourceState> GetByUserName(string UserName);
        Task<int> Create(SourceState user);
        Task<bool> Update(SourceState user);
        Task<bool> Delete(SourceState user);
        Task<IEnumerable<LogMessage>> GetLogByUserId(string UserId);
        Task<int> Create(LogMessage user);
        Task<bool> Update(LogMessage user);
        Task<bool> Delete(LogMessage user);
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

        public async Task<int> Create(LogMessage log)
        {
            using (IDbConnection db = new SqlConnection(_connection.Connection))
            {
                db.Open();
                return await db.InsertAsync<LogMessage>(log);
            }
        }

        public async Task<bool> Delete(SourceState user)
        {
            using (IDbConnection db = new SqlConnection(_connection.Connection))
            {
                return await db.DeleteAsync(user);
            }
        }

        public async Task<bool> Delete(LogMessage user)
        {
            using (IDbConnection db = new SqlConnection(_connection.Connection))
            {
                return await db.DeleteAsync(user);
            }
        }

        public async Task<SourceState> GetByUserName(string  UserName)
        {
            using (IDbConnection db = new SqlConnection(_connection.Connection))
            {
                return await db.GetAsync<SourceState>(UserName);
            }
        }

        public async Task<IEnumerable<LogMessage>> GetLogByUserId(string UserId)
        {
            using (IDbConnection db = new SqlConnection(_connection.Connection))
            {
               // return await db.GetAsync<List<LogMessage>>(UserName);
               return await db.QueryAsync<LogMessage>("select top 1 * from LogMessage where UserId = @UserId order by Id desc",new { UserId = UserId});
            }
        }

        public async Task<bool> Update(SourceState user)
        {
            using (IDbConnection db = new SqlConnection(_connection.Connection))
            {
                return await db.UpdateAsync<SourceState>(user);
            }
        }

        public async Task<bool> Update(LogMessage user)
        {
            using (IDbConnection db = new SqlConnection(_connection.Connection))
            {
                return await db.UpdateAsync<LogMessage>(user);
            }
        }

    
    }
}

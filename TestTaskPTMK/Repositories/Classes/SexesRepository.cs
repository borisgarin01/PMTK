using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using TestTaskPTMK.Models;
using TestTaskPTMK.Repositories.Interfaces.Base;

namespace TestTaskPTMK.Repositories.Classes
{
    public class SexesRepository : ISexesRepository
    {
        public SexesRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public string ConnectionString { get; }
        public async Task CreateAsync(Sex sex)
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(ConnectionString))
            {
                await dbConnection.ExecuteAsync("INSERT INTO Sexes(name);", new { sex.Name });
            }
        }

        public async Task DeleteAsync(long id)
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(ConnectionString))
            {
                await dbConnection.ExecuteAsync("DELETE FROM Sexes WHERE Id=@Id;", new { id });
            }
        }

        public async Task<IEnumerable<Sex>> GetAllAsync()
        {
            IEnumerable<Sex> sexes;
            using (IDbConnection dbConnection = new NpgsqlConnection(ConnectionString))
            {
                sexes = await dbConnection.QueryAsync<Sex>("SELECT * FROM Sexes;");
            }
            return sexes;
        }

        public async Task<Sex> GetAsync(long id)
        {
            Sex sex;
            using (IDbConnection dbConnection = new NpgsqlConnection(ConnectionString))
            {
                sex = await dbConnection.QueryFirstOrDefaultAsync<Sex>("SELECT * FROM Sexes WHERE Id=@Id;", new { Id = id });
            }
            return sex;
        }

        public async Task UpdateAsync(Sex sex)
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(ConnectionString))
            {
                await dbConnection.ExecuteAsync("UPDATE Sexes SET Name=@Name WHERE Id=@Id;",
                new { Name = sex.Name, Id = sex.Id });
            }
        }
    }
}
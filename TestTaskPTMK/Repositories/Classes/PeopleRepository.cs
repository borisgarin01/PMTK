using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using TestTaskPTMK.Models;
using TestTaskPTMK.Repositories.Interfaces.Base;

namespace TestTaskPTMK.Repositories.Classes
{
    public class PeopleRepository : IPeopleRepository
    {
        public PeopleRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public string ConnectionString { get; }
        public async Task CreateAsync(Person person)
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(ConnectionString))
            {
                await dbConnection.ExecuteAsync("INSERT INTO People(last_name, first_name, middle_name, birth_date, sex_id) VALUES(@last_name, @first_name, @middle_name, @birth_date, @sex_id);", new { person.LastName, person.FirstName, person.MiddleName, person.BirthDate, person.SexId });
            }
        }

        public async Task DeleteAsync(long id)
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(ConnectionString))
            {
                await dbConnection.ExecuteAsync("DELETE FROM People WHERE Id=@Id;", new { id });
            }
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            IEnumerable<Person> people;
            using (IDbConnection dbConnection = new NpgsqlConnection(ConnectionString))
            {
                people = await dbConnection.QueryAsync<Person>("SELECT * FROM People;");
            }
            return people;
        }

        public async Task<IEnumerable<Person>> GetOrderedAsync()
        {
            IEnumerable<Person> people;
            using (IDbConnection dbConnection = new NpgsqlConnection(ConnectionString))
            {
                people = await dbConnection.QueryAsync<Person>("SELECT DISTINCT Last_Name, First_Name, Middle_Name, Sexes.Name as Sex FROM People INNER JOIN Sexes ON People.Sex_Id = Sexes.Id ORDER BY Last_Name, First_Name, Middle_Name;");
            }
            return people;
        }

        public async Task<Person> GetAsync(long id)
        {
            Person person;
            using (IDbConnection dbConnection = new NpgsqlConnection(ConnectionString))
            {
                person = await dbConnection.QueryFirstOrDefaultAsync<Person>("SELECT * FROM People WHERE Id=@Id;", new { Id = id });
            }
            return person;
        }

        public async Task UpdateAsync(Person person)
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(ConnectionString))
            {
                await dbConnection.ExecuteAsync("UPDATE People SET BirthDate=@BirthDate, FirstName=@FirstName, LastName=@LastName, MiddleName=@MiddleName, SexId=@SexId WHERE Id=@Id;",
                new { BirthDate = person.BirthDate, FirstName = person.FirstName, LastName = person.LastName, MiddleName = person.MiddleName, SexId = person.SexId, Id = person.Id });
            }
        }

        public async Task<IEnumerable<Person>> GetByLastName(string lastName)
        {
            IEnumerable<Person> people;
            using (IDbConnection dbConnection = new NpgsqlConnection(ConnectionString))
            {
                people = await dbConnection.QueryAsync<Person>("SELECT DISTINCT Last_Name, First_Name, Middle_Name, Sexes.Name as Sex FROM People INNER JOIN Sexes ON People.Sex_Id = Sexes.Id ORDER BY Last_Name, First_Name, Middle_Name WHERE LastName Like %@lastName%;", new { LastName = lastName });
            }
            return people;
        }
    }
}
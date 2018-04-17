using System.Collections.Generic;
using System.Data.SqlClient;

namespace TempDataDemo.Data
{
    public class PersonDb
    {
        private string _connectionString;
        public PersonDb(string conStr)
        {
            _connectionString = conStr;
        }

        public IEnumerable<Person> GetPeople()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM People";
                connection.Open();
                List<Person> ppl = new List<Person>();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ppl.Add(new Person
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Age = (int)reader["Age"],
                    });
                }

                return ppl;
            }
        }

        public void AddPerson(Person person)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand cmd = connection.CreateCommand())


            {
                cmd.CommandText = "INSERT INTO People (FirstName, LastName, Age) " +
                                  "VALUES (@firstName, @lastName, @age) SELECT SCOPE_IDENTITY()";
                cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                cmd.Parameters.AddWithValue("@lastName", person.LastName);
                cmd.Parameters.AddWithValue("@age", person.Age);
                connection.Open();
                person.Id = (int)(decimal)cmd.ExecuteScalar();
            }
        }
    }
}
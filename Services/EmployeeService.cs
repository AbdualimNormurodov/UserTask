using System.Data;
using System.Data.SqlClient;
using UserTask.Enums;
using UserTask.Interface;
using UserTask.Models;

namespace UserTask.Services;

public class EmployeeService : IEmployeecs
{
    public Employee Create(Employee employee)
    {
        using (SqlConnection connection = new SqlConnection($"Server=(localdb)\\MSSQLLocalDB;Database=LMS;Trusted_Connection=True;"))
        {

            connection.Open();

            string query = "INSERT INTO Employee (Name, Surname, Email, Login, Password, Status) " +
                           "VALUES ( @Name, @Surname, @Email, @Login, @Password, @Status)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Surname", employee.Surname);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@Login", employee.Login);
                command.Parameters.AddWithValue("@Password", employee.Password);
                command.Parameters.AddWithValue("@Status", (int)employee.Status);

                command.ExecuteNonQuery();
            }
            return employee;
        }
    }
    public bool Delete(int id)
    {
        using (SqlConnection connection = new SqlConnection($"Server=(localdb)\\MSSQLLocalDB;Database=LMS;Trusted_Connection=True;"))
        {
            connection.Open();

            string query = "UPDATE Employee SET Status = @Status WHERE Id = @Id";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.Parameters.Add("@Status", SqlDbType.NVarChar).Value = Status.Deleted;
            command.Parameters.Add("@Deleted", SqlDbType.NVarChar).Value = DateTime.Now.ToString();

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }

    }
    public bool DeepDelete(int id)
    {
        using (SqlConnection connection = new SqlConnection($"Server=(localdb)\\MSSQLLocalDB;Database=LMS;Trusted_Connection=True;"))
        {
            connection.Open();

            string query = "DELETE FROM Employee WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", id);

            int affectedRows = command.ExecuteNonQuery();

            return affectedRows > 0;
        }


    }
    public Employee Update(int id, Employee employee)
    {
        using (SqlConnection connection = new SqlConnection($"Server=(localdb)\\MSSQLLocalDB;Database=LMS;Trusted_Connection=True;"))
        {
            connection.Open();
            string query = "UPDATE Employee SET Name = @Name, Surname = @Surname, " +
                               "Email = @Email, Login = @Login, Password = @Password," +
                               " Status = @Status WHERE Id = @Id";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = employee.Name;
            command.Parameters.Add("@Surname", SqlDbType.NVarChar).Value = employee.Surname;
            command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = employee.Email;
            command.Parameters.Add("@Login", SqlDbType.NVarChar).Value = employee.Login;
            command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = employee.Password;
            command.Parameters.Add("@Status", SqlDbType.NVarChar).Value = (int)Status.Updated;



            int rowsAffected = command.ExecuteNonQuery();
            return employee;


        }
    }
    public Employee GetById(int id)
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=LMS;Trusted_Connection=True;";
        Employee employee = null;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Employee WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Surname = reader.GetString(reader.GetOrdinal("Surname")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Login = reader.GetString(reader.GetOrdinal("Login")),
                            Password = reader.GetString(reader.GetOrdinal("Password")),
                            Status = (Status)int.Parse(reader[6].ToString())
                        };
                    }
                }
            }
        }

        if (employee is null || employee.Status == Status.Deleted)
        {
            return null;
        }

        return employee;

    }
    public List<Employee> GetAll()
    {
        using (SqlConnection connection = new SqlConnection($"Server=(localdb)\\MSSQLLocalDB;Database=LMS;Trusted_Connection=True;"))
        {
            connection.Open();

            string query = "SELECT * FROM Employee";
            SqlCommand command = new SqlCommand(query, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                List<Employee> employees = new List<Employee>();
                int index = 0;

                while (reader.Read())
                {
                    Employee employee = new Employee()
                    {
                        Id = int.Parse(reader[0].ToString()),
                        Name = reader["Name"].ToString(),
                        Surname = reader["Surname"].ToString(),
                        Email = reader["email"].ToString(),
                        Login = reader["Login"].ToString(),
                        Password = reader["Password"].ToString(),
                        Status = (Status)int.Parse(reader[6].ToString())
                    };
                    employees.Add(employee);
                }
                employees = employees.Where(employee => employee.Status!=Status.Deleted).ToList();

                if (employees is null)
                    return new List<Employee>();
                return employees;
            };
            

            

        }
    }




}


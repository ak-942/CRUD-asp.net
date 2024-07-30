using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class Employee_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["AdoEmployee"].ToString();
        
        //Get All Employee
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employeeList = new List<Employee>();

            using (SqlConnection connection= new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllProducts";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtEmployees = new DataTable();

                connection.Open();
                sqlDA.Fill(dtEmployees);
                connection.Close();

                foreach (DataRow dr in dtEmployees.Rows)
                {
                    employeeList.Add(new Employee
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        Name = dr["Name"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Phone = Convert.ToInt32(dr["Phone"]),
                        Email = dr["Email"].ToString(),
                    }) ;

                }
            }

            return employeeList;
        }

        //Insert Employee Detail
        public bool InsertDetail(Employee emp)
        {
            int id = 0;
            using(SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("spak_InsertData", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", emp.Name);
                command.Parameters.AddWithValue("@Age", emp.Age);
                command.Parameters.AddWithValue("@Phone", emp.Phone);
                command.Parameters.AddWithValue("@Email", emp.Email);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            if (id > 0)
                return true;
            else
                return false;

        }

        //Get Employee by ID
        public List<Employee> GetEmployeesbyID(int ID)
        {
            List<Employee> employeeList = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spak_GetEmployeeByID";
                command.Parameters.AddWithValue("@ID", ID);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtEmployees = new DataTable();

                connection.Open();
                sqlDA.Fill(dtEmployees);
                connection.Close();

                foreach (DataRow dr in dtEmployees.Rows)
                {
                    employeeList.Add(new Employee
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        Name = dr["Name"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Phone = Convert.ToInt32(dr["Phone"]),
                        Email = dr["Email"].ToString(),
                    });

                }
            }

            return employeeList;
        }

        //Update Employee Detail
        public bool UpdateDetail(Employee emp)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("spak_UpdateData", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", emp.ID);
                command.Parameters.AddWithValue("@Name", emp.Name);
                command.Parameters.AddWithValue("@Age", emp.Age);
                command.Parameters.AddWithValue("@Phone", emp.Phone);
                command.Parameters.AddWithValue("@Email", emp.Email);

                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();
            }
            if (i > 0)
                return true;
            else
                return false;

        }

        //Delete Employee Detail
        public string DeleteDetail(int empid)
        {
            string result = "";

            using(SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("SPAK_DeleteEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", empid);
                command.Parameters.Add("@RETURNMESSAGE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@RETURNMESSAGE"].Value.ToString();
                connection.Close();
            }
            return result;
        }
    }
}
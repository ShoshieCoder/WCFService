using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NewWcfService
{
    public class Service1 : IService1
    {
        //id=0 in request for all students
        //if not will give a specific student
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Post")]
        public List<Student> GetInfo(Student student)
        {
            if (student.id == 0)
            {
                List<Student> students = new List<Student>();

                using (SqlConnection conn = new SqlConnection(DatabaseCfg.CONNECTION_DB))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("Select * From Student", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                students.Add(new Student
                                {
                                    id = (int)reader["ID"],
                                    name = (string)reader["Name"],
                                    addressId = (int)reader["Address_Id"]
                                });
                            }
                        }
                    }
                }
                return students;
            }
            else
            {
                List<Student> students = new List<Student>();

                using (SqlConnection conn = new SqlConnection(DatabaseCfg.CONNECTION_DB))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand($"Select * From Student where ID={student.id}", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                students.Add(new Student
                                {
                                    id = (int)reader["ID"],
                                    name = (string)reader["Name"],
                                    addressId = (int)reader["Address_Id"]
                                });
                            }
                        }
                    }
                }
                return students;
            }

        }

        //method will accept any token
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Inner")]
        public List<Student> InnerJoin(JSONMsg msg)
        {

            List<Student> students = new List<Student>();

            using (SqlConnection conn = new SqlConnection(DatabaseCfg.CONNECTION_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Select * From Student Inner Join Address On Student.Address_Id = Address.ID", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                id = (int)reader["ID"],
                                name = (string)reader["Name"],
                                addressId = (int)reader["Address_Id"]
                            });
                        }
                    }
                }
            }
            return students;
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using FinapCRUD.Models;

namespace FinapCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public StudentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            SELECT StudentID
                                  ,FirstName
                                  ,LastName
                                  ,ContactPerson
                                  ,ContactNo
                                  ,EmailAddress
                                  ,DateOfBirth
                                  ,Age
                                  ,ClassroomID
                              FROM dbo.Students
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("conn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"
                            SELECT StudentID
                                  ,FirstName
                                  ,LastName
                                  ,ContactPerson
                                  ,ContactNo
                                  ,EmailAddress
                                  ,DateOfBirth
                                  ,Age
                                  ,ClassroomID
                              FROM dbo.Students
                            WHERE StudentID = @StudentID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("conn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@StudentID", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Students stu)
        {
            string query = @"
                           INSERT INTO dbo.Students
                                       (FirstName
                                       ,LastName
                                       ,ContactPerson
                                       ,ContactNo
                                       ,EmailAddress
                                       ,DateOfBirth
                                       ,ClassroomID)
                                 VALUES
                                       (@FirstName
                                       ,@LastName
                                       ,@ContactPerson
                                       ,@ContactNo
                                       ,@EmailAddress
                                       ,@DateOfBirth
                                       ,@ClassroomID)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("conn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@FirstName", stu.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", stu.LastName);
                    myCommand.Parameters.AddWithValue("@ContactPerson", stu.ContactPerson);
                    myCommand.Parameters.AddWithValue("@ContactNo", stu.ContactNo);
                    myCommand.Parameters.AddWithValue("@EmailAddress", stu.EmailAddress);
                    myCommand.Parameters.AddWithValue("@DateOfBirth", stu.DateOfBirth);
                    myCommand.Parameters.AddWithValue("@ClassroomID", stu.ClassroomID);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Students stu)
        {
            string query = @"
                           UPDATE dbo.Students
                               SET FirstName = @FirstName
                                  ,LastName = @LastName
                                  ,ContactPerson = @ContactPerson
                                  ,ContactNo = @ContactNo
                                  ,EmailAddress = @EmailAddress
                                  ,DateOfBirth = @DateOfBirth
                                  ,ClassroomID = @ClassroomID
                             WHERE StudentID = @StudentID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("conn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@FirstName", stu.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", stu.LastName);
                    myCommand.Parameters.AddWithValue("@ContactPerson", stu.ContactPerson);
                    myCommand.Parameters.AddWithValue("@ContactNo", stu.ContactNo);
                    myCommand.Parameters.AddWithValue("@EmailAddress", stu.EmailAddress);
                    myCommand.Parameters.AddWithValue("@DateOfBirth", stu.DateOfBirth);
                    myCommand.Parameters.AddWithValue("@ClassroomID", stu.ClassroomID);
                    myCommand.Parameters.AddWithValue("@StudentID", stu.StudentID);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           DELETE FROM dbo.Students
                            WHERE StudentID = @StudentID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("conn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@StudentID", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}

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
    public class TeachersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TeachersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            SELECT TeacherID
                                  ,FirstName
                                  ,LastName
                                  ,ContactNo
                                  ,EmailAddress
                              FROM dbo.Teachers
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
                    SELECT TeacherID
                          ,FirstName
                          ,LastName
                          ,ContactNo
                          ,EmailAddress
                    FROM dbo.Teachers
                    WHERE TeacherID = @TeacherID
                    ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("conn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TeacherID", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }



        [HttpPost]
        public JsonResult Post(Teachers tea)
        {
            string query = @"
                           INSERT INTO dbo.Teachers
                                   (FirstName
                                   ,LastName
                                   ,ContactNo
                                   ,EmailAddress)
                             VALUES
                                   (@FirstName
                                   ,@LastName
                                   ,@ContactNo
                                   ,@EmailAddress)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("conn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@FirstName", tea.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", tea.LastName);
                    myCommand.Parameters.AddWithValue("@ContactNo", tea.ContactNo);
                    myCommand.Parameters.AddWithValue("@EmailAddress", tea.EmailAddress);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Teachers tea)
        {
            string query = @"
                           UPDATE dbo.Teachers
                               SET FirstName = @FirstName
                                  ,LastName = @LastName
                                  ,ContactNo = @ContactNo
                                  ,EmailAddress = @EmailAddress
                             WHERE TeacherID = @TeacherID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("conn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@FirstName", tea.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", tea.LastName);
                    myCommand.Parameters.AddWithValue("@ContactNo", tea.ContactNo);
                    myCommand.Parameters.AddWithValue("@EmailAddress", tea.EmailAddress);
                    myCommand.Parameters.AddWithValue("@TeacherID", tea.TeacherID);
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
                           DELETE FROM dbo.Teachers
                            WHERE TeacherID = @TeacherID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("conn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TeacherID", id);

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

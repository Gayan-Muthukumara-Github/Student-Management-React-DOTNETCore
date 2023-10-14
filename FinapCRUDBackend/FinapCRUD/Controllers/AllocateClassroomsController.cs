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
    public class AllocateClassroomsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AllocateClassroomsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            SELECT AC.AllocateClassroomID
                                   ,AC.TeacherID
                                    ,T.FirstName
									,T.LastName
                                   ,AC.ClassroomID
                                   ,C.ClassroomName
                            FROM dbo.AllocateClassrooms AC
                            INNER JOIN dbo.Classrooms C ON C.ClassroomID = AC.ClassroomID
                            INNER JOIN dbo.Teachers T ON T.TeacherID = AC.TeacherID
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

        [HttpPost]
        public JsonResult Post(AllocateClassrooms ac)
        {
            string query = @"
                           INSERT INTO dbo.AllocateClassrooms
                                       (TeacherID
                                       ,ClassroomID)
                                 VALUES
                                       (@TeacherID
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
                    myCommand.Parameters.AddWithValue("@TeacherID", ac.TeacherID);
                    myCommand.Parameters.AddWithValue("@ClassroomID", ac.ClassroomID);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(AllocateClassrooms ac)
        {
            string query = @"
                           UPDATE dbo.AllocateClassrooms
                               SET TeacherID = @TeacherID
                                  ,ClassroomID = @ClassroomID
                             WHERE  AllocateClassroomID = @AllocateClassroomID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("conn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TeacherID", ac.TeacherID);
                    myCommand.Parameters.AddWithValue("@ClassroomID", ac.ClassroomID);
                    myCommand.Parameters.AddWithValue("@AllocateClassroomID", ac.AllocateClassroomID);
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
                           DELETE FROM dbo.AllocateClassrooms
                                 WHERE AllocateClassroomID = @AllocateClassroomID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("conn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@AllocateClassroomID", id);

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

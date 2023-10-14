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
    public class AllocateSubjectsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AllocateSubjectsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            SELECT ALS.AllocateSubjectID
		                            ,ALS.TeacherID
									,T.FirstName
									,T.LastName
		                            ,ALS.SubjectID
		                            ,S.SubjectName
	                            FROM dbo.AllocateSubjects ALS
	                            INNER JOIN dbo.Subjects S ON S.SubjectID = ALS.SubjectID
								INNER JOIN dbo.Teachers T ON T.TeacherID = ALS.TeacherID
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
        public JsonResult Post(AllocateSubjects asub)
        {
            string query = @"
                           INSERT INTO dbo.AllocateSubjects
                                       (TeacherID
                                       ,SubjectID)
                                 VALUES
                                       (@TeacherID
                                       ,@SubjectID)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("conn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TeacherID", asub.TeacherID);
                    myCommand.Parameters.AddWithValue("@SubjectID", asub.SubjectID);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(AllocateSubjects asub)
        {
            string query = @"
                           UPDATE dbo.AllocateSubjects
                               SET TeacherID = @TeacherID
                                  ,SubjectID = @SubjectID
                             WHERE  AllocateSubjectID = @AllocateSubjectID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("conn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TeacherID", asub.TeacherID);
                    myCommand.Parameters.AddWithValue("@SubjectID", asub.SubjectID);
                    myCommand.Parameters.AddWithValue("@AllocateSubjectID", asub.AllocateSubjectID);
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
                           DELETE FROM dbo.AllocateSubjects
                                WHERE AllocateSubjectID = @AllocateSubjectID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("conn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@AllocateSubjectID", id);

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

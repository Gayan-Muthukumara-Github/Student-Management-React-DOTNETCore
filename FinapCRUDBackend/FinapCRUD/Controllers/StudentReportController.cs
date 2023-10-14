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
    public class StudentReportController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public StudentReportController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"
                            SELECT S.*,C.ClassroomName 
                            FROM dbo.Students S
                            INNER JOIN dbo.Classrooms C ON C.ClassroomID = S.ClassroomID
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
    }
}

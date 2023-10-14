using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinapCRUD.Models
{
    public class Students
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public string DateOfBirth { get; set; }
        public int ClassroomID { get; set; }
    }
}

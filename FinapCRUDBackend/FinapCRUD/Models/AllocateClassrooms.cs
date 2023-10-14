using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinapCRUD.Models
{
    public class AllocateClassrooms
    {
        public int AllocateClassroomID { get; set; }
        public int TeacherID { get; set; }
        public int ClassroomID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class CurrentStudentSubjectsDto
    {
        public StudentDto Student { get; set; }
        public List<VwStudentSubjectsDto> Subjects { get; set; }
    }
}

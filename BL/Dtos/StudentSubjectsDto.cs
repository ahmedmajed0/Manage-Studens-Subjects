using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class StudentSubjectsDto
    {

        public StudentDto Student { get; set; }
        public List<SubjectDto> Subjects { get; set; }
    }
}

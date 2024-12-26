using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class VwStudentSubjectsDto : BaseTableDto
    {
        public string Email { get; set; }
        public string SubjectName { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class StudentDto : BaseTableDto
    {
        public string FirstName { get; set; } = null!;
        public string LasttName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Level { get; set; }
    }
}

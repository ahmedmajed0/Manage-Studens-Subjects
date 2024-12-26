using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class VwStudenSubjects : BaseTable
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string SubjectName { get; set; } = null!;
    }
}

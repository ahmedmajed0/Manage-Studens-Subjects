using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class TbStudentSubjects : BaseTable
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }

        public virtual TbStudent? Student { get; set; }
        public virtual TbSubject? Subject { get; set; }
    }
}

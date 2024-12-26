using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class TbSubject : BaseTable
    {
        public TbSubject()
        {
            StudentSubjects = new HashSet<TbStudentSubjects>();
        }
        public string SubjectName { get; set; } = null!;
        public virtual ICollection<TbStudentSubjects> StudentSubjects { get; set; }
    }
}

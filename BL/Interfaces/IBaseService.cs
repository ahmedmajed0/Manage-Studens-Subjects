using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IBaseService<T,Dto> where T : BaseTable
    {
        List<Dto> GetAll();
        Dto GetById(Guid id);
        bool Add(Dto entity);
        bool AddRange(List<Dto> entities);
        bool Update(Dto entity);
        bool ChangeStatus(Guid id, int status = 1);
    }
}

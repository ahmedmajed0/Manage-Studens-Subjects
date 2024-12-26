using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseTable
    {
        List<T> GetAll();
        T GetById(Guid id);
        bool Add(T entity);
        bool AddRange(List<T> entities);
        bool Update(T entity);
        bool Delete(Guid id);
        bool DeleteRange(List<T> entities);
        bool ChangeStatus(Guid id, string userId, int status = 1);
        List<T> FindAll(Expression<Func<T, bool>> criteria);
    }
}

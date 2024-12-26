using DAL.Context;
using DAL.Exceptions;
using DAL.Interfaces;
using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repopsitories
{
    public class GenericRepository<T> :IGenericRepository<T> where T : BaseTable
    {
        private readonly StudentContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<GenericRepository<T>> _logger; 

        public GenericRepository(StudentContext context, ILogger<GenericRepository<T>> logger)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _logger = logger;
        }

        public List<T> GetAll()
        {
            try
            {
                return _dbSet.Where(a => a.CurrentState == 0).ToList();
            }
            catch(Exception ex) 
            {
                throw new DataAccessException(ex, "Error while getting the list", _logger);
            }
            
        }

        public T GetById(Guid id)
        {
            try
            {
                return _dbSet.AsNoTracking().FirstOrDefault(a => a.Id == id);
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "Error while getting by id ", _logger);
            }

        }

        public bool Add(T entity)
        {
            try
            {
                entity.CreatedDate = DateTime.Now;
                _dbSet.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "Error while add", _logger);
            }

        }

        public bool AddRange(List<T> entities)
        {
            try
            {
                _dbSet.AddRange(entities);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "Error while add", _logger);
            }

        }

        public bool Update(T entity)
        {
            try
            {
                var dbData = GetById(entity.Id);
                entity.CreatedBy = dbData.CreatedBy;
                entity.CreatedDate = dbData.CreatedDate;
                entity.UpdatedDate = DateTime.Now;
                entity.CurrentState = dbData.CurrentState;
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "Error while update ", _logger);
            }

        }

        public bool Delete(Guid id)
        {
            try
            {
                var entity = GetById(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "Error while delete  ", _logger);
            }

        }

        public bool DeleteRange(List<T> entities)
        {
            try
            {
                _dbSet.RemoveRange(entities);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "Error while add", _logger);
            }

        }

        public bool ChangeStatus(Guid id, string userId, int status = 1)
        {
            try
            {
                var entity = GetById(id);
                if (entity != null)
                {
                    entity.CurrentState = status;
                    entity.UpdatedBy = userId;
                    entity.UpdatedDate = DateTime.Now;
                    _context.Entry(entity).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "Error while change status  ", _logger);
            }

        }

        public List<T> FindAll(Expression<Func<T, bool>> criteria)
        {
            return _dbSet.Where(criteria).ToList();
        }


    }
}

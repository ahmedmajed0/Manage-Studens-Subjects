using AutoMapper;
using BL.Dtos;
using BL.Interfaces;
using DAL.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BaseService<T, Dto> : IBaseService<T, Dto> where T : BaseTable
    {
        private readonly IGenericRepository<T> _repo;
        private readonly IMapper _mapper;
        private readonly IUser _userService;
        public BaseService(IGenericRepository<T> repo, IMapper mapper, IUser userService)
        {
            _repo = repo;
            _mapper = mapper;
            _userService = userService;
        }
        public List<Dto> GetAll()
        {
            var entities = _repo.GetAll();
            return _mapper.Map<List<Dto>>(entities);
        }
        public Dto GetById(Guid id)
        {
            var entity = _repo.GetById(id);
            return _mapper.Map<Dto>(entity);
        }

        public bool Add(Dto entityDto)
        {
            var userId = "a10c8e1e60d5";
            var dbObject = _mapper.Map<T>(entityDto);
            dbObject.CreatedBy = userId; /*_userService.GetLoggedInUser();*/
            return _repo.Add(dbObject);
        }
        public bool AddRange(List<Dto> entities)
        {
            var userId = "a10c8e1e60d5";
            var dbObject = _mapper.Map<List<T>>(entities);
            dbObject.ForEach(a => { a.CreatedDate = DateTime.Now; a.CreatedBy = userId; }); /*_userService.GetLoggedInUser();*/
            _repo.AddRange(dbObject);
            return true;
        }


        public bool Update(Dto entityDto)
        {
            var userId = "a10c8e1e60d5";
            var dbObject = _mapper.Map<T>(entityDto);
            dbObject.UpdatedBy = userId; /*_userService.GetLoggedInUser();*/
            return _repo.Update(dbObject);
        }
        public bool ChangeStatus(Guid id, int status = 1)
        {
            var userId = "a10c8e1e60d5";
            return _repo.ChangeStatus(id, userId, status);
        }
    }
}

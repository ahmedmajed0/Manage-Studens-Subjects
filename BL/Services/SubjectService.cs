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
    //This model has been created for the incremental functions related to this model only.
    public class SubjectService : BaseService<TbSubject, SubjectDto>, ISubject
    {
        public SubjectService(IGenericRepository<TbSubject> repository, IMapper mapper, IUser userService) : base(repository, mapper, userService)
        {

        }
    }
}

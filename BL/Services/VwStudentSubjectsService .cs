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
    public class VwStudentSubjectsService : BaseService<VwStudenSubjects, VwStudentSubjectsDto>, IVwStudentSubjects
    {
        public VwStudentSubjectsService(IGenericRepository<VwStudenSubjects> repository, IMapper mapper, IUser userService) : base(repository, mapper, userService)
        {

        }
    }
}

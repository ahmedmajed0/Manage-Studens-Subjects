using AutoMapper;
using BL.Dtos;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.MappingProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<BaseTable, BaseTableDto>().ReverseMap();
            CreateMap<TbStudent, StudentDto>().ReverseMap();
            CreateMap<TbSubject, SubjectDto>().ReverseMap();
            CreateMap<VwStudenSubjects, VwStudentSubjectsDto>().ReverseMap();
        }
    }
}

using AutoMapper;
using BL.Dtos;
using BL.Interfaces;
using DAL.Interfaces;
using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    //This model has been created for the incremental functions related to this model only.
    public class StudentSubjectService : BaseService<TbStudentSubjects, StudentSubjectsDto>, IStudentSubjects
    {
        IGenericRepository<TbStudentSubjects> _repo;
        private readonly IMapper _mapper;
        public StudentSubjectService(IGenericRepository<TbStudentSubjects> repository, IMapper mapper, IUser userService) : base(repository, mapper, userService)
        {
            _repo = repository;
            _mapper = mapper;
        }


        // To add the subjects to student
        public bool AddSubjectsToStudent(StudentSubjectsDto dto)
        {
            var userId = "a10c8e1e60d5";
            var Subjects = new List<TbStudentSubjects>();
            foreach (var subject in dto.Subjects)
            {
                Subjects.Add(new TbStudentSubjects 
                {
                    StudentId = dto.Student.Id,
                    SubjectId = subject.Id ,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now,
                });
            }

            _repo.AddRange(Subjects);
            return true;
        }

        // To edit the subjects of students
        public bool EditStudentSubjects(StudentSubjectsDto dto)
        {
            // get old subjects for this student and remove them
            var oldSubjects = _repo.FindAll(a => a.StudentId == dto.Student.Id).ToList();
            // if > 0 thats mean updating student subjects ,
            // else means that the student don't have any subjects and i will add directly without delete any subject for this user

            // also we can use another way => we can compare about the old subjects and new subjects or removed  
            if (oldSubjects.Count > 0)
            {
                _repo.DeleteRange(oldSubjects);
                AddSubjectsToStudent(dto);
            }
            else
            {
                AddSubjectsToStudent(dto);
            }

            return true;
        }

    }
}

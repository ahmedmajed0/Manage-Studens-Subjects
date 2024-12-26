using AppResources.Messages;
using BL.Dtos;
using BL.Interfaces;
using BL.Services;
using Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.ApisControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentSubjectsController : ControllerBase
    {
        private readonly IStudentSubjects _studentSubjectsService;
        private readonly IVwStudentSubjects _vwStudentSubjects;
        private readonly IStudent _studentService;
        private readonly ISubject _subjectsService;
        public StudentSubjectsController(IStudentSubjects studentSubjectsService, IStudent studentService, ISubject subjectsService, IVwStudentSubjects vwStudentSubjects)
        {
            _studentSubjectsService = studentSubjectsService;
            _studentService = studentService;
            _subjectsService = subjectsService;
            _vwStudentSubjects = vwStudentSubjects;
        }
        // GET: api/<StudentsController>/GetAll
        [HttpGet("GetAll")]
        public ApiResponse GetAll()
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                var students = _studentService.GetAll();
                List<CurrentStudentSubjectsDto> llStudentsSubjectsDto = new List<CurrentStudentSubjectsDto>();

                foreach (var student in students)
                {
                    llStudentsSubjectsDto.Add(new CurrentStudentSubjectsDto
                    {
                        Student = _studentService.GetById(student.Id),
                        Subjects = _vwStudentSubjects.GetAll().Where(e => e.Email == student.Email).ToList(),
                    });
                }

                oApiResponse.Data = llStudentsSubjectsDto;
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = "200";
                return oApiResponse;

            }
            catch (Exception ex)
            {
                oApiResponse.Data = null;
                oApiResponse.Errors = ex.Message;
                oApiResponse.StatusCode = "502";
                return oApiResponse;
            }
        }

        // GET api/<StudentsController>/5
        [HttpGet("GetById")]
        public ApiResponse GetById([FromBody] Guid studentId)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                CurrentStudentSubjectsDto currentSubjectsDto = new CurrentStudentSubjectsDto();
                currentSubjectsDto.Student = _studentService.GetById(studentId);
                currentSubjectsDto.Subjects = _vwStudentSubjects.GetAll().Where(e => e.Email == currentSubjectsDto.Student.Email).ToList();

                oApiResponse.Data = currentSubjectsDto;
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = "200";
                return oApiResponse;

            }
            catch (Exception ex)
            {
                oApiResponse.Data = null;
                oApiResponse.Errors = ex.Message;
                oApiResponse.StatusCode = "502";
                return oApiResponse;
            }
        }



        // POST api/<StudentsController>/Save
        [HttpPost("Save")]
        public ApiResponse Save([FromBody] StudentSubjectsDto modelDto)
        {
            ApiResponse oApiResponse = new ApiResponse();

            try
            {

                if (modelDto.Student.Id == Guid.Empty || modelDto.Subjects == null || !modelDto.Subjects.Any())
                {
                    oApiResponse.Data = "Invalid request: StudentId and SubjectIds are required.";
                    oApiResponse.StatusCode = "400";
                    return oApiResponse;
                }


                // here i made only one function to add and Edit the student subjects
                _studentSubjectsService.EditStudentSubjects(modelDto);
                oApiResponse.Data = Messages.SaveSuccessMessage;
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = "200";
                return oApiResponse;
            }
            catch (Exception ex)
            {
                oApiResponse.Data = null;
                oApiResponse.Errors = ex.Message;
                oApiResponse.StatusCode = "502";
                return oApiResponse;
            }
        }



    }
}

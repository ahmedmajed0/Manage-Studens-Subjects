using AppResources.Messages;
using BL.Dtos;
using BL.Interfaces;
using Domains;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.ApisControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubject _subjectService;
        public SubjectsController(ISubject subjectService)
        {
            _subjectService = subjectService;
        }
        // GET: api/<StudentsController>/GetAll
        [HttpGet("GetAll")]
        public ApiResponse GetAll()
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oApiResponse.Data = _subjectService.GetAll();
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
        public ApiResponse GetById([FromBody] Guid id)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                var model = _subjectService.GetById(id);
                oApiResponse.Data = model== null ? "No Item with this id" : model;
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

        // POST api/<StudentsController>/Add
        [HttpPost("Add")]
        public ApiResponse Add([FromBody] SubjectDto modelDto)
        {
            ApiResponse oApiResponse = new ApiResponse();

            try
            {

                if (modelDto.Id != Guid.Empty)
                {
                    oApiResponse.Data = "Invalid request: ID should be empty for adding a new student.";
                    oApiResponse.StatusCode = "400";
                    return oApiResponse;
                }

                _subjectService.Add(modelDto);
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

        // PUT api/<StudentsController>/Edit
        [HttpPost("Edit")]
        public ApiResponse Edit([FromBody] SubjectDto modelDto)
        {
            ApiResponse oApiResponse = new ApiResponse();

            try
            {
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = "200";

                if (modelDto.Id == Guid.Empty)
                {
                    oApiResponse.Data = "Invalid request: ID cannot be empty for updating a student.";
                    oApiResponse.StatusCode = "400";
                    return oApiResponse;
                }

                var student = _subjectService.GetById(modelDto.Id);
                if (student == null)
                {
                    oApiResponse.Data = "Invalid request:No student with this ID available please make sure that this student already exists or not  ";
                    oApiResponse.StatusCode = "404";
                    return oApiResponse;
                }

                _subjectService.Update(modelDto);
                oApiResponse.Data = "Item updated successfully";
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

        [HttpPost("Delete")]
        public ApiResponse Delete([FromBody] Guid id)
        {
            try
            {
                ApiResponse oApiResponse = new ApiResponse();
                if (_subjectService.ChangeStatus(id, 0))
                {
                    oApiResponse.Data = Messages.DeleteSuccessMessage;
                    oApiResponse.Errors = null;
                    oApiResponse.StatusCode = "200";
                    return oApiResponse;
                }
                else
                {
                    oApiResponse.Data = Messages.DeleteFailedTitle;
                    oApiResponse.Errors = Messages.DeleteFailedMessage;
                    oApiResponse.StatusCode = "502";
                    return oApiResponse;

                }
            }
            catch (Exception ex)
            {
                ApiResponse oApiResponse = new ApiResponse
                {
                    Data = null,
                    Errors = ex.Message,
                    StatusCode = "502"
                };
                return oApiResponse;
            }
        }


        // POST api/<StudentsController>
        // [HttpPost("Save")] Add and Edit together in  one api
        //public ApiResponse Save([FromBody] StudentDto modelDto)
        //{
        //    ApiResponse oApiResponse = new ApiResponse();

        //    try
        //    {
        //        oApiResponse.Errors = null;
        //        oApiResponse.StatusCode = "200";
        //        if (modelDto.Id == Guid.Empty)
        //        {
        //            _studentService.Add(modelDto);
        //            oApiResponse.Data = Messages.SaveSuccessMessage;
        //            return oApiResponse;
        //        }
        //        else
        //        {
        //            _studentService.Update(modelDto);
        //            oApiResponse.Data = "Item updated successfully";
        //            return oApiResponse;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        oApiResponse.Data = null;
        //        oApiResponse.Errors = ex.Message;
        //        oApiResponse.StatusCode = "502";
        //        return oApiResponse;

        //    }

        //}

        // POST api/<StudentsController>





    }
}

using BL.Dtos;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    //This interface has been created for the incremental functions related to this model only.
    public interface IStudent : IBaseService<TbStudent, StudentDto>
    {
    }
}

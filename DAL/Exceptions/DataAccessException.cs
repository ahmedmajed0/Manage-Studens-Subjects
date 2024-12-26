using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class DataAccessException :Exception
    {
        public DataAccessException(Exception ex, string message, ILogger logger)
        {
            logger.LogError($"main exception is {ex.Message} the developer exception is {message}");
        }
    }
}

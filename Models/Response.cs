using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aYoUnitConversion.Models
{
    [Serializable]
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object ResponseData { get; set; }

        public Response(bool status, string message, object data)
        {
            IsSuccess = status;
            Message = message;
            ResponseData = data;
        }

        public Response()
        {
        }
    }
}

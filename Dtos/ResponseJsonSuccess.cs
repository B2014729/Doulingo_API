using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doulingo_Api.Dtos
{
    public class ResponseJsonSuccess<T> where T : class
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<T> Data { get; set; } = new List<T>();
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoAPI.Response
{
    public class EmployeeResponse
    {
        public string Status { get; set; }
        public List<Employee> Data { get; set; }

        public string Message { get; set; }
    }
}

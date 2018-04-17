using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TempDataDemo.Data;

namespace TempDataDemo.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<Person> People { get; set; }
        public string Message { get; set; }
    }
}
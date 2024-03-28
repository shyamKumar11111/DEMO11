using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DEMO11.Models
{
    public class Registration
    {
        public string name {  get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string Cpassowrd {  get; set; }
        public string phone { get; set; }
        public string adhar_No { get; set; }
      public HttpPostedFileBase pic { get; set; }
    }
}
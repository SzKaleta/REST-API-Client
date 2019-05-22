using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestApi.Models.ServerModels
{
    public class UserModel
    {
        [Required(ErrorMessage = "ID is required")]
        [Range(1, Int32.MaxValue, ErrorMessage = "ID is out of range")]
        public int user_id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        [Range(1, 120, ErrorMessage = "Age is out of range")]
        public int? age { get; set; }

        public string toString()
        {
            return user_id.ToString() + ") Name: " + name + ", age: " + age.ToString();
        }
    }
}
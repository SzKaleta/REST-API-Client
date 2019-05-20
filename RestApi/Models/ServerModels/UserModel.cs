using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestApi.Models.ServerModels
{
    public class UserModel
    {
        public int user_id { get; set; }
        public string name { get; set; }
        public Nullable<int> age { get; set; }
    }
}
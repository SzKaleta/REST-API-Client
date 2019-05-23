using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestApi.Models.Error
{
    public class ErrorModel
    {
        public string errorType { get; set; }

        public ErrorModel(string newType)
        {
            errorType = newType;
        }
    }
}
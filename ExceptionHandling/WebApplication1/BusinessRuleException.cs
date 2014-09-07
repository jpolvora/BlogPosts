using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string msg)
            : base(msg)
        {

        }
    }
}
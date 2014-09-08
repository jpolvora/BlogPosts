using System;
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
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCommunity.Common.Exceptions
{
    public class MyCommunityExceptions:Exception
    {
        public string Code { get; }
        public MyCommunityExceptions()
        {

        }
        public MyCommunityExceptions(string code)
        {
            Code = code;
        }
        public MyCommunityExceptions(string message, params object[] args):this(string.Empty,message,args)
        {
            
        }
        public MyCommunityExceptions(string code,string message, params object[] args) : this(null, code, message, args)
        {
            
        }
        public MyCommunityExceptions(Exception innerException, string message, params object[] args) : this(innerException,string.Empty, message, args)
        {

        }
        public MyCommunityExceptions(Exception innerException, string code, string message, params object[] args) : base(string.Format(message,args), innerException)
        {
            Code = code;
        }
    }
}

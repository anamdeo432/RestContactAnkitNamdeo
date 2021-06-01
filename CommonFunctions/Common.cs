using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace CommonFunctions
{
    public class Common
    {
        public bool ValidateRequestHeader(HttpRequestMessage Request)
        {

            if (Request.Headers.Contains("UserId"))
            {
                if (Request.Headers.GetValues("UserId").FirstOrDefault() == "ankituser")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }


        }
    }
}

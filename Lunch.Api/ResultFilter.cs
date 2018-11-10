using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Lunch.Data;

namespace Lunch.Data
{
    public class ApiResultFilter : ResultFilter
    {
        [FromQuery(Name = "limit")]
        public int Limit_
        {
            set
            {
                Limit = value;
            }
        }

        [FromQuery(Name = "offset")]
        public int Offset_ 
        {
            set
            {
                Offset = value;
            }
        }
    }
}
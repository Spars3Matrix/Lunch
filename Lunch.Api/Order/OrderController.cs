using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lunch.Data;
using Lunch.Menu;
using Lunch.Order;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lunch.Slack
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet("{person}")]
        public IActionResult GetRange(string person, [FromQuery] ApiResultFilter filter)
        {
            return new JsonResult(new OrderService().GetItems(person, filter));
        }

        [HttpPost("{description}/{amount}")]
        public IActionResult Order(string description, int amount) 
        {
            return new JsonResult(new OrderService().Order(description, "Test Person", amount));
        }

        [HttpPut("add/{description}")]
        public IActionResult IncrementOrder(string description) 
        {
            return new JsonResult(new OrderService().IncrementOrder(description, "Test Person"));
        }

        [HttpPut("remove/{description}")]
        public IActionResult DecrementOrder(string description) 
        {
            return new JsonResult(new OrderService().DecrementOrder(description, "Test Person"));
        }

        [HttpDelete("{description}")]
        public void Cancel(string description) 
        {
            new OrderService().Cancel(description, "Test Person");
        }
    }
} 

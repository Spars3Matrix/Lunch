using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lunch.Slack
{
    [Route("api/slack")]
    [ApiController]
    public class SlackController : ControllerBase
    {
        [HttpPost("order")]
        public Message Order([FromForm] Payload payload)
        {
            return new Message($"You've ordered a '{payload.Text}'.");
        }

        [HttpPost("list-order")]
        public Message ListOrder([FromForm] Payload payload)
        {
            return new Message($"List the complete order.");
        }
    }
} 

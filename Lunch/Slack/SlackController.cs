using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lunch.Menu;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lunch.Slack
{
    [Route("api/slack")]
    [ApiController]
    public class SlackController : ControllerBase
    {
        private readonly IMenuProvider MenuProvider;

        public SlackController(IMenuProvider menuProvider)
        { 
            MenuProvider = menuProvider;
        }

        [HttpPost("order")]
        public Message Order([FromForm] Payload payload)
        {
            string message = "Could not find the desired product.";
            MenuItem item = MenuProvider.Menu.GetItem(payload.Text.Trim());

            if (item != null)
            {
                message = $"You've ordered a '{item.Description}' for {item.Price} euro.";
            }

            return new Message(message);
        }

        [HttpPost("list-order")]
        public Message ListOrder([FromForm] Payload payload)
        {
            return new Message($"List the complete order.");
        }
    }
} 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Lunch.Commands;
using Lunch.Menu;
using Lunch.Order;
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
            OrderResult result = new CommandHandler().Order(payload.UserName, payload.Text);
            return new Message(result.Successful ?
                $"You've ordered a '{result.OrderItem.Description}'." :
                $"Could not order this item. ({result.Exception.ToString()})");
        }

        [HttpPost("list-order")]
        public Message ListOrder([FromForm] Payload payload)
        {
            IEnumerable<OrderItem> items = new CommandHandler().ListOrder(payload.UserName, payload.Text);

            Message message = new Message();
            Attachment attachment = new Attachment("Order", string.Join("\n", items.Select(i => $"{i.Amount} x {i.Description} = {i.Total}")));
            attachment.AddField("Total", items.Sum(i => i.Total).ToString());
            message.AddAttachment(attachment);

            return message;
        }

        [HttpPost("menu")]
        public Message ListMenu([FromForm] Payload payload)
        {
            return new Message(string.Join(", ", new CommandHandler().ListMenu(payload.UserName, payload.Text).Select(i => $"{i.Description}: {i.Price} euro")));
        }
    }
} 

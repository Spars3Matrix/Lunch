using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
            OrderResult result = Order(payload.Text, payload.UserName);
            return new Message(result.Successful ?
                $"You've ordered a '{result.OrderItem.Description}'." :
                $"Could not order this item. ({result.Exception.ToString()})");
        }

        [HttpPost("list-order")]
        public Message ListOrder([FromForm] Payload payload)
        {
            OrderService service = new OrderService();
            IEnumerable<OrderItem> items = payload.Text != null && payload.Text.Trim().ToLower().StartsWith("all") ?
                service.GetItems(DateTime.MinValue, DateTime.MaxValue) :
                service.GetItems(payload.UserName);

            return new Message(string.Join(", ", items.Select(i => $"{i.Amount} x {i.Description} a {i.Price} = {i.Total} ({i.Person})")));
        }

        [HttpPost("menu")]
        public Message ListMenu([FromForm] Payload payload)
        {
            return new Message(string.Join(", ", new MenuService().Menu.GetItems().Select(i => $"{i.Description}: {i.Price} euro")));
        }

        private OrderResult Order(string text, string person)
        {
            text = text.Trim();

            string amount = "";
            foreach (char c in text)
            {
                if (char.IsDigit(c)) amount += c;
                else break;
            }
            string description = text.Substring(amount.Length).Trim();

            OrderService service = new OrderService();
            return amount.Length > 0 ?
                service.Order(description, person, int.Parse(amount)) :
                service.IncrementOrder(description, person);
        }
    }
} 

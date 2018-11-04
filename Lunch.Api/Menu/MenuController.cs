using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lunch.Data;
using Lunch.Menu;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lunch.Slack
{
    [Route("api/menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMenu()
        {
            return new JsonResult(new MenuService().Menu);
        }
        
        [HttpGet("items")]
        public IActionResult GetMenuItems([FromQuery] ApiResultFilter filter)
        {
            return new JsonResult(new MenuService().GetItems(filter));
        }
    }
} 

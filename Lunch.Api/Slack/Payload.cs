using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lunch.Slack
{
    public class Payload
    {
        [BindProperty(Name = "token")]
        public string Token { get; set; }

        [BindProperty(Name = "team_id")]
        public string TeamId { get; set; }

        [BindProperty(Name = "team_domain")]
        public string TeanDomain { get; set; }
        
        [BindProperty(Name = "enterprise_id")]
        public string EnterpriseId { get; set; }

        [BindProperty(Name = "enterprise_name")]
        public string EnterpriseName { get; set; }

        [BindProperty(Name = "channel_id")]
        public string ChannelId { get; set; }

        [BindProperty(Name = "channel_name")]
        public string ChannelName { get; set; }

        [BindProperty(Name = "user_id")]
        public string UserId { get; set; }

        [BindProperty(Name = "user_name")]
        public string UserName { get; set; }

        [BindProperty(Name = "command")]
        public string Command { get; set; }

        [BindProperty(Name = "text")]
        public string Text { get; set; }

        [BindProperty(Name = "response_url")]
        public string ResponseUrl { get; set; }

        [BindProperty(Name = "trigger_id")]
        public string TriggerId { get; set; }
    }
}
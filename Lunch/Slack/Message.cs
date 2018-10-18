using Newtonsoft.Json;

namespace Lunch.Slack
{
    public class Message
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        public Message() {}

        public Message(string text)
        {
            Text = text;
        }
    }
}
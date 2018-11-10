using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lunch.Slack
{
    public class Message
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("attachments")]
        public IList<Attachment> Attachments { get; set; } = new List<Attachment>();

        public Message() {}

        public Message(string text)
        {
            Text = text;
        }

        public void AddAttachment(string title, string text) 
        {
            AddAttachment(new Attachment(title, text));
        }

        public void AddAttachment(Attachment attachment) 
        {
            Attachments.Add(attachment);
        }
    }
}
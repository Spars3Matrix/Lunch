using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lunch.Slack
{
    public class Attachment
    {
        [JsonProperty("title")]
        public string Title { get => Fallback; set => Fallback = value; }

        [JsonProperty("color")]
        public string Color { get; set; } = "#e91e63";

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("fallback")]
        public string Fallback { get; private set; }

        [JsonProperty("fields")]
        public IList<Field> Fields { get; set; } = new List<Field>();

        public Attachment() {}

        public Attachment(string title, string text)
        {
            Title = title;
            Text = text;
        }

        public void AddField(string title, string value)
        {
            Fields.Add(new Field(title, value));
        }
    }
}
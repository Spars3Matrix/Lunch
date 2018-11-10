using Newtonsoft.Json;

namespace Lunch.Slack
{
    public class Field
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("value")]
        public string Value { get; private set; }

        public Field() {}

        public Field(string title, string value)
        {
            Title = title;
            Value = value;
        }
    }
}
namespace Sample.Identity.Infra.Services.Zenvia.Models
{
    internal class ZenviaSmsBody
    {
        public ZenviaSmsBody(string from, string to, string message)
        {
            From = from;
            To = to.Replace("+", "");
            Contents = new() { new Content("text", message) };
        }

        public string From { get; set; }
        public string To { get; set; }
        public List<Content> Contents { get; set; }

        public class Content
        {
            public Content(string type, string text)
            {
                Type = type;
                Text = text;
            }

            public string Type { get; set; }
            public string Text { get; set; }
        }
    }
}
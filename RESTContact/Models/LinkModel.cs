namespace RESTContact.Models
{
    public class LinkModel
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }
        public string Prev { get; set; }

        public string Next { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriThucOnline_TTN.Models
{
    public class Book
    {
        public int id { get; set; }
        public string title { get; set; }
        public float price { get; set; }
        public int publish_year { get; set; }
        public string picture { get; set; }
        public int page_number { get; set; }
        public int quantity { get; set; }
        public string quotes_about { get; set; }
        public int author_id { get; set; }
        public int publisher_id { get; set; }
        public int category_id { get; set; }
        public string publisher_name { get; set; }
        public string category_name { get; set; }
        public string author_name { get; set; }
    }

    public class Books
    {
        public List<Book> books { get; set; }
    }
    public class Publisher
    {
        public string name { get; set; }
        public int id { get; set; }
        public Book books { get; set; }
    }

    public class Publishers
    {
        public List<Publisher> publishers { get; set; }
    }
}
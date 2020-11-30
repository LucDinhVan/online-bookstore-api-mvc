using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriThucOnline_TTN.Models
{
    public class Book
    {
        public int id {get; set;}
        public string title {get; set;}
        public float price {get; set;}
        public int? publish_year {get; set;}
        public string picture {get; set;}
        public int? page_number {get; set;}
        public int? quantity {get; set;}
        public string quotes_about {get; set;}
        public int author_id {get; set;}
        public int publisher_id {get; set;}
        public int category_id {get; set;}
        public string publisher_name {get; set;}
        public string category_name {get; set;}
        public string author_name {get; set;}
    }

    public class Books
    {
        public bool has_next { get; set; }
        public bool has_previous { get; set; }
        public int page { get; set;}
        public int pages { get; set;}
        public List<Book> items { get; set; }
    }

    public class Category
    {
        public string name { get; set; }
        public int id { get; set;}
        public List<Book> books {get; set;}
    }

    public class Categories
    {
        public List<Category> categories { get; set; }
    }

    public class Author
    {
        public string name { get; set; }
        public string picture { get; set; }
        public string info { get; set; }
        public int id { get; set;}
        public List<Book> books {get; set;}
    }

    public class Authors
    {
        public List<Author> authors { get; set; }
    }

    public class Publisher
    {
        public string name { get; set; }
        public int id { get; set;}
        public List<Book> books {get; set;}
    }

    public class Publishers
    {
        public List<Publisher> publishers { get; set; }
    }

    public class User
    {
        public string username { get; set;}
        public string password { get; set;}
        public string address { get; set;}
        public string phone { get; set;}
        public string email { get; set;}
        public string picture { get; set;}
        public int id { get; set;}
        public string name { get; set; }
    }

    public class Users
    {
        public List<User> users { get; set; }
    }

    public class Admin
    {
        public string username { get; set;}
        public string password { get; set;}
        public int id { get; set;}
        public string name { get; set; }
    }

    public class Admins
    {
        public List<Admin> admins { get; set; }
    }

    public class Coupon
    {
        public int id { get; set;}
        public string code { get; set;}
        public string description { get; set;}
        public float discount { get; set;}
        public float max_value { get; set;}
        public int count { get; set;}
        public float valid_from { get; set;}
        public float valid_until { get; set;}
        public bool is_enable { get; set; }
    }

    public class Coupons
    {
        public List<Coupon> coupons { get; set; }
    }

    public class Order
    {
        public int id { get; set;}
        public float order_date { get; set;}
        public float required_date { get; set;}
        public float shipped_date { get; set;}
        public string status { get; set;}
        public string comment { get; set;}
        public float total { get; set;}
        public int user_id { get; set;}
        public string coupon_code { get; set; }
        public OrderDetails order_details { get; set; }
        
    }

    public class Orders
    {
        public List<Order> orders { get; set; }
    }

    public class OrderDetail
    {
        public int id { get; set;}
        public int quantity { get; set;}
        public float price { get; set;}
        public int order_id { get; set;}
        public int book_id { get; set;}        
    }

    public class OrderDetails
    {
        public List<OrderDetail> order_details { get; set; }
    }
}
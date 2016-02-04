using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStoreDemo1.Models;

namespace BookStoreDemo1.Controllers
{
    public class HomeController : Controller
    {
        Store store = new Store();

        public HomeController() 
        {
            Stack stack1 = new Stack {Location="A1" };
            Book mobyDick = new Book{Title="Moby Dick", Author="Herman Melville", Price=5.00M , ISBN="9780746062760"};
            Book readyPlayerOne = new Book{Title="Ready Player One", Author="Ernest Cline", Price=10.00M, ISBN="9781446493830"};
            stack1.Books.Add(mobyDick);
            stack1.Books.Add(readyPlayerOne);
            store.Stacks.Add(stack1);
        }
        //
        // GET: /Store/
        public ActionResult Index()
        {
            ViewBag.Heading = "Welcome to our bookstore";
            return View();
        }

        public ActionResult Inventory()
        {
            List<Book> books = store.Stacks[0].Books;
            //string bookTitle = "";
            //foreach(Book b in store.Stacks[0].Books)
            //{
            //    bookTitle += b.Title + "<br />";

            //}

            return View(books);
        }
	}
}
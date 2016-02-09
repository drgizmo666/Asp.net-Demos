using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStoreDemo1.Models;

namespace BookStoreDemo1.Controllers
{
    public class BookCrudController : Controller
    {
        private BookStoreDbContext db = new BookStoreDbContext();

        // GET: /BookCrud/
        public ActionResult Index()
        {
            

            //Old Code
            //foreach(Book b in db.Books)
            //{
            //    var bookVm = new BookViewModel();
            //    bookVm.Author = b.Author;
            //    bookVm.BookId = b.BookId;
            //    bookVm.Title = b.Title;
            //    bookVm.Price = b.Price;
            //    bookVm.ISBN = b.ISBN;
            //    //foreach(Book bs in db.Stacks.ToList()[0].Books)
            //    bookVm.Location = db.Stacks.ToList()[0].Location;
            //    books.Add(bookVm);
            //}


            return View(GetStacksAndBooks(0));
        }

        // GET: /BookCrud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //BookViewModel book = GetStacksAndBooks(id).FirstOrDefault();
            BookViewModel book = GetStackAndBook(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: /BookCrud/Create
        public ActionResult Create()
        {
            ViewBag.StackLocation = new SelectList(db.Stacks.OrderBy(s => s.Location), "StackID", "Location");
            return View();
        }

        // POST: /BookCrud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="BookId, Title, Author, Price, ISBN, Location, StackLocation")] BookViewModel bookVM, int StackLocation)
        {

            if (ModelState.IsValid)
            {
                Stack stack = (from s in db.Stacks
                              where s.StackId == StackLocation
                                  select s).FirstOrDefault();
                if(stack == null)
                {
                    stack = new Stack() { Location = bookVM.Location };
                    db.Stacks.Add(stack);
                }

                Book book = new Book()
                {
                    Author = bookVM.Author,
                    BookID = bookVM.BookId,
                    ISBN = bookVM.ISBN,
                    Price = bookVM.Price,
                    Title = bookVM.Title,
                    StackID = stack.StackId
                };
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StackLocation = new SelectList(db.Stacks.OrderBy(s => s.Location), "StackID", "Location");
            return View(bookVM);
        }

        // GET: /BookCrud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: /BookCrud/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="BookId,Title,Author,Price,ISBN")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: /BookCrud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: /BookCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(String searchTerm)
        {
            //TODO: Get a List of book view models
            List<BookViewModel> bookVMs = new List<BookViewModel>();
            //TODO: Get the books that matches the search term
            var books = (from b in db.Books
                               where b.Title.Contains(searchTerm)
                               select b).ToList<Book>();
            //In a Loop:
                
                
            foreach(Book b in books)
            {
                //TODO: Get the Stack that contains each book
                var stack = (from s in db.Stacks
                             where s.StackId == b.StackID
                             select s).FirstOrDefault();
                //TODO: Create View models for the book and put them in the list
                bookVMs.Add(new BookViewModel()
                {
                    Title = b.Title,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    Price = b.Price,
                    Location = stack.Location
                });
            }
            /*
            bookVMs = from b in db.Books
                      join s in db.Stacks on b.StackID equals s.StackId
                      where b.Title.Contains(searchTerm)
                      select new List<BookViewModel> 
                      {
                          Title = b.Title, 
                          Author = b.Author, 
                          ISBN = b.ISBN, 
                          Price = b.Price, 
                          Location = s.Location
                      };
             */

            //TODO: if there is just one book, display it
            if (bookVMs.Count == 1)
            {
                return View("Details", bookVMs[0]);
            }
            //TODO: if there is more than one book display the list of books
            else 
            {
                return View("Index", bookVMs);
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Pass zero for all books or an id for one book
        private List<BookViewModel> GetStacksAndBooks(int? bookId)
        {
            List<BookViewModel> books = new List<BookViewModel>();
                var stacks = from stack in db.Stacks.Include("Books")
                             select stack;

            foreach (Stack s in stacks)
            {
                foreach (Book b in s.Books)
                {
                    if (b.BookID == bookId || 0 == bookId)
                    {
                        var bookVm = new BookViewModel();
                        bookVm.Author = b.Author;
                        bookVm.BookId = b.BookID;
                        bookVm.Title = b.Title;
                        bookVm.Price = b.Price;
                        bookVm.ISBN = b.ISBN;
                        bookVm.Location = s.Location;
                        books.Add(bookVm);
                    }

                }
            }

            return books;
        }

        private BookViewModel GetStackAndBook(int? BookId)
        {
            BookViewModel bookVM = (from b in db.Books
                                    join s in db.Stacks on b.StackID equals s.StackId
                                    where b.BookID == BookId
                                    select new BookViewModel {Title = b.Title, 
                                    Author = b.Author, ISBN = b.ISBN, Price = b.Price, 
                                    Location = s.Location}).FirstOrDefault();
            return bookVM;
        }
    }

}

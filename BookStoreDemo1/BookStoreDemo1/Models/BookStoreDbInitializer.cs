using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace BookStoreDemo1.Models
{
    public class BookStoreDbInitializer : DropCreateDatabaseIfModelChanges<BookStoreDbContext>
    {
        protected override void Seed(BookStoreDbContext context)
        {
            //Stack 1
            Stack stack1 = new Stack { Location = "Bloody Kush" };
            Book mobyDick = new Book { Title = "Fahrenheit 451", Author = "Ray Bradbury", Price = 9.78M, ISBN = "9781451673319" };
            Book readyPlayerOne = new Book { Title = "Ready Player One", Author = "Ernest Cline", Price = 10.00M, ISBN = "9781446493830" };
            stack1.Books.Add(mobyDick);
            stack1.Books.Add(readyPlayerOne);
            context.Stacks.Add(stack1);
            //store.Stacks.Add(stack1);

            //Stack 2
            Stack stack2 = new Stack { Location = "9 Pound Hammer" };
            Book DeadPoolKMU = new Book { Title = "Deadpool Kills the Marvel Universe", Author = "Cullen Bunn, Dalibor Talajic", Price = 14.00M, ISBN = "9780785164036" };
            Book Watchmen = new Book { Title = "Watchmen", Author = "Alan Moore, Dave Gibbons", Price = 13.00M, ISBN = "9781401245252" };
            stack2.Books.Add(DeadPoolKMU);
            stack2.Books.Add(Watchmen);
            context.Stacks.Add(stack2);

            base.Seed(context);
        }
    }
}
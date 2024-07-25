using LibraryManagementSystem.Backend.Contexts;
using LibraryManagementSystem.Backend.Enums;
using LibraryManagementSystem.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Backend.Utils
{

    /*
     * The purpose of this class is for adding a set of books for testing and display purposes.
     * If books already exist in the database, then we won't add any, otherwise, we will add a 
     * list of predefined books
     * **/
    public static class DatabaseInitializerUtil
    {
        public static async Task InitializeAsync(LibraryContext context)
        {
            // return if books already exist
            if (context.Books.Any())
                return;

            List<Book> books = GetInitialBooks();
            foreach (Book book in books)
            {
                // check if the book exists in the database
                var existingBook = await context.Books
                                                .Where(b => b.ISBN == book.ISBN)
                                                .FirstOrDefaultAsync();

                // add it if it doesn't
                if (existingBook == null)
                {
                    book.DeweyDecimalNumber = DeweyDecimalNumberGeneratorUtil.GenerateDeweyDecimalNumber(book);
                    context.Books.Add(book);
                }
            }

            await context.SaveChangesAsync();
        }

        private static List<Book> GetInitialBooks() 
        {
            return new List<Book>
            {
                new Book
                {
                    Title = "The Selfish Gene",
                    Description = "A book on evolution by Richard Dawkins.",
                    Author = "Richard Dawkins",
                    ISBN = "9780199291141",
                    Genre = Genre.Science,
                    Quantity = 10,
                    PictureUrl = "https://covers.openlibrary.org/b/id/8244084-L.jpg"
                },
                new Book
                {
                    Title = "Pride and Prejudice",
                    Description = "A classic novel by Jane Austen.",
                    Author = "Jane Austen",
                    ISBN = "9780141439518",
                    Genre = Genre.Literature,
                    Quantity = 8,
                    PictureUrl = "https://covers.openlibrary.org/b/id/8049340-L.jpg"
                },
                new Book
                {
                    Title = "Guns, Germs, and Steel",
                    Description = "A historical analysis by Jared Diamond.",
                    Author = "Jared Diamond",
                    ISBN = "9780393336746",
                    Genre = Genre.History,
                    Quantity = 12,
                    PictureUrl = "https://covers.openlibrary.org/b/id/7224874-L.jpg"
                },
                new Book
                {
                    Title = "The Code Book",
                    Description = "A book on cryptography by Simon Singh.",
                    Author = "Simon Singh",
                    ISBN = "9780385495325",
                    Genre = Genre.Technology,
                    Quantity = 5,
                    PictureUrl = "https://covers.openlibrary.org/b/id/7806803-L.jpg"
                },
                new Book
                {
                    Title = "The Metamorphosis",
                    Description = "A novella by Franz Kafka.",
                    Author = "Franz Kafka",
                    ISBN = "9780486281004",
                    Genre = Genre.Art,
                    Quantity = 7,
                    PictureUrl = "https://covers.openlibrary.org/b/id/7877068-L.jpg"
                },
                new Book
                {
                    Title = "The Story of Music",
                    Description = "A comprehensive guide to music history by Howard Goodall.",
                    Author = "Howard Goodall",
                    ISBN = "9780198727998",
                    Genre = Genre.Music,
                    Quantity = 6,
                    PictureUrl = "https://covers.openlibrary.org/b/id/8239721-L.jpg"
                },
                new Book
                {
                    Title = "Beyond Good and Evil",
                    Description = "A philosophical work by Friedrich Nietzsche.",
                    Author = "Friedrich Nietzsche",
                    ISBN = "9780140449235",
                    Genre = Genre.Philosophy,
                    Quantity = 9,
                    PictureUrl = "https://covers.openlibrary.org/b/id/8232231-L.jpg"
                },
                new Book
                {
                    Title = "The Quran",
                    Description = "The holy book of Islam.",
                    Author = "Various Authors",
                    ISBN = "9781904063054",
                    Genre = Genre.Religion,
                    Quantity = 15,
                    PictureUrl = "https://covers.openlibrary.org/b/id/7896282-L.jpg"
                },
                new Book
                {
                    Title = "The Tipping Point",
                    Description = "A book about social behavior by Malcolm Gladwell.",
                    Author = "Malcolm Gladwell",
                    ISBN = "9780316346627",
                    Genre = Genre.SocialScience,
                    Quantity = 11,
                    PictureUrl = "https://covers.openlibrary.org/b/id/7301786-L.jpg"
                },
                new Book
                {
                    Title = "The Elements of Style",
                    Description = "A classic guide to writing by William Strunk Jr. and E.B. White.",
                    Author = "William Strunk Jr. and E.B. White",
                    ISBN = "9780205309023",
                    Genre = Genre.Language,
                    Quantity = 10,
                    PictureUrl = "https://covers.openlibrary.org/b/id/7436704-L.jpg"
                },
                new Book
                {
                    Title = "Gödel, Escher, Bach",
                    Description = "A book on mathematics and cognition by Douglas Hofstadter.",
                    Author = "Douglas Hofstadter",
                    ISBN = "9780465026562",
                    Genre = Genre.Mathematics,
                    Quantity = 8,
                    PictureUrl = "https://covers.openlibrary.org/b/id/7567816-L.jpg"
                },
                new Book
                {
                    Title = "The Emperor of All Maladies",
                    Description = "A biography of cancer by Siddhartha Mukherjee.",
                    Author = "Siddhartha Mukherjee",
                    ISBN = "9781439107959",
                    Genre = Genre.Medicine,
                    Quantity = 7,
                    PictureUrl = "https://covers.openlibrary.org/b/id/7973516-L.jpg"
                },
                new Book
                {
                    Title = "Just Mercy",
                    Description = "A memoir by Bryan Stevenson about his work as a lawyer.",
                    Author = "Bryan Stevenson",
                    ISBN = "9780812984965",
                    Genre = Genre.Law,
                    Quantity = 6,
                    PictureUrl = "https://covers.openlibrary.org/b/id/7820864-L.jpg"
                },
                new Book
                {
                    Title = "Pedagogy of the Oppressed",
                    Description = "A book on education by Paulo Freire.",
                    Author = "Paulo Freire",
                    ISBN = "9780826412768",
                    Genre = Genre.Education,
                    Quantity = 5,
                    PictureUrl = "https://covers.openlibrary.org/b/id/7319228-L.jpg"
                },
                new Book
                {
                    Title = "Thinking, Fast and Slow",
                    Description = "A book on psychology by Daniel Kahneman.",
                    Author = "Daniel Kahneman",
                    ISBN = "9780374533557",
                    Genre = Genre.Psychology,
                    Quantity = 12,
                    PictureUrl = "https://covers.openlibrary.org/b/id/7636816-L.jpg"
                },
                new Book
                {
                    Title = "To Kill a Mockingbird",
                    Description = "A novel by Harper Lee.",
                    Author = "Harper Lee",
                    ISBN = "9780061120084",
                    Genre = Genre.Fiction,
                    Quantity = 9,
                    PictureUrl = "https://covers.openlibrary.org/b/id/8220625-L.jpg"
                },
                new Book
                {
                    Title = "The Sun and Her Flowers",
                    Description = "A collection of poems by Rupi Kaur.",
                    Author = "Rupi Kaur",
                    ISBN = "9781449486792",
                    Genre = Genre.Poetry,
                    Quantity = 11,
                    PictureUrl = "https://covers.openlibrary.org/b/id/8503095-L.jpg"
                },
                new Book
                {
                    Title = "Hamlet",
                    Description = "A tragedy by William Shakespeare.",
                    Author = "William Shakespeare",
                    ISBN = "9780743477123",
                    Genre = Genre.Drama,
                    Quantity = 6,
                    PictureUrl = "https://covers.openlibrary.org/b/id/8197882-L.jpg"
                },
                new Book
                {
                    Title = "The Geography of Bliss",
                    Description = "A travel memoir by Eric Weiner.",
                    Author = "Eric Weiner",
                    ISBN = "9780446554796",
                    Genre = Genre.Travel,
                    Quantity = 7,
                    PictureUrl = "https://covers.openlibrary.org/b/id/7565201-L.jpg"
                },
                new Book
                {
                    Title = "Steve Jobs",
                    Description = "A biography by Walter Isaacson.",
                    Author = "Walter Isaacson",
                    ISBN = "9781451648539",
                    Genre = Genre.Biography,
                    Quantity = 8,
                    PictureUrl = "https://covers.openlibrary.org/b/id/8194082-L.jpg"
                },
                new Book
                {
                    Title = "The Lean Startup",
                    Description = "A guide to business innovation by Eric Ries.",
                    Author = "Eric Ries",
                    ISBN = "9780307887894",
                    Genre = Genre.Business,
                    Quantity = 10,
                    PictureUrl = "https://covers.openlibrary.org/b/id/7603491-L.jpg"
                },
                new Book
                {
                    Title = "Atomic Habits",
                    Description = "A book on self-help by James Clear.",
                    Author = "James Clear",
                    ISBN = "9780735211292",
                    Genre = Genre.SelfHelp,
                    Quantity = 5,
                    PictureUrl = "https://covers.openlibrary.org/b/id/8260451-L.jpg"
                },
                new Book
                {
                    Title = "How Not to Die",
                    Description = "A book on health by Michael Greger.",
                    Author = "Michael Greger",
                    ISBN = "9781250066118",
                    Genre = Genre.Health,
                    Quantity = 7,
                    PictureUrl = "https://covers.openlibrary.org/b/id/7889397-L.jpg"
                },
                new Book
                {
                    Title = "Dune",
                    Description = "A science fiction novel by Frank Herbert.",
                    Author = "Frank Herbert",
                    ISBN = "9780441013593",
                    Genre = Genre.ScienceFiction,
                    Quantity = 9,
                    PictureUrl = "https://covers.openlibrary.org/b/id/8236314-L.jpg"
                },
                new Book
                {
                    Title = "The Girl with the Dragon Tattoo",
                    Description = "A mystery novel by Stieg Larsson.",
                    Author = "Stieg Larsson",
                    ISBN = "9780307454546",
                    Genre = Genre.Mystery,
                    Quantity = 10,
                    PictureUrl = "https://covers.openlibrary.org/b/id/7519327-L.jpg"
                }
            };
        }
    }
}

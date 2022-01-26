using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain;
public class Author : BaseEntity
{
    private Author() : base() { }

    // access modifier set to internal as currently you can only create an author via a book domain, though in future we may need to support directly creating author
    internal Author(string name) : base()
    {
        Name = name;

        _books = new HashSet<Book>();
    }

    internal Author(string name, Book book) : base()
    {
        Name = name;

        _books = new HashSet<Book> { book };
    }

    public string Name { get; private set; }


    // Navigation properties
    private HashSet<Book> _books;
    public IReadOnlyCollection<Book> Books => _books;


    // Domain methods (given as examples of how Domain Driven Design would be implemented)
    public void AddNewBook(string category, string title, decimal price)
    {
        if (_books.Any(x => x.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase)))
            throw new InvalidOperationException($"Book with title: {title} already exists for this author {Name}");

        var book = new Book(category, title, price, this);

        _books.Add(book);
    }

    public void AddBook(Book book)
    {
        _books.Add(book);
    }
}

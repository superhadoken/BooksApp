using System.Collections.Generic;
using System.Linq;

namespace Domain;

public class Book : BaseEntity
{
    private Book() : base() { }

    public Book(string category, string title, decimal price, IEnumerable<string> authors) : base()
    {
        Category = category;
        Title = title;
        Price = price;

        _authors = authors.Select(x => new Author(x)).ToHashSet();
    }

    public Book(string category, string title, decimal price, Author author) : base()
    {
        Category = category;
        Title = title;
        Price = price;

        _authors = new HashSet<Author> { author };
    }

    // Properties
    public string Category { get; private set; } //TODO use enum in future for category
    public string Title { get; private set; }
    public decimal Price { get; private set; }

    // Navigation property
    private HashSet<Author> _authors;
    public IReadOnlyCollection<Author> Authors => _authors;


    // Domain Methods (used more so in a more complex app, below is for demonstration purposes)
    public void AddAuthor(Author author)
    {
        _authors.Add(author);
    }

    public void AddNewAuthor(string name)
    {
        // No check for duplicate against current authors as we could potentially have different authors with the same name
        var author = new Author(name, this);

        _authors.Add(author);
    }
}

using System.Collections.Generic;
using Domain;

namespace DataAccess;

public interface IWriteBooks
{
    void SaveBooks(ICollection<Book> books);
}
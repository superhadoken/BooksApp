using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess;

// normally would use Entity Framework as ORM with UnitOfWork to enable transactions/atomicity 
// in the case of smaller applications I'd use a light-weight micro ORM like dapper or PetaPoco to abstract away things like
// connection management, parameterisation and protect against injection attacks. 
public class BookWriter : IWriteBooks
{
    //todo don't use magic string and get from config // appsettings.json
    private const string ConnectionString = "Data Source=.;Initial Catalog=BooksApp;Integrated Security=True";

    public void SaveBooks(ICollection<Book> books)
    {
        // save books to the database
        using var connection = new SqlConnection(ConnectionString);

        connection.Open();

        using var command = connection.CreateCommand();

        // no need for using statement with this as we assign the transaction to command.Transaction and use its dispose
        var transaction = connection.BeginTransaction();

        // Use transaction for Atomicity - we want to persist the whole request at once or none of it if part fails
        command.Transaction = transaction;
        command.Connection = connection;

        try
        {
            var bookIndex = 0;
            var authorIndex = 0;

            foreach (var book in books)
            {
                InsertBook(command, book, bookIndex);
                
                bookIndex++;

                foreach (var author in book.Authors)
                {
                    InsertAuthor(command, author, book.Id, authorIndex);
                    
                    authorIndex++;
                }
            }

            transaction.Commit();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
            Console.WriteLine("  Message: {0}", ex.Message);
            Console.WriteLine("  Stack Trace: {0}", ex.StackTrace);

            // Rollback the transaction to return to our original state
            try
            {
                transaction.Rollback();
            }
            catch (Exception rollbackException)
            {
                // This catch block will handle any errors that may have occurred
                // on the server that would cause the rollback to fail, such as
                // a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", rollbackException.GetType());
                Console.WriteLine("  Message: {0}", rollbackException.Message);
                Console.WriteLine("  Stack Trace: {0}", rollbackException.StackTrace);
            }
        }
    }

    private void InsertBook(SqlCommand command, Book book, int index)
    {
        // build sql or call sp to insert book
        // Create book
        command.CommandType = CommandType.Text;
        command.CommandText = $"INSERT INTO [dbo].[Books] (Id, Category, Title, Price) VALUES (@Id{index}, @Category{index}, @Title{index}, @Price{index})";
        command.Parameters.AddWithValue($"@Id{index}", book.Id);
        command.Parameters.AddWithValue($"@Category{index}", book.Category);
        command.Parameters.AddWithValue($"@Title{index}", book.Title);
        command.Parameters.AddWithValue($"@Price{index}", book.Price);

        command.ExecuteNonQuery();
    }

    private void InsertAuthor(SqlCommand command, Author author, Guid bookId, int index)
    {
        // Create author
        command.CommandType = CommandType.Text;
        command.CommandText = $"INSERT INTO [dbo].[Authors] (Id, Name) VALUES (@AuthorId{index}, @Name{index})";
        command.Parameters.AddWithValue($"@AuthorId{index}", author.Id);
        command.Parameters.AddWithValue($"@Name{index}", author.Name);

        command.ExecuteNonQuery();

        // Create link between author and book
        command.CommandType = CommandType.Text;
        command.CommandText = $"INSERT INTO [dbo].[BookAuthors] (BookId, AuthorId) VALUES (@BookId{index}, @Author2Id{index})";
        command.Parameters.AddWithValue($"@BookId{index}", bookId);
        command.Parameters.AddWithValue($"@Author2Id{index}", author.Id);

        command.ExecuteNonQuery();
    }
}

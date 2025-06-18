// Application/Interfaces/IBookService.cs
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetAll();
        Task<Book?> GetById(Guid id);
        Task<Book> Add(Book book);
        Task<Book?> Update(Guid id, Book book);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Book>> SearchBooks(string? title, string? author, string? genre);
    }
}

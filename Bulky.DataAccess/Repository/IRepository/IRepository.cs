using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // This is Generic Repo Interfact.

        /*What is Generic Repo ? 
         
         In ASP.NET Core MVC, a generic repository is a design pattern that provides a consistent way to CRUD(Create, Read, Update, Delete) operations on various types of entities in a data access layer. generic repository acts as an abstraction layer between the application and the underlying data source, such a database.
        
        The main purpose of using a generic repository is to promote code reuse and reduce duplication. Instead creating separate repositories for each entity, a generic repository allows you to define a repository class that can work with any entity type. This helps in maintaining a cleaner and more scalable codebase.
         
        For eg. T=Category, we wanna perform all the curd operation on Caterogy table.
        
        */


        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);
        void Add(T enitiy);
        void Remove(T enitiy);
        void RemoveRange(IEnumerable<T> enitiy);

    }
}

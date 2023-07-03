using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        /* this below code creates the object of ApplicationDBContext class so that we can use all the functionalities of Entity Framework Core.

         Setting the ApplicationDBContext Object to use all the EF Core functionailty for curd operations

        Passing the object of ApplicationDBContext to the base class as well, this class is implimenting the functionalities of Generic Repo which is Repository<T> Interface and that Interface accepts the obj of ApplicationDBContext.
        */
        private readonly ApplicationDBContext _db;
        public CategoryRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}

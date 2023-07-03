using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        // this below code creates the object of ApplicationDBContext class so that we can use all the functionalities of Entity Framework Core.

        private readonly ApplicationDBContext _db;
        public ICategoryRepository Category { get; private set; }
        public IItemRepository Item { get; private set; }

        // Setting the ApplicationDBContext Object to use all the EF Core functionailty for curd operations
        public UnitOfWork(ApplicationDBContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Item = new ItemRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

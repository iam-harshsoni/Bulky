using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using BulkyBook.DataAccess.Repository.IRepository;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDBContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDBContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();

            _db.Items.Include(x => x.Category).Include(x => x.CategoryId);
        }

        public void Add(T enitiy)
        {
            dbSet.Add(enitiy);
        }
        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var includePro in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query.Include(includePro);
                }
            }


            return query.FirstOrDefault();
        }

        // indlucing this too Entity as well 'Category,CoverType' because Items uses it as Forieng key so we can fetch data of these tables using Include function of Entity Framework Core

        // Populating Navigation Property Using ".include()" 
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includePro in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query.Include(includePro);
                }
            }

            return query.ToList();
        }

        public void Remove(T enitiy)
        {
            dbSet.Remove(enitiy);
        }

        public void RemoveRange(IEnumerable<T> enitiy)
        {

            dbSet.RemoveRange(enitiy);
        }
    }
}

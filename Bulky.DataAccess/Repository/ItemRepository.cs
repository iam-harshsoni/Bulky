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
    public class ItemRepository : Repository<Item>, IItemRepository
    {

        private readonly ApplicationDBContext _db;

        public ItemRepository(ApplicationDBContext db) : base(db) {
            
            _db = db;        
        }
        
        public void Update(Item obj)
        {
            //_db.Items.Update(obj);

            var objFromDB = _db.Items.FirstOrDefault(x => x.Id == obj.CategoryId);

            if (objFromDB != null)
            {
                objFromDB.Title= obj.Title;
                objFromDB.Description= obj.Description;
                objFromDB.Author= obj.Author;
                objFromDB.Category= obj.Category;
                objFromDB.ISBN= obj.ISBN;
                objFromDB.Price = obj.Price;
                objFromDB.Price50= obj.Price50;
                objFromDB.Price100= obj.Price100;
                objFromDB.ListPrice=obj.ListPrice;  

                if(obj.ImageURL!=null)
                {
                    objFromDB.ImageURL= obj.ImageURL;
                }
            }
        }
    }
}

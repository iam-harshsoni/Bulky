using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {

        /* this IunitofWork repo created because to reduce duplicacy of Save() Functionality. Prior we have Save functionality in Category repo, but then this save functionlity is all same for all the other model classes. And Its not logical to write it mulitple times for all the models.

        Better practise is to make a seprate repo for this general functionality and use it for all the model class.

        */

        ICategoryRepository Category { get; }
        IItemRepository Item { get; }
        void Save();

    }
}

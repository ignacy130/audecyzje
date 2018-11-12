using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Audecyzje.Core.Domain;

namespace Audecyzje.Core.Repositories
{
    public interface IRepository<T> where T:BaseEntity
    {
        Task<T> Get(int id);
        Task<ICollection<T>> GetAll();
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(int id);
    }
}

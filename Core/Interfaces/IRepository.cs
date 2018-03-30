using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Core.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(string id);

        Task Add(T entity);

        Task<bool> Delete(ObjectId id);
    }
}
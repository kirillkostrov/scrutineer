using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IStandartRepository : IRepository<Standart>
    {
        Task<Standart> GetByCode(string code);
    }
}
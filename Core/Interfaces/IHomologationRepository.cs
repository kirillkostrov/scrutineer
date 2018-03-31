using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IHomologationRepository : IRepository<Homologation>
    {
        Task<Homologation> GetByCode(string homologationCode);
    }
}
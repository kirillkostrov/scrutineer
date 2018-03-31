using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICheckerService
    {
        Task<CheckResult> Check(string rawRecognizedString);
    }
}
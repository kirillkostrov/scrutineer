using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    public class CheckerController : Controller
    {
        private readonly ICheckerService _checkerService;

        public CheckerController(ICheckerService checkerService)
        {
            _checkerService = checkerService;
        }

        [HttpPost]
        public async Task<CheckResult> Check([FromBody] string standartCode)
        {
            return await _checkerService.Check(standartCode, string.Empty);
        }
    }
}
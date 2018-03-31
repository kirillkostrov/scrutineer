using System;
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

        [HttpGet]
        public async Task<CheckResult> Check(string str)
        {
            return await _checkerService.Check(str);
        }
        
        [HttpPost]
        public async Task<CheckResult> CheckPost([FromBody] string str)
        {
            return await _checkerService.Check(str);
        }
    }
}
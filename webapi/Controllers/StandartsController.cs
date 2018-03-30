using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    public class StandartsController : Controller
    {
        private readonly IStandartRepository _standartRepository;

        public StandartsController(IStandartRepository standartRepository)
        {
            _standartRepository = standartRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Standart>> Get()
        {
            return await _standartRepository.GetAll();
        }
    }
}
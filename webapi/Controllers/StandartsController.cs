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
        private readonly IStandartService _standartService;

        public StandartsController(
            IStandartRepository standartRepository,
            IStandartService standartService)
        {
            _standartRepository = standartRepository;
            _standartService = standartService;
        }

        [HttpGet]
        public async Task<IEnumerable<Standart>> Get()
        {
            return await _standartRepository.GetAll();
        }

        [HttpGet("list")]
        public IEnumerable<NamedItem> GetAllList()
        {
            return _standartService.GetStandartsList();
        }

        [HttpPost]
        public void Add([FromBody] Standart standart)
        {
            _standartRepository.Add(new Standart
            {
                Code = standart.Code
            });
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _standartRepository.Delete(id);
        }
    }
}
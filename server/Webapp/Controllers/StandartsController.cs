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
        public async Task<IEnumerable<Standart>> Get() => await _standartRepository.GetAll();
        
        [HttpPost]
        public void Add([FromBody] Standart standart) => 
            _standartRepository.Add(new Standart
            {
                Code = standart.Code,
                Description = standart.Description,
                EndDate = standart.EndDate,
                Name = standart.Name,
                StartDate = standart.StartDate,
            });

        [HttpDelete("{id}")]
        public void Delete(string id) => _standartRepository.Delete(id);
    }
}
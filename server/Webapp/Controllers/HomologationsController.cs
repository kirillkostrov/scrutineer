using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    public class HomologationsController : Controller
    {
        private readonly IHomologationRepository _homologationRepository;
        
        public HomologationsController(IHomologationRepository homologationRepository)
        {
            _homologationRepository = homologationRepository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Homologation>> Get() => await _homologationRepository.GetAll();
        
        
        [HttpPost]
        public void Add([FromBody] Homologation homologation) => 
            _homologationRepository.Add(new Homologation
            {
                Code = homologation.Code,
                Name = homologation.Name,
                HomologationItems = homologation.HomologationItems,
                Manufacturer = homologation.Manufacturer,
            });
    }
}
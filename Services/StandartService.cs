using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Interfaces;

namespace Services
{
    public class StandartService : IStandartService
    {
        private readonly IStandartRepository _standartRepository;

        public StandartService(IStandartRepository standartRepository)
        {
            _standartRepository = standartRepository;
        }

        public IEnumerable<NamedItem> GetStandartsList()
        {
            return _standartRepository.GetAll().Result.Select(x => new NamedItem
            {
                InternalId = x.InternalId,
                Name = x.Name
            });
        }
    }
}
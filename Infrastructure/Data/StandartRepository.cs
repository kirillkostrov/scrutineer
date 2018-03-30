using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class StandartRepository : IRepository<Standart>
    {
        public Task<IEnumerable<Standart>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Standart> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Standart> Add(Standart entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
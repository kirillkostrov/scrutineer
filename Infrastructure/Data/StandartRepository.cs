using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Options;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class StandartRepository : IStandartRepository
    {
        private readonly Context _context;

        public StandartRepository(IOptions<Settings> settings)
        {
            _context = new Context(settings);
        }

        public async Task<IEnumerable<Standart>> GetAll()
        {
            try
            {
                return await _context.Standarts.Find(_ => true).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task<Standart> GetById(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task<Standart> Add(Standart entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(ObjectId id)
        {
            throw new NotImplementedException();
        }
    }
}
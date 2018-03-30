using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Helpers;
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

        public async Task<Standart> GetById(string id)
        {
            try
            {
                var internalId = IdParser.GetInternalId(id);
                return await _context.Standarts.Find(x => x.InternalId == internalId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }            
        }

        public async Task Add(Standart entity)
        {
            try
            {
                if (entity.InternalId == ObjectId.Empty)
                {
                    entity.InternalId = ObjectId.GenerateNewId();
                }
                await _context.Standarts.InsertOneAsync(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task<bool> Delete(ObjectId id)
        {
            throw new NotImplementedException();
        }
    }
}
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
    public class HomologationRepository : IHomologationRepository
    {
        private readonly Context _context;

        public HomologationRepository(IOptions<Settings> settings)
        {
            _context = new Context(settings);
        }

        public async Task<IEnumerable<Homologation>> GetAll()
        {
            return await ExecuteAndHandleException<IEnumerable<Homologation>>.Execute(async () =>
                await _context.Homologations.Find(_ => true).ToListAsync());
        }

        public async Task<Homologation> GetById(string id)
        {
            return await ExecuteAndHandleException<Homologation>.Execute(async () =>
                {
                    var internalId = IdParser.GetInternalId(id);
                    return await _context.Homologations.Find(x => x.InternalId == internalId).FirstOrDefaultAsync();
                }
            );
        }

        public async Task Add(Homologation entity)
        {
            await ExecuteAndHandleException.Execute(async () =>
                {
                    if (entity.InternalId == ObjectId.Empty) entity.InternalId = ObjectId.GenerateNewId();
                    await _context.Homologations.InsertOneAsync(entity);
                }
            );
        }

        public Task<bool> Delete(string id)
        {
            // TODO: [IS] ...
            throw new NotImplementedException();
        }

        public async Task<Homologation> GetByCode(string homologationCode)
        {
            return await ExecuteAndHandleException<Homologation>.Execute(async () =>
                await _context.Homologations.Find(x => x.Code == homologationCode).FirstOrDefaultAsync());
        }
    }
}
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
            return await ExecuteAndHandleException<IEnumerable<Standart>>.Execute(async () =>
                await _context.Standarts.Find(_ => true).ToListAsync());
        }

        public async Task<Standart> GetById(string id)
        {
            return await ExecuteAndHandleException<Standart>.Execute(async () =>
                {
                    var internalId = IdParser.GetInternalId(id);
                    return await _context.Standarts.Find(x => x.InternalId == internalId).FirstOrDefaultAsync();
                }
            );
        }

        public async Task Add(Standart entity)
        {
            await ExecuteAndHandleException.Execute(async () =>
                {
                    if (entity.InternalId == ObjectId.Empty) entity.InternalId = ObjectId.GenerateNewId();
                    await _context.Standarts.InsertOneAsync(entity);
                }
            );
        }

        public async Task<bool> Delete(string id)
        {
            return await ExecuteAndHandleException<bool>.Execute(async () =>
                {
                    // TODO: [IS] need to change "id" cield from "Code" to real identifier
                    var deleteResult = await _context.Standarts
                        .DeleteOneAsync(Builders<Standart>.Filter.Eq("Code", id));
                    return deleteResult.IsAcknowledged
                           && deleteResult.DeletedCount > 0;
                }
            );
        }
    }
}
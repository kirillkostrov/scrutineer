using MongoDB.Bson;

namespace Core.Helpers
{
    public static class IdParser
    {
        public static ObjectId GetInternalId(string id)
        {
            if(!ObjectId.TryParse(id, out var internalId))
                internalId = ObjectId.Empty;
            return internalId;
        }
    }
}
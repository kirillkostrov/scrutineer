﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities
{
    public class BaseEntity
    {
        [BsonId]
        public ObjectId InternalId { get; set; }

        public Guid Id { get; set; }
    }
}
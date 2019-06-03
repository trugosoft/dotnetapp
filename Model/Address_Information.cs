﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Pellucid.Core.Api.Model
{
    public class Address_Information
    {
        [BsonId]
        // standard BSonId generated by MongoDb
        public ObjectId InternalId { get; set; }

        public string pincode { get; set; }
        public string division_name { get; set; }
        public string office_type { get; set; }
        public string district { get; set; }
        public string state_name { get; set; }


    }
}

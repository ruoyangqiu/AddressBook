﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace AddressBook.Data
{
    public class Country
    {
        // Unique id, primary key.
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // Country name.
        [BsonElement("Name")]
        public string Name { get; set; }

        // List of administrative areas.
        [BsonElement("AdminAreas")]
        public List<string> AdminAreas { get; set; }


        #region format

        public bool HasAddressLine3 { get; set; } = false;

        public bool HasExtraData { get; set; } = false;

        public string AdminAreaDisplayName { get; set; } = "State/Province/Region";

        public string LocalityDisplayName { get; set; } = "City";

        public bool HasLocality { get; set; } = true;

        public string SublocalityDisplayName { get; set; } = "District";

        public bool HasSublocality { get; set; } = false;

        #endregion format
    }
}

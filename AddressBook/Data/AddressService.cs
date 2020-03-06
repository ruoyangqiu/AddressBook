﻿using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;


namespace AddressBook.Data
{
    public class AddressService
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();


        //To Get number of addresses     
        public long GetNumOfAddresses()
        {
            try
            {
                return db.AddressRecord.CountDocuments(_ => true);
            }
            catch
            {
                throw;
            }
        }

        //To Get all address details      
        public List<Address> GetAllAddresses()
        {
            try
            {
                return db.AddressRecord.Find(_ => true).Limit(1000).ToList();
            }
            catch
            {
                throw;
            }
        }

        //To Add new address record      
        public void CreateAddress(Address address)
        {
            try
            {
                db.AddressRecord.InsertOne(address);
            }
            catch
            {
                throw;
            }
        }

        // To get the list of Countries 
        public List<CountryFormat> GetAllCountryFormats()
        {
            try
            {
                return db.CountryRecord.Find(_ => true).ToList();
            }
            catch
            {
                throw;
            }
        }

        // To add a new country record
        internal void CreateCountryFormat(CountryFormat country)
        {
            try
            {
                db.CountryRecord.InsertOne(country);
            }
            catch
            {
                throw;
            }
        }

        // Get country by id
        public CountryFormat GetCountryFormat(string id)
        {
            try
            {
                FilterDefinition<CountryFormat> filterCountryData = Builders<CountryFormat>.Filter.Eq("Id", id);

                return db.CountryRecord.Find(filterCountryData).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        //To Delete all addresses
        public void DeleteAllAddresses()
        {
            try
            {
                db.AddressRecord.DeleteManyAsync(_ => true);
            }
            catch
            {
                throw;
            }
        }

        //To load all default addresses
        public void LoadDefaultAddresses()
        {
            try
            {
                var addressList = DefaultData.GetDefaultAddressList();
                foreach (var item in addressList)
                {
                    db.AddressRecord.InsertOne(item);
                }
            }
            catch
            {
                throw;
            }
        }

        //To Delete all Countries
        public void DeleteAllCountryFormats()
        {
            try
            {
                db.CountryRecord.DeleteManyAsync(_ => true);
            }
            catch
            {
                throw;
            }
        }

        //To load all default countries
        public void LoadDefaultCountryFormats()
        {
            try
            {
                foreach (var item in DefaultData.DefaultCountryList)
                {
                    db.CountryRecord.InsertOne(item);
                }
            }
            catch
            {
                throw;
            }
        }

        // Find if the given address exist in database
        public List<Address> GetAddressByWholeAddress(Address data)
        {
            FilterDefinition<Address> addrline1 = Builders<Address>.Filter.Eq(x => x.AddressLine1, data.AddressLine1);
            FilterDefinition<Address> addrline2 = Builders<Address>.Filter.Eq(x => x.AddressLine2, data.AddressLine2);
            FilterDefinition<Address> addrline3 = Builders<Address>.Filter.Eq(x => x.AddressLine3, data.AddressLine3);
            FilterDefinition<Address> extradata = Builders<Address>.Filter.Eq(x => x.ExtraData, data.ExtraData);
            FilterDefinition<Address> combineAddr = Builders<Address>.Filter.And(addrline1, addrline2, addrline3, extradata);
            FilterDefinition<Address> country = Builders<Address>.Filter.Eq(x => x.Country, data.Country);
            FilterDefinition<Address> adminArea = Builders<Address>.Filter.Eq(x => x.AdminArea, data.AdminArea);
            FilterDefinition<Address> city = Builders<Address>.Filter.Eq(x => x.Locality, data.Locality);
            FilterDefinition<Address> town = Builders<Address>.Filter.Eq(x => x.Sublocality, data.Sublocality);
            FilterDefinition<Address> region = Builders<Address>.Filter.And(country, adminArea, city, town);
            FilterDefinition<Address> postcode = Builders<Address>.Filter.Eq(x => x.PostalCode, data.PostalCode);
            FilterDefinition<Address> allMatch = Builders<Address>.Filter.And(combineAddr, region, postcode);
            return db.AddressRecord.Find(allMatch).ToList();
        }

        // Find all addresses that partially match
        public List<Address> GetAddressByPartialAddress(Address data)
        {
            FilterDefinition<Address> addrFilter = Builders<Address>.Filter.Eq(x => x.AddressLine1, data.AddressLine1); // line 1 is always required

            // add filters for all non-empty fields

            if (!string.IsNullOrEmpty(data.AddressLine2))
            {
                addrFilter &= Builders<Address>.Filter.Eq(x => x.AddressLine2, data.AddressLine2);
            }

            if (!string.IsNullOrEmpty(data.AddressLine3))
            {
                addrFilter &= Builders<Address>.Filter.Eq(x => x.AddressLine3, data.AddressLine3);
            }

            if (!string.IsNullOrEmpty(data.ExtraData))
            {
                addrFilter &= Builders<Address>.Filter.Eq(x => x.ExtraData, data.ExtraData);
            }

            if (!string.IsNullOrEmpty(data.AdminArea))
            {
                addrFilter &= Builders<Address>.Filter.Eq(x => x.AdminArea, data.AdminArea);
            }

            if (!string.IsNullOrEmpty(data.Locality))
            {
                addrFilter &= Builders<Address>.Filter.Eq(x => x.Locality, data.Locality);
            }

            if (!string.IsNullOrEmpty(data.Locality))
            {
                addrFilter &= Builders<Address>.Filter.Eq(x => x.Locality, data.Locality);
            }

            if (!string.IsNullOrEmpty(data.Sublocality))
            {
                addrFilter &= Builders<Address>.Filter.Eq(x => x.Sublocality, data.Sublocality);
            }

            if (!string.IsNullOrEmpty(data.PostalCode))
            {
                addrFilter &= Builders<Address>.Filter.Eq(x => x.PostalCode, data.PostalCode);
            }

            if (!string.IsNullOrEmpty(data.Country))
            {
                addrFilter &= Builders<Address>.Filter.Eq(x => x.Country, data.Country);
            }

            return db.AddressRecord.Find(addrFilter).ToList();
        }
    }
}

using quepasa_api.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace quepasa_api.Services
{
    public class ContactsService
    {
        private readonly IMongoCollection<Contact> _contacts;
        private readonly IContactsDatabaseSettings _settings;

        public ContactsService(IContactsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _contacts = database.GetCollection<Contact>(settings.ContactsCollectionName);
            _settings = settings;
        }

        public List<Contact> Get()
        {
            return _contacts.Find(c => true).ToList();
        }

        public Contact Get(string id)
        {
            return _contacts.Find<Contact>(c => c.Id == id).FirstOrDefault();
        }

        public Contact Create(Contact c)
        {
            if (c == null)
            {
                throw new System.ArgumentNullException(nameof(c));
            }

            _contacts.InsertOne(c);
            return c;
        }

        public void Update(string id, Contact newContact)
        {
            _contacts.ReplaceOne(c => c.Id == id, newContact);
        }

        public void Remove(Contact c)
        {
            _contacts.DeleteOne(contact => contact.Id == c.Id);
        }

        public void Remove(string id)
        {
            _contacts.DeleteOne(c => c.Id == id);
        }

        public void AssociateOneToOne(string sourceContactId, string targetContactId)
        {
            var filter = Builders<Contact>.Filter.Eq("_id", targetContactId);
            var update = Builders<Contact>.Update.Set("RelatedPerson", new MongoDBRef(_settings.ContactsCollectionName, sourceContactId));

            _contacts.UpdateOne(filter, update);
        }
    }
}
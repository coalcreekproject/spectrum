using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace Spectrum.Data.Eoc.Infrastructure
{
    public class DocumentDbRepository<T>
    {
        public DocumentDbRepository(string endPoint,
            string authKey,
            string databaseId,
            string collectionId)
        {
            var endPointUri = new Uri(endPoint);
            _client = new DocumentClient(endPointUri, authKey);
            _databaseId = databaseId;
            _collectionId = collectionId;
            _database = ReadOrCreateDatabase();
        }

        private readonly DocumentClient _client;
        private readonly string _collectionId;
        private readonly Database _database;
        private readonly string _databaseId;

        private DocumentCollection _collection;

        public DocumentCollection Collection
        {
            get { return _collection ?? (_collection = ReadOrCreateCollection(_database.SelfLink)); }
        }

        // Use the Database if it exists, if not create a new Database
        private Database ReadOrCreateDatabase()
        {
            var db = _client.CreateDatabaseQuery()
                .Where(d => d.Id == _databaseId)
                .AsEnumerable()
                .FirstOrDefault() ?? _client.CreateDatabaseAsync(new Database {Id = _databaseId}).Result;

            return db;
        }

        // Use the DocumentCollection if it exists, if not create a new Collection
        private DocumentCollection ReadOrCreateCollection(string databaseLink)
        {
            var col = _client.CreateDocumentCollectionQuery(databaseLink)
                .Where(c => c.Id == _collectionId)
                .AsEnumerable()
                .FirstOrDefault();

            if (col == null)
            {
                var collectionSpec = new DocumentCollection {Id = _collectionId};
                var requestOptions = new RequestOptions {OfferType = "S1"};

                col = _client.CreateDocumentCollectionAsync(databaseLink, collectionSpec, requestOptions).Result;
            }

            return col;
        }

        public IEnumerable<T> GetItems(Expression<Func<T, bool>> predicate)
        {
            return _client.CreateDocumentQuery<T>(Collection.DocumentsLink)
                .Where(predicate)
                .AsEnumerable();
        }

        public IEnumerable<T> GetItems()
        {
            return _client.CreateDocumentQuery<T>(Collection.DocumentsLink).AsEnumerable();
        }

        public async Task<Document> CreateItemAsync(T item)
        {
            return await _client.CreateDocumentAsync(Collection.SelfLink, item);
        }

        public T GetItem(Expression<Func<T, bool>> predicate)
        {
            return _client.CreateDocumentQuery<T>(Collection.DocumentsLink)
                .Where(predicate)
                .AsEnumerable()
                .FirstOrDefault();
        }

        public async Task<Document> UpdateItemAsync(string id, T item)
        {
            var doc = GetDocument(id);
            return await _client.ReplaceDocumentAsync(doc.SelfLink, item);
        }

        private Document GetDocument(string id)
        {
            return _client.CreateDocumentQuery(Collection.DocumentsLink)
                .Where(d => d.Id == id)
                .AsEnumerable()
                .FirstOrDefault();
        }
    }
}
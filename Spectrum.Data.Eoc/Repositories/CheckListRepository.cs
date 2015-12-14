using System.Collections.Generic;
using Raven.Client.Document;
using Spectrum.Data.Eoc.Models;

namespace Spectrum.Data.Eoc.Repositories
{
    public class ChecklistRepository
    {
        private DocumentStore _documentStore;

        //session for RavenDB
        public void Initialize()
        {
            _documentStore = new DocumentStore { Url = "http://localhost:8080/" };
            _documentStore.Initialize();
        }

        public void SaveSomething()
        {
            using (var session = _documentStore.OpenSession())
            {
                var checklist = new CheckList()
                {
                    Name = "Test Checklist for NCOEM",
                };

                session.Store(checklist);

                session.SaveChanges();
            }
        }


        public void Dispose()
        {
            _documentStore.Dispose();
        }


        //CRUD
    }
}

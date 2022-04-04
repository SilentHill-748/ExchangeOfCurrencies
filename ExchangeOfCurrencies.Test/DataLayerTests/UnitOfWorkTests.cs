using System.Linq;
using Generics = System.Collections.Generic;

using NUnit.Framework;

using Microsoft.EntityFrameworkCore;

using ExchangeOfCurrencies.Data;
using ExchangeOfCurrencies.Data.Interfaces;
using ExchangeOfCurrencies.Data.Entities;
using ExchangeOfCurrencies.Test.DataLayerTests.Helpers;

namespace ExchangeOfCurrencies.Test.DataLayerTests
{
    public class UnitOfWorkTests
    {
        private readonly UnitOfWork<AppDbContext> _unitOfWork;
        private IRepository<Client> _clientRepository;


        public UnitOfWorkTests()
        {
            _unitOfWork = new UnitOfWork<AppDbContext>(
                new AppDbContextFactory().CreateDbContext());

            _clientRepository = _unitOfWork.GetRepository<Client>();
        }


        [SetUp]
        public void Init()
        {
            DropDatabase();

            var clients = ClientHelper.GetClientsWithIncludes();

            _clientRepository.AddRange(clients);

            _ = _unitOfWork.Save();
            _unitOfWork.DbContext.ChangeTracker.Clear();
        }


        [Test]
        public void AddClientWithoutIncludesTest()
        {
            DropDatabase();
            Client expected = ClientHelper.GetClearClient();

            _clientRepository.Add(expected);
            int affectedRows = _unitOfWork.Save();

            Assert.AreEqual(expected, _unitOfWork.DbContext.Clients.First());
            Assert.AreEqual(3, affectedRows);
        }

        [Test]
        public void AddClientWithIncludesTest()
        {
            DropDatabase();
            Client expected = ClientHelper.GetClientWithIncludes();

            _clientRepository.Add(expected);
             int affectedRows = _unitOfWork.Save();

            var actual = _unitOfWork.DbContext.Clients
                .First(x => x.Email.Equals(expected.Email));

            Assert.AreEqual(3, affectedRows);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddClientsWithoutIncludesTest()
        {
            DropDatabase();
            var expected = ClientHelper.GetClearClients();

            _clientRepository.AddRange(expected);
             int affectedRows = _unitOfWork.Save();

            var actual = _unitOfWork.DbContext.Clients.ToList();

            for (int i = 0; i < expected.Count; i++)
                Assert.AreEqual(expected[i], actual[i]);

            Assert.AreEqual(9, affectedRows);
        }

        [Test]
        public void AddClientsWithIncludesTest()
        {
            DropDatabase();
            var expected = ClientHelper.GetClientsWithIncludes();

            _clientRepository.AddRange(expected);
            int affectedRows = _unitOfWork.Save();

            var actual = _unitOfWork.DbContext.Clients.ToList();

            for (int i = 0; i < actual.Count; i++)
                Assert.AreEqual(expected[i], actual[i]);

            Assert.AreEqual(9, affectedRows);
        }

        [Test]
        public void UpdateClientFromDatabaseTest()
        {
            string newName = "Иванов Иван Иваныч";

            Client client = _unitOfWork.DbContext.Clients
                .First(x => x.FullName.Equals("Tester Client Two"));
            client.FullName = newName;

            _clientRepository.Update(client);
            int affectedRows = _unitOfWork.Save();

            Client? actual = _unitOfWork.DbContext.Clients
                .FirstOrDefault(x => x.FullName.Equals(newName));

            Assert.AreEqual(1, affectedRows);
            Assert.IsNotNull(actual);
            Assert.AreEqual(client, actual);
        }

        [Test]
        public void UpdateAllClientsFromDatabaseTest()
        {
            string expectedName = "Петров Петр Петрович";

            var clients = _unitOfWork.DbContext.Clients.ToList();

            for (int i = 0; i < clients.Count; i++)
                clients[i].FullName = expectedName;

            int affectedRows = _unitOfWork.Save();

            var actualClients = _unitOfWork.DbContext.Clients.ToList();

            Assert.AreEqual(3, affectedRows);

            for (int i = 0; i < actualClients.Count; i++)
                Assert.AreEqual(actualClients[i].FullName, expectedName);
        }

        [Test]
        public void RemoveOneClientFromDatabaseTest()
        {
            Client? client = _unitOfWork.DbContext.Find<Client>(1);

            Assert.NotNull(client);

            _clientRepository.Delete(client!);
            _unitOfWork.Save();

            bool isRemoved = _unitOfWork.DbContext.Clients.Contains(client);

            Assert.IsFalse(isRemoved);
        }

        [Test]
        public void RemoveAllClientsTest()
        {
            var expected = _unitOfWork.DbContext.Clients.ToList();

            _clientRepository.Delete(expected);
            int affectedRows = _unitOfWork.Save();

            var actual = _unitOfWork.DbContext.Clients.ToList();

            Assert.IsTrue(actual.Count == 0);
            Assert.AreEqual(3, affectedRows);
        }

        public void GetAllClientsByIdTest()
        {
            int id1 = 1, id2 = 2, id3 = 3;

            var actual = new Generics.List<Client>()
            {
                _clientRepository.GetById(id1),
                _clientRepository.GetById(id2),
                _clientRepository.GetById(id3)
            };

            var expected = _unitOfWork.DbContext.Clients.ToList();

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
                Assert.AreEqual(expected[i], actual[i]);
        }

        [Test]
        public void SelectAllClientsWithoutParametersTest()
        {
            var expected = _unitOfWork.DbContext.Clients.ToList();

            _unitOfWork.DbContext.ChangeTracker.Clear();
            var actual = _clientRepository.Select().ToList();

            Assert.AreEqual(expected.Count, actual.Count);
            Assert.IsFalse(_unitOfWork.DbContext.ChangeTracker.Entries().Any()); //Tracking should be empty.

            for (int i = 0; i < expected.Count; i++)
                Assert.AreEqual(expected[i], actual[i]);
        }

        [Test]
        public void SelectAllClientsWithPredicateTest()
        {
            var expected = _unitOfWork.DbContext.Clients.Find(1)!;

            _unitOfWork.DbContext.ChangeTracker.Clear();
            var actual = _clientRepository.Select(x => x.ClientId < 2).ToList();

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(expected, actual[0]);
            Assert.IsFalse(_unitOfWork.DbContext.ChangeTracker.Entries().Any()); //Tracking should be empty.
        }

        [Test]
        public void SelectAllClientsWithPredicateAndIncludeTest()
        {
            var expected = _unitOfWork.DbContext.Clients.Find(1)!;

            _unitOfWork.DbContext.ChangeTracker.Clear();
            var actual = _clientRepository
                .Select(
                    x => x.ClientId < 2, 
                    x => x.Include(x => x.Wallet))
                .ToList();

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(actual[0].Credentials.Login, string.Empty);
            Assert.AreEqual(actual[0].Credentials.Password, string.Empty);
            Assert.IsFalse(_unitOfWork.DbContext.ChangeTracker.Entries().Any()); //Tracking should be empty.
        }

        [Test]
        public void SelectAllClientsWithAllParametersTest()
        {
            var expected = _unitOfWork.DbContext.Clients.Find(1)!;

            _unitOfWork.DbContext.ChangeTracker.Clear();
            var actual = _clientRepository
                .Select(
                    x => x.ClientId < 2,
                    x => x.Include(x => x.Wallet)
                        .Include(x => x.Credentials),
                    false)
                .ToList();

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(actual[0].Credentials.Login, "Test1");
            Assert.AreEqual(actual[0].Credentials.Password, "test1");
            Assert.IsTrue(_unitOfWork.DbContext.ChangeTracker.Entries().Any()); //Tracking should be not empty.
        }

        [Test]
        public void SelectAllClientsWithAllParametersButNotTrackedTest()
        {
            var expected = _unitOfWork.DbContext.Clients.Find(1)!;

            _unitOfWork.DbContext.ChangeTracker.Clear();
            var actual = _clientRepository
                .Select(
                    x => x.ClientId < 2,
                    x => x.Include(x => x.Wallet)
                        .Include(x => x.Credentials),
                    true)
                .ToList();

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(actual[0].Credentials.Login, "Test1");
            Assert.AreEqual(actual[0].Credentials.Password, "test1");
            Assert.IsFalse(_unitOfWork.DbContext.ChangeTracker.Entries().Any()); //Tracking should be empty.
        }

        private void DropDatabase()
        {
            _unitOfWork.DbContext.Database.EnsureDeleted();
            _unitOfWork.DbContext.Database.EnsureCreated();
            _unitOfWork.DbContext.ChangeTracker.Clear();
        }
    }
}
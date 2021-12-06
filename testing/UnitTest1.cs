using System.Linq;
using infrastructure;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace testing
{
    public class Tests
    {
        private DbContextOptions<TestDBContext> _contextOptions;
        private TestDBContext _context;
        [SetUp]
        public void Setup()
        {
            _contextOptions = new DbContextOptionsBuilder<TestDBContext>()
                .UseSqlServer(
                    @"data source=127.0.0.1;initial catalog=testDB;persist security info=True;user id=sa;password=1qaz@WSX;MultipleActiveResultSets=True;")
                .Options;

            _context = new TestDBContext(_contextOptions);
        }

        [Test]
        public void Test1()
        {
            var query = _context.Users.FirstOrDefault(x => x.Id == 1);
            Assert.IsNotNull(query);
            Assert.AreEqual(query?.UserName, "Peter Parker");
            Assert.AreEqual(query?.Date.ToString("yyyy-MM-dd"),"2021-12-07");
        }
    }
}
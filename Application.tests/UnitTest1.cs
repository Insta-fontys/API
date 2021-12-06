using DataAccesLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.tests
{
    public class UnitTest1
    {
        [Fact, Trait("Category", "A")]
        public void CheckNumberTest()
        {
            int number = 1;

            Assert.Equal(1, number);
        }

        [Fact]
        public async Task Follow_Creator()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "MyGram").Options;
        }
    }
}

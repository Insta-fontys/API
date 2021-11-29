using System;
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
    }
}

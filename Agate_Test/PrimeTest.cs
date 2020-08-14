using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Agate_Test
{
    public class PrimeTest
    {
        [Theory]
        [InlineData(1, false)]
        [InlineData(2, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        [InlineData(9, false)]
        [InlineData(13, true)]
        [InlineData(23, true)]
        public void SqrtMethodTest(int n, bool res)
        {
            Assert.Equal(Prime.SqrtMethod(n), res);
        }
    }
}

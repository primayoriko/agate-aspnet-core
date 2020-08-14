using System;
using System.Collections.Generic;
using System.Linq;
using Test_App;
using Xunit;

namespace XUnit_Test
{
    public class SortTest
    {
        [Fact]
        public void BubbleTest1()
        {
            var data = new List<int>()
            {
                1, 2, 23, 12, 1, 23, 7
            };
            var result = new List<int>()
            {
                1, 1, 2, 7, 12, 23, 23
            };
            var equal = result.SequenceEqual(Sort.BubbleSortInt(data));
            Assert.True(equal);
        }

        [Fact]
        public void BubbleTest2()
        {
            var data = new List<int>()
            {
                1, 2, 0, 12, 12, 23, 7
            };
            var result = new List<int>()
            {
                1, 1, 0, 2, 7, 12, 23, 12
            };
            var equal = result.SequenceEqual(Sort.BubbleSortInt(data));
            Assert.False(equal);
        }
    }
}

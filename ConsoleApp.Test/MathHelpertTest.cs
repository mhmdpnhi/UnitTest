using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Test.TestData;
using Xunit.Abstractions;

namespace ConsoleApp.Test
{
    public class MathHelpertTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public MathHelpertTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact(Skip = "[Describe reason for skip]")]
        //[Fact]
        [Trait("Service", "group1")]
        public void SumTest()
        {
            MathHelper mathHelper = new MathHelper();

            var x = 4;
            var y = 2;
            var result = mathHelper.Sum(x, y);

            Assert.Equal(6, result);
            Assert.IsType<int>(result);
        }

        //[Theory(Skip = "[Describe reason for skip]")]
        [Theory]
        [Trait("Service", "group2")]
        [InlineData(10, 15, 25)]
        [InlineData(5, -10, -5)]
        public void SumTest_InlineData(int x, int y, int expected)
        {
            MathHelper mathHelper = new MathHelper();
            var result = mathHelper.Sum(x, y);

            Assert.Equal(expected, result);
            Assert.IsType<int>(expected);
        }

        [Theory]
        [Trait("Service", "group3")]
        [MemberData(nameof(MathHelper_MemberTestData.Sum_TestData), MemberType = typeof(MathHelper_MemberTestData))]
        public void SumTest_MemberData(int x, int y, int expected)
        {
            MathHelper mathHelper = new MathHelper();
            var result = mathHelper.Sum(x, y);

            Assert.Equal(expected, result);
        }

        [Theory]
        [Trait("Service", "group4")]
        [ClassData(typeof(MathHelper_ClassTestData))]
        public void SumTest_ClassData(int x, int y, int expected)
        {
            MathHelper mathHelper = new MathHelper();
            var result = mathHelper.Sum(x, y);

            _outputHelper.WriteLine($"x is :{x} \ny is :{y}\nresult : x + y = {result}\nexpected : {expected}");

            Assert.Equal(expected, result);
        }
    }
}

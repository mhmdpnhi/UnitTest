using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Test.TestData
{
    public static class MathHelper_MemberTestData
    {
        public static List<object[]> Sum_TestData()
        {
            List<object[]> data = new List<object[]>();

            data.Add(new object[] { 8, 5, 13 });
            data.Add(new object[] { 14, 6, 20 });

            return data;
        }
    }
}

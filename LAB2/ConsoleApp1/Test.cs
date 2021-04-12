using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;



namespace Lab2.Test
{
    [TestFixture]
    class Test
    {
        [TestCase(new int[] { -3, 4, 8, 20, -8, 6, -13, -6, 12, -12 })]
        [TestCase(new int[] { 6, 4, -15, -8, 6, -9, 2 })]
        [TestCase(new int[] { 19, 13, 16, 2, 18, -10, -17, 17, -15, 16, 3 })]
        [TestCase(new int[] { 17, 9, 3, -15, -5, -9, -18, 8, -13, -6, 11, -18, 20, 18, -13, -7, -1 })]
        [TestCase(new int[] { 15, -19, 20, 7, 2, 6, 0, 10, -18, -15, 5, -13, 17, 15, -1 })]
        [TestCase(new int[] { -14, 11, 19, 17, 1, 0, 3, 13, -11, -15, -2 })]
        [TestCase(new int[] { 16, 5, -8, -14, 18, 16, -7, -6, -15, -13, 6, -11, 5, -7, 0 })]
        [TestCase(new int[] { 1, -10, -20, 18, 15, 11, 4, 6, -10, 15, -1, -14, 18, -18, 5, 2, 18, -13 })]
        [TestCase(new int[] { 17, 9, -17, 5, 20, -4, 0, -19, 5, -1, 7, -14, 6, 9, 14, 18, -5, 13, -16, 9, -17 })]
        [TestCase(new int[] { -3, 13, -1, -10, -11, 18, 14 })]
        [TestCase(new int[] {1, 2, 3, 4, 5, 6})]
        public void Compare(int[] input)
        {
            Program.BubbleSort(input);
            for (var i = 0; i < input.Length - 1; i++)
            {
                if (input[i] > input[i + 1])
                {
                    Assert.IsTrue(input[i] <= input[i + 1]);
                }
            }
        }
    }
}

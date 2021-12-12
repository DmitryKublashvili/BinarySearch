using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BinarySearchTests
{
    public class Tests
    {
        private static IEnumerable<TestCaseData> MatchingStringsCase
        {
            get
            {
                yield return new TestCaseData(
                    new string[] { "alpha", "beta",  "delta", "gamma", "Z" },
                    new int[] { 0, 1, 2, 3, 4 });
                yield return new TestCaseData(
                    new string[] { "aaaa", "Aaaab",  "aaaba", "aabaa" },
                    new int[] { 0, 1, 2, 3 });
                yield return new TestCaseData(
                    new string[] { "", " ", "aaaab",  "aaaba", "aabaaz", "zZ aaa" },
                    new int[] { 0, 1, 2, 3, 4, 5 });
            }
        }

        private static IEnumerable<TestCaseData> NotMatchingStringsCase
        {
            get
            {
                yield return new TestCaseData(
                    new string[] { "alpha", "beta",  "delta", "gamma", "Z" },
                    new string[] { "alapha", "betta",  " delta", "gama", "z" },
                    new int[] { -1, -1, -1, -1, -1 });
                yield return new TestCaseData(
                    new string[] { " ", "beta",  "delta", "gamma", "Z" },
                    new string[] { "", "Beta",  "deltA", "Gamma", "Z " },
                    new int[] { -1, -1, -1, -1, -1 });
            }
        }

        class ComparerForIntegers : IComparer<int>
        {
            public int Compare(int x, int y) => x.CompareTo(y);
        }

        class ComparerForStrings : IComparer<string>
        {
            public int Compare(string x, string y) => x.CompareTo(y);
        }

        [Test]
        public void ThrowingExceptionWhenIntegerSoursIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => BinarySearch.BinarySearch.Search(null, 8, new ComparerForIntegers()));
        }

        [Test]
        public void ThrowingExceptionWhenStringSoursIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => BinarySearch.BinarySearch.Search(null, "a", new ComparerForStrings()));
        }

        [Test]
        public void ThrowingExceptionWhenIntegerComparerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => BinarySearch.BinarySearch.Search(new[] { 1 }, 1, null));
        }

        [Test]
        public void ThrowingExceptionWhenStringComparerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => BinarySearch.BinarySearch.Search(new[] { "a" }, "a", null));
        }

        [TestCase(new[] { 1 }, ExpectedResult = new[] { 0 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, ExpectedResult = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
        [TestCase(new[] { int.MinValue, -2, 0, 4, 5, 6, 7, 8, 9, int.MaxValue }, ExpectedResult = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
        [TestCase(new[] { int.MinValue, -1029369, -123694, -2315, -981, -555, -302, -222, -111, 0 }, ExpectedResult = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
        public int[] IntegersSearchAllElemmentsTests(int[] source)
        {
            int[] actual = new int[source.Length];

            var comparer = new ComparerForIntegers();

            for (int i = 0; i < actual.Length; i++)
            {
                actual[i] = BinarySearch.BinarySearch.Search(source, source[i], comparer);
            }

            return actual;
        }

        [TestCaseSource(nameof(MatchingStringsCase))]
        public void StringsSearchAllElemmentsTests(string[] source, int[] expected)
        {
            int[] actual = new int[source.Length];

            var comparer = new ComparerForStrings();

            for (int i = 0; i < actual.Length; i++)
            {
                actual[i] = BinarySearch.BinarySearch.Search(source, source[i], comparer);
            }

            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(nameof(NotMatchingStringsCase))]
        public void StringsSearchNotMatchingElemmentsTests(string[] source, string[] search, int[] expected)
        {
            int[] actual = new int[search.Length];

            var comparer = new ComparerForStrings();

            for (int i = 0; i < actual.Length; i++)
            {
                actual[i] = BinarySearch.BinarySearch.Search(source, search[i], comparer);
            }

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { }, new[] { int.MinValue, int.MaxValue, 96985, 10, -5697, -1, 11, 888, -888, 9867599 }, ExpectedResult = new[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 })]
        [TestCase(new[] { 0 }, new[] { int.MinValue, int.MaxValue, 96985, 10, -5697, -1, 11, 888, -888, 9867599 }, ExpectedResult = new[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new[] { int.MinValue, int.MaxValue, 96985, 0, -5697, -1, 11, 888, -888, 9867599 }, ExpectedResult = new[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 })]
        [TestCase(new[] { 999, -999, int.MinValue + 1, int.MaxValue - 1 }, new[] { int.MinValue, int.MaxValue, 96985, 0, -5697, -1, 11, 888, -888, 9867599 }, ExpectedResult = new[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 })]
        public int[] IntegersSearchAbsentElemmentsTests(int[] source, int[] serchElements)
        {
            int[] actual = new int[serchElements.Length];

            var comparer = new ComparerForIntegers();

            for (int i = 0; i < actual.Length; i++)
            {
                actual[i] = BinarySearch.BinarySearch.Search(source, serchElements[i], comparer);
            }

            return actual;
        }
    }
}
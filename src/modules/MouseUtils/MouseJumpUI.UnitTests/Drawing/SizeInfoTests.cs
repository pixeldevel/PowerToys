﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MouseJumpUI.Drawing.Models;

namespace MouseJumpUI.UnitTests.Drawing;

public sealed class SizeInfoTests
{
    [TestClass]
    public class ScaleToFitTests
    {
        public class TestCase
        {
            public TestCase(SizeInfo obj, SizeInfo bounds, SizeInfo expectedResult)
            {
                this.Obj = obj;
                this.Bounds = bounds;
                this.ExpectedResult = expectedResult;
            }

            public SizeInfo Obj { get; set; }

            public SizeInfo Bounds { get; set; }

            public SizeInfo ExpectedResult { get; set; }
        }

        public static IEnumerable<object[]> GetTestCases()
        {
            // identity tests
            yield return new[] { new TestCase(new(512, 384), new(512, 384), new(512, 384)), };
            yield return new[] { new TestCase(new(1024, 768), new(1024, 768), new(1024, 768)), };

            // general tests
            yield return new[] { new TestCase(new(512, 384), new(2048, 1536), new(2048, 1536)), };
            yield return new[] { new TestCase(new(2048, 1536), new(1024, 768), new(1024, 768)), };

            // scale to fit width
            yield return new[] { new TestCase(new(512, 384), new(2048, 3072), new(2048, 1536)), };

            // scale to fit height
            yield return new[] { new TestCase(new(512, 384), new(4096, 1536), new(2048, 1536)), };
        }

        [TestMethod]
        [DynamicData(nameof(GetTestCases), DynamicDataSourceType.Method)]
        public void RunTestCases(TestCase data)
        {
            var actual = data.Obj.ScaleToFit(data.Bounds);
            var expected = data.ExpectedResult;
            Assert.AreEqual(expected.Width, actual.Width);
            Assert.AreEqual(expected.Height, actual.Height);
        }
    }

    [TestClass]
    public class ScaleToFitRatioTests
    {
        public class TestCase
        {
            public TestCase(SizeInfo obj, SizeInfo bounds, decimal expectedResult)
            {
                this.Obj = obj;
                this.Bounds = bounds;
                this.ExpectedResult = expectedResult;
            }

            public SizeInfo Obj { get; set; }

            public SizeInfo Bounds { get; set; }

            public decimal ExpectedResult { get; set; }
        }

        public static IEnumerable<object[]> GetTestCases()
        {
            // identity tests
            yield return new[] { new TestCase(new(512, 384), new(512, 384), 1), };
            yield return new[] { new TestCase(new(1024, 768), new(1024, 768), 1), };

            // general tests
            yield return new[] { new TestCase(new(512, 384), new(2048, 1536), 4), };
            yield return new[] { new TestCase(new(2048, 1536), new(1024, 768), 0.5M), };

            // scale to fit width
            yield return new[] { new TestCase(new(512, 384), new(2048, 3072), 4), };

            // scale to fit height
            yield return new[] { new TestCase(new(512, 384), new(4096, 1536), 4), };
        }

        [TestMethod]
        [DynamicData(nameof(GetTestCases), DynamicDataSourceType.Method)]
        public void RunTestCases(TestCase data)
        {
            var actual = data.Obj.ScaleToFitRatio(data.Bounds);
            var expected = data.ExpectedResult;
            Assert.AreEqual(expected, actual);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity1week202408.Anomaly;
using UnityEngine;
using UnityEngine.TestTools;

namespace Unity1week202408.Tests
{
    public class AnomalyLotteryCalculatorTest
    {
        [Test]
        public void 異変確率がおおよそ指定の通り()
        {
            var calculator = new AnomalyLotteryCalculator();
            // 30%の確率で異変が発生する
            calculator.Initialize(Enumerable.Range(1, 10).Select(i => new AnomalyId(i)), 30);

            var count = 0;
            for (var i = 0; i < 1000; i++)
            {
                if (calculator.Lottery() != AnomalyId.Empty)
                {
                    count++;
                }
            }

            TestContext.WriteLine($"count: {count}");
            // おおよそ50前後のブレ
            Assert.That(count, Is.EqualTo(300).Within(50));
        }
    }
}
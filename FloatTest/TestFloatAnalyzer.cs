using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Floats;
using NUnit.Framework;

namespace FloatTest {
    [TestFixture]
    public class TestFloatAnalyzer {


        [Test]
        public void ZeroByte() {
            var analyzer = new FloatAnalyzer();
            var bits = analyzer.ByteToBits(0x00).ToArray();
            Assert.IsFalse(bits.Any(x => x == Bit.One));


        }

        [Test]
        public void ByteToBits1() {
            var analyzer = new FloatAnalyzer();
            var bits = analyzer.ByteToBits(0x01).ToArray();
            Assert.IsTrue(bits.Last() == Bit.One);
            Assert.IsTrue(bits.Take(7).All(x => x != Bit.One));
        }

        [Test]
        public void ByteToBits2() {
            var analyzer = new FloatAnalyzer();
            var bits = analyzer.ByteToBits(0x02).ToArray();
            Assert.IsTrue(bits[6] == Bit.One);
            Assert.IsFalse(bits.Take(6).Any(x => x == Bit.One));
        }

        [Test]
        public void ByteToBitsMax() {
            var analyzer = new FloatAnalyzer();
            var bits = analyzer.ByteToBits(0x80).ToArray();
            Assert.IsTrue(bits.First() == Bit.One);
            Assert.IsFalse(bits.Skip(1).Any(x => x == Bit.One));
        }

        [Test]
        public void ByteToBitsAll() {
            var analyzer = new FloatAnalyzer();
            var bits = analyzer.ByteToBits(0xFF).ToArray();
            Assert.IsTrue(bits.All(x => x != Bit.Zero));
        }
    }
}

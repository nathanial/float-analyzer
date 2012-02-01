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

        [Test]
        public void CheckByteOrder() {
            var analyzer = new FloatAnalyzer();
            var d = new FloatDescription {
                BitCount = 8,
                ExponentBias = 0,
                ExponentBits = 3,
                SignificandBits = 4
            };
            
            var value = analyzer.FromBytes(d,0x00);
            Assert.AreEqual(0,value);

            value = analyzer.FromBytes(d,0x01);
            Assert.IsTrue(value > 0);

            value = analyzer.FromBytes(d, 0x82);
            Assert.IsTrue(value < 0);
        }

        [Test]
        public void CheckByteOrder2() {
            var analyzer = new FloatAnalyzer();
            var d = new FloatDescription {
                BitCount = 9,
                ExponentBias = 0,
                ExponentBits = 4,
                SignificandBits = 4
            };

            var value = analyzer.FromBytes(d, 0x00, 0x00);
            Assert.AreEqual(0,value);

            value = analyzer.FromBytes(d, 0x00, 0x80);
            Assert.IsTrue(value > 0);

            value = analyzer.FromBytes(d, 0x82, 0x00);
            Assert.IsTrue(value < 0);
        }

    }
}

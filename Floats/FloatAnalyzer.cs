using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Floats {

    public class FloatDescription {
        public int BitCount { get; set; }
        public int ExponentBits { get; set; }
        public int SignificandBits { get; set; }
        public int ExponentBias { get; set; }
    }

    public enum Bit {
        Zero = 0,
        One = 1
    }

    public class FloatParts {
        public Bit Sign { get; set; }
        public Bit[] Exponent { get; set; }
        public Bit[] Fraction { get; set; }
    }

    public class FloatAnalyzer {
        public IEnumerable<float> Enumerate(FloatDescription desc) {
            var max = Math.Pow(2, desc.BitCount);
            for (var i = 0; i < max; i++) {
                var bytes = BitConverter.GetBytes(i);
                var byteCount = desc.BitCount/8;
                var remainder = desc.BitCount%8;
                if(remainder > 0) {
                    byteCount += 1;
                }
                var newBytes = bytes.Take(byteCount).Reverse().ToArray();
                var bits = newBytes.SelectMany(ByteToBits).Reverse().Take(desc.BitCount).ToArray();
                var floatParts = ToFloatParts(desc, bits);
                var value = FromFloatParts(desc, floatParts);
                yield return value;
            }
        }

        public IEnumerable<Bit> ByteToBits(byte b) {
            var bits = new List<Bit>();
            for (var i = 0; i < 8; i++) {
                bits.Add(ByteToBit(i, b));
            }
            bits.Reverse();
            return bits;
        }

        public Bit ByteToBit(int i, byte b) {
            var x = (byte)Math.Pow(2, i);
            var and = x & b;
            return and > 0 ? Bit.One : Bit.Zero;
        }

        public FloatParts ToFloatParts(FloatDescription desc, Bit[] bits) {
            if(bits.Length != desc.BitCount) {
                throw new Exception("not enough bits");
            }
            var e = new List<Bit>();
            var f = new List<Bit>();
            if(desc.ExponentBits > 0) {
                e = bits.Skip(1).Take(desc.ExponentBits).ToList();
            } 
            if(desc.SignificandBits > 0) {
                f = bits.Skip(1 + desc.ExponentBits).ToList();
            }
            var parts = new FloatParts {
                Sign = bits[0],
                Exponent = e.ToArray(),
                Fraction = f.ToArray()
            };
            return parts;
        }

        public float FromFloatParts(FloatDescription desc, FloatParts parts) {
            var isZero = true;
            var sign = parts.Sign > 0 ? -1 : 1;
            var e = BitsToInt(parts.Exponent) - desc.ExponentBias;
            if (e != 0) {
                isZero = false;
            }
            var exp = Math.Pow(2.0, e);
            double frac = 0;
            frac += 1;
            for (var i = 0; i < desc.SignificandBits; i++) {
                if (parts.Fraction[i] == Bit.One) {
                    frac += Math.Pow(2.0, -(i + 1));
                    isZero = false;
                }
            }
            if (isZero) {
                return 0;
            }
            return (float)(sign * frac * exp);
        }

        public int BitsToInt(Bit[] bits) {
            var value = 0;
            foreach (var b in bits) {
                value <<= 1;
                if (b == Bit.One) {
                    value |= 0x01;
                } else {
                    value &= ~(0x01);
                }
            }
            return value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Floats {

    public class FloatDescription {
        public double BitCount { get; set; }
    }

    public enum Bit {
        Zero,
        One
    }

    public class FloatParts{}

    public class FloatAnalyzer {
        public IEnumerable<float> Enumerate(FloatDescription desc) {
            var max = Math.Pow(2, desc.BitCount);
            for(var i = 0 ; i < max; i++) {
                var bytes = BitConverter.GetBytes(i);
                var bits = bytes.SelectMany(ByteToBits).ToArray();
                var floatParts = ToFloatParts(desc, bits);
                var value = FromFloatParts(desc, floatParts);
                yield return value;
            }
        }

        public IEnumerable<Bit> ByteToBits(byte b) {
            var bits = new List<Bit>();
            for(var i = 0; i < 8; i++) {
                bits.Add(ByteToBit(i,b));
            }
            bits.Reverse();
            return bits;
        }

        public Bit ByteToBit(int i, byte b) {
            var x = (byte) Math.Pow(2, i);
            var and = x & b;
            return and > 0 ? Bit.One : Bit.Zero;
        }

        public FloatParts ToFloatParts(FloatDescription desc, Bit[] bits) {
            throw new NotImplementedException();
        }

        public float FromFloatParts(FloatDescription desc, FloatParts floatParts) {
            throw new NotImplementedException();            
        }


    }
}

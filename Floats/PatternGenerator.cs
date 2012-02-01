using System;
using System.Collections.Generic;
using System.Linq;

namespace Floats {
    public class Pattern {
        public int V1 { get; set; }
        public int V2 { get; set; }
        public int V3 { get; set; }
        public Range Range { get; set; }

        public byte[] GetBytes() {
            var vcount = (Range.Max - Range.Min) / Range.Step;
            var intValue = Adjust(V1) * (vcount * vcount) + Adjust(V1) * vcount + Adjust(V3);
            var bytes = BitConverter.GetBytes(intValue).Reverse();
            return bytes.Skip(1).ToArray();
        }

        int Adjust(int v) {
            return (v - Range.Min) / Range.Step;
        }

        public override string ToString() {
            return string.Format("{0} {1} {2}", V1, V2, V3);
        }

    }

    public class Range {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Step { get; set; }
    }

    public class PatternGen {
        public int Sum { get; set; }
        public Range Range { get; set; }

        public IList<Pattern> Generate() {
            var s1 = Enumerable.Range(0, Sum)
                               .Where(x => x <= Range.Max && x >= Range.Min && ((x - Range.Min) % Range.Step == 0))
                               .ToArray();

            var patterns = new List<Pattern>();
            foreach (var n1 in s1) {
                var v1 = n1;
                var s2 = s1.ToArray();
                foreach (var n2 in s2) {
                    var v2 = n2;
                    var s3 = s2.ToArray();
                    foreach (var n3 in s3) {
                        var v3 = n3;
                        patterns.Add(new Pattern {
                            V1 = v1,
                            V2 = v2,
                            V3 = v3,
                            Range = Range
                        });
                    }
                }
            }
            return patterns;
        }

    }

}
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Floats;
using MessageBox = Microsoft.Windows.Controls.MessageBox;

namespace Float_Analyzer {
    public partial class MainWindow {
        string _delimiter = "\n";

        public MainWindow() {
            InitializeComponent();
            if (DesignerProperties.GetIsInDesignMode(this)) return;


        }

        void CalculateClick(object sender, RoutedEventArgs e) {
            var analyzer = new FloatAnalyzer();
            var values = analyzer.Enumerate(CreateFloatDescription()).ToArray();
            Array.Sort(values);
            output.Clear();
            var lcount = int.Parse(perLine.SelectionBoxItem.ToString());
            var i = 1;
            foreach (var v in values) {
                output.AppendText(v.ToString().PadRight(15));
                output.AppendText(_delimiter);
                if (_delimiter != "\n" && i % lcount == 0) {
                    output.AppendText("\r\n");
                }
                i++;
            }
            output.ScrollToEnd();
        }

        void AboutClick(object sender, RoutedEventArgs e) {
            MessageBox.Show("EM Float Analyzer 1.0", "Version", MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        void ExitClick(object sender, RoutedEventArgs e) {
            Close();
        }

        void BitsValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            if (exponent == null || fraction == null || bits == null) return;

            if (bits.Value.Value < 1) {
                bits.Value = 1;
            }

            if (bits.Value.Value > Total) {
                exponent.Value += 1;
            } else if (bits.Value.Value < Total) {
                exponent.Value -= 1;
            }
        }

        int Total {
            get { return 1 + exponent.Value.Value + fraction.Value.Value; }
        }

        void FractionValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            if (exponent == null || fraction == null || bits == null) return;
            if (fraction.Value == null) return;
            var v = fraction.Value.Value;
            if (v < 0) {
                fraction.Value = 0;
            }
            if (Total > bits.Value.Value) {
                if (exponent.Value.Value > 0) {
                    exponent.Value -= 1;
                } else if (fraction.Value.Value > 0) {
                    fraction.Value -= 1;
                }
            }
            if (Total < bits.Value.Value) {
                exponent.Value += 1;
            }
        }

        void ExponentValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            if (exponent == null || fraction == null || bits == null) return;
            var v = exponent.Value.Value;
            if (v < 0) {
                exponent.Value = 0;
            }
            if (Total > bits.Value.Value) {
                if (fraction.Value.Value > 0) {
                    fraction.Value -= 1;
                } else if (exponent.Value.Value > 0) {
                    exponent.Value -= 1;
                }
            }
            if (Total < bits.Value.Value) {
                fraction.Value += 1;
            }
        }

        void OutputRadioButtonClick(object sender, RoutedEventArgs e) {
            if (sender == newLine) {
                _delimiter = "\n";
            } else if (sender == space) {
                _delimiter = " ";
            } else if (sender == comma) {
                _delimiter = ",";
            }
        }

        void GeneratePatternsClick(object sender, RoutedEventArgs e) {
            var minValue = min.Value.Value;
            var maxValue = max.Value.Value;
            var stepValue = increment.Value.Value;
            var sumValue = sum.Value.Value;
            var range = new Range {
                Min = minValue,
                Max = maxValue,
                Step = stepValue
            };
            var pgen = new PatternGen {
                Range = range,
                Sum = sumValue,
            };
            var desc = CreateFloatDescription();
            var analyzer = new FloatAnalyzer();

            output.Clear();

            //var patterns = pgen.Generate().OrderBy(v => analyzer.FromIntegerValue(desc,v.GetBytes()));
            var patterns = pgen.Generate();
            foreach (var p in patterns) {
                var bcount = desc.BitCount / 8;
                if (desc.BitCount % 8 != 0) {
                    bcount += 1;
                }
                var floatValue = analyzer.FromBytes(desc, p.GetBytes().Take(bcount).ToArray());
                output.AppendText(string.Format("{0} {1}", p, floatValue));
                output.AppendText("\n");
            }
        }

        FloatDescription CreateFloatDescription() {
            return new FloatDescription {
                BitCount = bits.Value.Value,
                ExponentBias = expBias.Value.Value,
                ExponentBits = exponent.Value.Value,
                SignificandBits = fraction.Value.Value
            };
        }

    }
}

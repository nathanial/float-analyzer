using System.ComponentModel;
using System.Windows;
using Floats;
using MessageBox = Microsoft.Windows.Controls.MessageBox;

namespace Float_Analyzer {
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            if (DesignerProperties.GetIsInDesignMode(this)) return;


        }

        void CalculateClick(object sender, RoutedEventArgs e) {
            var analyzer = new FloatAnalyzer();
            var values = analyzer.Enumerate(new FloatDescription {
                BitCount = 8,
                ExponentBias = 4,
                ExponentBits = 3,
                SignificandBits = 4
            });
            foreach(var v in values) {
                output.AppendText(v.ToString());
                output.AppendText("\n");
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
    }
}

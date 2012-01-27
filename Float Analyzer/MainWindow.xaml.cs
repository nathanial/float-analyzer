using System.ComponentModel;
using System.Windows;
using MessageBox = Microsoft.Windows.Controls.MessageBox;

namespace Float_Analyzer {
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            if (DesignerProperties.GetIsInDesignMode(this)) return;


        }

        void CalculateClick(object sender, RoutedEventArgs e) {
        }

        void AboutClick(object sender, RoutedEventArgs e) {
            MessageBox.Show("Erdos Miller Float Analyzer: 1.0", "Version", MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        void ExitClick(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}

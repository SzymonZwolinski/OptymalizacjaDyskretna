using AlgorytmWegierski.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlgorytmWegierski
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLos_Click(object sender, RoutedEventArgs e)
        {
            AlgorytmWegierski.View.MatrixMainView window = new AlgorytmWegierski.View.MatrixMainView();
            MatrixVM matrixvm = new MatrixVM();
            window.Show();
            matrixvm.losowo(4,4);
            window.DataContext = matrixvm;
            
        }
    }
}

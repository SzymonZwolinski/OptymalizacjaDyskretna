using AlgorytmWegierski.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
using System.Windows.Shapes;

namespace AlgorytmWegierski.View
{
    /// <summary>
    /// Logika interakcji dla klasy MatrixMainView.xaml
    /// </summary>
    public partial class MatrixMainView : Window
    {
        private MatrixVM matrixVM = new MatrixVM();
        public MatrixMainView()
        {
            InitializeComponent();


        }

        private void Window_Activated(object sender, EventArgs e)
        {

            MatrixVM matrixVM = new MatrixVM();

            this.DataContext = matrixVM;

            var okniarz = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            matrixVM.GetMatrixToGrid(okniarz);
            matrixVM.AlgorytmoHungaro();



        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            matrixVM.AlgorytmoHungaro();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using AlgorytmWegierski.Model;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.Windows;
using AlgorytmWegierski.View;

namespace AlgorytmWegierski.ViewModel
{
    internal class MatrixVM
    {
        private IList<Matrix> _MatrixContent;

        public MatrixVM()
        {
            _MatrixContent = new List<Matrix>
            {
                new Matrix{NrId = 1, RowId=0,ColumnId=0, Number=10 },
                new Matrix{NrId = 2, RowId=0,ColumnId=1, Number=1 },
                new Matrix{NrId =3 , RowId=1, ColumnId=1, Number=9 }
            };
        }

        public IList<Matrix> Matrix
        {
            get { return _MatrixContent; }
            set { _MatrixContent = value; }
        }

        #region Resizing
        protected static void getColumsAndRows(IList<Matrix> myMatrix, out Matrix? columns, out Matrix? rows)
        {
            columns = myMatrix.OrderByDescending(x => x.ColumnId).FirstOrDefault();
            rows = myMatrix.OrderByDescending(x => x.RowId).FirstOrDefault();
        }

        protected void resizeColumn(Grid grid)
        {
            ColumnDefinition myColumn = new ColumnDefinition();
            grid.ColumnDefinitions.Add(myColumn);
        }

        protected void resizeRow(Grid grid)
        {
            RowDefinition myRow = new RowDefinition();
            grid.RowDefinitions.Add(myRow);

        }

        protected Matrix getNumberToMatrix(IList<Matrix> myMatrix, int id)
        {
            return myMatrix.FirstOrDefault(x => x.NrId == id);
        }
        #endregion

        public void GetMatrixToGrid(Window myWindow )
        {
            Matrix? columns, rows;
            getColumsAndRows(_MatrixContent, out columns, out rows);

            if (columns != null || rows != null)
            {
                Grid myGrid = new Grid();
                myGrid.ShowGridLines = true;

                for (int i = 0; i < columns.ColumnId; i++)
                {
                    resizeColumn(myGrid);
                }

                for (int i = 0; i < rows.RowId; i++)
                {
                    resizeRow(myGrid);
                }
                var asd = getNumberToMatrix(_MatrixContent, 1);
                TextBlock txt1 = new TextBlock();
                txt1.Text =  asd.Number.ToString();
                txt1.FontSize = 20;
                txt1.FontWeight = FontWeights.Bold;
                Grid.SetColumnSpan(txt1, 3);
                Grid.SetRow(txt1, 0);
                myGrid.Children.Add(txt1);
                myWindow.Content = myGrid;
                myWindow.Show();
            }

            
        }
    


    private ICommand mUpdater;
        public ICommand UpdateCommand
        {
            get
            {
                if (mUpdater == null)
                    mUpdater = new Updater();
                return mUpdater;
            }
            set
            {
                mUpdater = value;
            }
        }

        private class Updater : ICommand
        {
            #region ICommand Members  

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {

            }

            #endregion
        }
    }
}

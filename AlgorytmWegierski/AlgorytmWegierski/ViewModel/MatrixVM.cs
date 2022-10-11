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
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AlgorytmWegierski.ViewModel
{
    internal class MatrixVM
    {
        private IList<Matrix> _MatrixContent;

        public MatrixVM()
        {
            _MatrixContent = new List<Matrix>();
            
        }

        public IList<Matrix> Matrix
        {
            get { return _MatrixContent; }
            set { _MatrixContent = value; }
        }

        #region wpisywanie danych
        Random rnd = new Random();
        static int id = 0;
        private int losowaLiczba()
        {
            return rnd.Next(0, 20);
        }
        public void losowo(int x, int y)
        {
             for(int i = 0 ; i <= y; i++) // y kolumny
            {
                for(int j =0; j<=x; j++) //x rzad
                {
                    _MatrixContent.Add(new Matrix { NrId= id, ColumnId=j,RowId= i, Number=losowaLiczba() });
                    id++;
                }
            }
        }


        public void zPliku(string nazwa)
        {
            try
            {
                using (var sr = new StreamReader(nazwa))
                {
                    string result;
                    int index;
                    string str;
                    List<string> numbersFromFile = new List<string>();
                    Console.WriteLine(sr.ReadToEnd());
                    str = sr.ToString();
                    
                    for (int i = 0; i <= str.Length;)
                    {
                        index = str.IndexOf(' ');
                        if (index < 0)
                        {
                            result = str;
                            numbersFromFile.Add(result);
                            break;
                        }
                        else
                        {
                            result = str.Substring(0, index);
                            numbersFromFile.Add(result);
                            str = str.Remove(0, index + 1);
                        }
                    }

                    for(int i =0; i<=Math.Sqrt(numbersFromFile.Count);i++)
                    {
                        for(int j=0;j<Math.Sqrt(numbersFromFile.Count);j++)
                        {
                            _MatrixContent.Add(new Matrix { NrId = id, ColumnId = j, RowId = i, Number = int.Parse( numbersFromFile[id]) });
                            id++;
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("bład:");
                Console.WriteLine(e.Message);
            }
        }

        public void zReki(string str)
        {
            string result;
            int index;
            List<string> numbersFromFile = new List<string>();
            for (int i = 0; i <= str.Length;)
            {
                index = str.IndexOf(' ');
                if (index < 0)
                {
                    result = str;
                    numbersFromFile.Add(result);
                    break;
                }
                else
                {
                    result = str.Substring(0, index);
                    numbersFromFile.Add(result);
                    str = str.Remove(0, index + 1);
                }
            }

            for (int i = 0; i <= Math.Sqrt(numbersFromFile.Count); i++)
            {
                for (int j = 0; j < Math.Sqrt(numbersFromFile.Count); j++)
                {
                    _MatrixContent.Add(new Matrix { NrId = id, ColumnId = j, RowId = i, Number = int.Parse(numbersFromFile[id]) });
                    id++;
                }
            }
        }
    


         






        #endregion

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

        protected Matrix getNumberOfElements(IList<Matrix> myMatrix)
        {
            return myMatrix.OrderByDescending(x => x.NrId).FirstOrDefault();
        }

        protected void addNumberToMatrix(Matrix myCurrentElement, Grid myGrid)
        {
            if (myCurrentElement != null)
            {
                Trace.WriteLine(myCurrentElement.NrId);

                TextBlock txt = new TextBlock();
                txt.Text = myCurrentElement.Number.ToString();
                txt.TextAlignment = TextAlignment.Center;
                txt.Padding = new Thickness(5, 10, 5, 10);
                Grid.SetColumn(txt, myCurrentElement.ColumnId);
                Grid.SetRow(txt, myCurrentElement.RowId);
                myGrid.Children.Add(txt);
            }
        }
        protected void replaceNumberToMatrix(Matrix myCurrentElement, Grid myGrid)
        {
            if (myCurrentElement != null)
            {
                Trace.WriteLine(myCurrentElement.NrId);

                TextBlock txt = new TextBlock();
                txt.Text = myCurrentElement.Number.ToString();
                txt.TextAlignment = TextAlignment.Center;
                txt.Padding = new Thickness(5, 10, 5, 10);
                Grid.SetColumn(txt, myCurrentElement.ColumnId);
                Grid.SetRow(txt, myCurrentElement.RowId);
                myGrid.Children.Add(txt);
            }
        }

        #endregion
        public Grid myGrid = new Grid(); // Globalny grid

        public void GetMatrixToGrid(Window myWindow )
        {
            Matrix? columns, rows;
            Matrix currentMatrixContent;
            getColumsAndRows(_MatrixContent, out columns, out rows);
            int? elements = getNumberOfElements(_MatrixContent).NrId;


            if (columns != null || rows != null)
            {

                myGrid.Margin = new Thickness(0,0,0,0);
                myGrid.ShowGridLines = true;

                for (int i = 0; i <= columns.ColumnId; i++)
                {
                    resizeColumn(myGrid);
                }

                for (int i = 0; i <= rows.RowId; i++)
                {
                    resizeRow(myGrid);
                }

                for (int i = 0; i <= elements; i++)
                {
                    
                    currentMatrixContent = getNumberToMatrix(_MatrixContent, i);
                    addNumberToMatrix(currentMatrixContent, myGrid);
                }
               
                myWindow.Content = myGrid;
                myWindow.Show();
            }

            
        }

     

        #region Algorytm Wegierski
        /*
        private Matrix getFromRow(IList<Matrix> myMatrix, int row)
        {
            return myMatrix.Where(x => x.RowId == row).FirstOrDefault();
        }

        private Matrix getFromColumn(IList<Matrix> myMatrix, int column)
        {
            return myMatrix.Where(x => x.ColumnId == column).FirstOrDefault();
        }
        */
        public void AlgorytmoHungaro()
        {
            AlgorytmWegierskiKrokPierwszy(_MatrixContent, myGrid);
        }
        protected void AlgorytmWegierskiKrokPierwszy(IList<Matrix> myMatrix, Grid myGrid)
        {
            Matrix? rows, columns;
            List<Matrix> tmpList = new List<Matrix>();
            int lowest;
            getColumsAndRows(myMatrix,out columns,out rows);
           for(int i =0; i <= rows.RowId; i=i+1)
           {
                
                lowest = myMatrix.Where(x => x.RowId == i).Min(v => v.Number);

                myMatrix.Where(x => x.RowId == i).ToList().ForEach(s =>  s.Number = s.Number - lowest );

                myMatrix.Where(x => x.RowId == i).ToList().ForEach(f => addNumberToMatrix(f, myGrid));
           }
           for(int i =0; i<= columns.ColumnId;i=i+1)
            {
                
                lowest = myMatrix.Where(x => x.ColumnId == i).Min(v => v.Number);

                myMatrix.Where(x => x.ColumnId == i).ToList().ForEach(s => s.Number = s.Number - lowest);
                myMatrix.Where(x => x.ColumnId == i).ToList().ForEach(f => addNumberToMatrix(f, myGrid));
            }
        }

        


        #endregion

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

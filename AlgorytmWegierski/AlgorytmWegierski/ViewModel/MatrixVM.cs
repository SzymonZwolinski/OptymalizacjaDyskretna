using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using AlgorytmWegierski.Model;
using System.Security.Cryptography;

namespace AlgorytmWegierski.ViewModel
{
    internal class MatrixVM
    {
        private IList<Matrix> _MatrixContent;

        public MatrixVM()
        {
            _MatrixContent = new List<Matrix>
            {
                new Matrix{NrId = 1, RowId=1,ColumnId=1, Number=10 },
                new Matrix{NrId = 2, RowId=1,ColumnId=2, Number=1 },
                new Matrix{NrId =3 , RowId=2, ColumnId=2, Number=9 },
            };
        }

        public IList<Matrix> Matrix
        {
            get { return _MatrixContent; }
            set { _MatrixContent = value; }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AlgorytmWegierski.Model
{
    public class Matrix : INotifyPropertyChanged
    {
        private int nrId;
        private int rowId;
        private int columnId;
        private int number;

        public int NrId { get { return nrId; } set { nrId = value; OnPropertyChanged("NrId"); } }
        public int RowId { get { return rowId; } set { rowId = value; OnPropertyChanged("RowId"); } }
        public int ColumnId { get { return columnId; } set { columnId = value; OnPropertyChanged("ColumnId"); } }
        public int Number { get { return number; } set { number = value; OnPropertyChanged("Number"); } }

        #region INotifyPropertyChanged Members  

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}

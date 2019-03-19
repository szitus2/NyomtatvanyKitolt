using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;


namespace NyomtatvanyKitolto
{
    public class PrinterSettings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string pName = "Default";
        public string Name
        {
            get { return pName; }
            set { pName = value; OnPropertyChanged("Name"); }
        }

        private string pDescription = "";
        public string Description
        {
            get { return pDescription; }
            set { pDescription = value; OnPropertyChanged("Description"); }
        }

        private Double pXOffset = 0;
        public Double XOffset
        {
            get { return pXOffset; }
            set { pXOffset = value; OnPropertyChanged("XOffset"); }
        }

        private Double pYOffset = 0;
        public Double YOffset
        {
            get { return pYOffset; }
            set { pYOffset = value; OnPropertyChanged("YOffset"); }
        }

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}

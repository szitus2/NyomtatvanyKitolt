using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;

namespace NyomtatvanyKitolto
{
    [Serializable]
    [XmlInclude(typeof(StringDataGroup))]
    [XmlInclude(typeof(CheckDataGroup))]
    public class DataGroup : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string pname;
        public string Name
        {
            get { return pname; }
            set { pname = value; OnPropertyChanged("Name"); }
        }

        private Point pscreenPosition;
        public Point ScreenPosition {
            get { return pscreenPosition; }
            set { pscreenPosition = value; OnPropertyChanged("ScreenPosition"); }
        }

        private Point pprintPosition;
        public Point PrintPosition {
            get { return pprintPosition; }
            set { pprintPosition = value; OnPropertyChanged("PrintPosition"); }
        }

        private string pfontName = "Arial";
        public string FontName
        {
            get { return pfontName; }
            set { pfontName = value; OnPropertyChanged("FontName"); }
        }

        private double pfontScale = 1;
        public double FontScale
        {
            get { return pfontScale; }
            set { pfontScale = value; OnPropertyChanged("FontScale"); }
        }

        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}

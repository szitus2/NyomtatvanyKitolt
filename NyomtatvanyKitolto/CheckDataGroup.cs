using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyomtatvanyKitolto
{
    [Serializable]
    public class CheckDataGroup : DataGroup
    {
        private string pcheckString = "X";
        public string CheckString
        {
            get { return pcheckString; }
            set { pcheckString = value; OnPropertyChanged("CheckString"); }
        }

        private bool pchecked = true;
        public bool Checked {
            get { return pchecked; }
            set { pchecked = value; OnPropertyChanged("Checked"); }
        }
    }
}

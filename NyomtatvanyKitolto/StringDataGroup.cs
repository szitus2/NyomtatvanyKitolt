using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyomtatvanyKitolto
{
    [Serializable]
    public class StringDataGroup : DataGroup
    {
        private string ptbString;
        public string TbString {
            get { return ptbString; }
            set { ptbString = value; OnPropertyChanged("TbString"); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace NyomtatvanyKitolto
{
    public class DocumentDescription
    {
        public string Name { get; set; }
        public string PrintOrientation = "Portrait";
        public string GraphicName;        
        public BitmapImage Graphic
        { 
            get
            {
                return Nykdocs.Nykdocs.GetBitmapImage(GraphicName);
                
            }
        }
        public string XmlFileName;
    }
}

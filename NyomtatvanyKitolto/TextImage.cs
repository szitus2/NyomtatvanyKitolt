using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace NyomtatvanyKitolto
{
    public class TextImage : Image
    {
        private Dictionary<string, DataGroup> pdataGroups;
        public Dictionary<string, DataGroup> DataGroups
        {
            get { return pdataGroups; }
            set { pdataGroups = value; setEvent(); }
        }

        private void setEvent()
        {
            foreach (KeyValuePair<string, DataGroup> kvp in DataGroups)
            {
                kvp.Value.PropertyChanged += Value_PropertyChanged;
            }
        }

        private void Value_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (DataGroups == null)
            {
                return;
            }
            foreach (KeyValuePair<string, DataGroup> kvp in DataGroups)
            {
                double k = 1.2;
                double xs = this.ActualWidth / 100;
                double ys = this.ActualHeight / 100;
                double fs = Convert.ToInt32(this.ActualHeight / 30) * k;
                
                StringDataGroup sdg = kvp.Value as StringDataGroup;
                if (sdg != null)
                {
                    string s = sdg.TbString ?? "";
                    FormattedText ft = new FormattedText(s, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(sdg.FontName), fs * sdg.FontScale, new SolidColorBrush(Colors.Black));
                    dc.DrawText(ft, new Point(sdg.ScreenPosition.X * xs, sdg.ScreenPosition.Y * ys));
                    /*dc.DrawLine(new Pen(new SolidColorBrush(Colors.Black), 1),
                        new Point(sdg.ScreenPosition.X * xs, sdg.ScreenPosition.Y * ys + ft.Baseline),
                        new Point(sdg.ScreenPosition.X * xs + ft.Width, sdg.ScreenPosition.Y * ys + ft.Baseline));//*/
                }
                CheckDataGroup cdg = kvp.Value as CheckDataGroup;
                if (cdg != null)
                {
                    string s = cdg.CheckString ?? "X";
                    if (cdg.Checked)
                    {
                        FormattedText ft = new FormattedText(s, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(cdg.FontName), fs * cdg.FontScale, new SolidColorBrush(Colors.Black));
                        dc.DrawText(ft, new Point(cdg.ScreenPosition.X * xs, cdg.ScreenPosition.Y * ys));
                    }
                }
            }
            
            
        }
    }
}

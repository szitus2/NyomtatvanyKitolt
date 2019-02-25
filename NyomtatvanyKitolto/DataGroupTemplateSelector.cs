using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NyomtatvanyKitolto
{
    public class DataGroupTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StringDataGroupTemplate { get; set; }
        public DataTemplate CheckDataGroupTemplate { get; set; }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (item is StringDataGroup)
            {
                return StringDataGroupTemplate;
            }
            if (item is CheckDataGroup)
            {
                return CheckDataGroupTemplate;
            }
            return StringDataGroupTemplate;
        }
    }
}

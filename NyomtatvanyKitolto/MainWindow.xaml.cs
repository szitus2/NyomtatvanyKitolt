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
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Printing;


namespace NyomtatvanyKitolto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<DocumentDescription> Documents;
        public Dictionary<string, DataGroup> DataGroups;
        public SerializableDictionary<string, List<string>> Strings;

        public MainWindow()
        {
            InitializeComponent();
            LoadDocuments();
        }

        private void LoadDocuments()
        {
            FileStream fs = new FileStream(@"Xml\docs.xml", FileMode.Open);
            XmlSerializer xs = new XmlSerializer(typeof(List<DocumentDescription>));
            Documents = xs.Deserialize(fs) as List<DocumentDescription>;
            fs.Close();
            fs.Dispose();
            DocsList.ItemsSource = Documents;


            fs = new FileStream(@"Xml\strings.xml", FileMode.Open);
            xs = new XmlSerializer(typeof(SerializableDictionary<string, List<string>>));
            Strings = xs.Deserialize(fs) as SerializableDictionary<string, List<string>>;
            fs.Close();
            fs.Dispose();
            DocsList.ItemsSource = Documents;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           

            //DisplayDoc.Visibility = Visibility.Collapsed;

            /*List<DocumentDescription> documents = new List<DocumentDescription>();
            documents.Add(
                new DocumentDescription()
                {
                    Name = "Belföldi Tértivevény",
                    GraphicName = "btv",
                    XmlFileName = "default.xml"
                }
                );
            documents.Add(
                new DocumentDescription()
                {
                    Name = "Könyvelt Küldemény Feladólap",
                    GraphicName = "kkf",
                    XmlFileName = "default.xml"
                }
                );
            FileStream file = new FileStream("docs.xml", FileMode.Create);
            XmlSerializer xs = new XmlSerializer(typeof(List<DocumentDescription>));
            xs.Serialize(file, documents);
            file.Close();
            file.Dispose();//*/

            //DocsList.ItemsSource = Documents;

            /*List<DataGroup> dataGroups = new List<DataGroup>();
            StringDataGroup sdg = new StringDataGroup();
            sdg.Name = "Feladó";
            sdg.PrintPosition = new Point(10, 10);
            sdg.ScreenPosition = new Point(10, 10);
            sdg.TbString = "MWW Kft.\r\n1046 Budapest, Erdősor út 24.";
            dataGroups.Add(sdg);
            sdg = new StringDataGroup();
            sdg.Name = "Címzett";
            sdg.PrintPosition = new Point(20, 10);
            sdg.ScreenPosition = new Point(20, 10);
            dataGroups.Add(sdg);
            CheckDataGroup cdg = new CheckDataGroup();
            cdg.Name = "Tértivevény";
            cdg.PrintPosition = new Point(30.3, 23.5);
            cdg.ScreenPosition = new Point(30, 30);
            cdg.Checked = true;
            dataGroups.Add(cdg);

            FileStream file = new FileStream("dgs.xml", FileMode.Create);
            XmlSerializer xs = new XmlSerializer(typeof(List<DataGroup>));
            xs.Serialize(file, dataGroups);
            file.Close();
            file.Dispose();//*/

            /*Strings = new SerializableDictionary<string, List<string>>();
            List<string> ss = new List<string>();
            ss.Add("MWW Kft.\r\n1046 Budapest, Erdősor út 24.");
            Strings.Add("Feladó", ss);
            ss = new List<string>();
            ss.Add("Scadanet Kft.\r\n1113 Budapest, Cirmos u. 8.");
            ss.Add("Univer Zrt.\r\n6000 Kecskemét, Mindszenty körút 10.");
            ss.Add("Nestle Hungária Zrt.\r\nPénzügyi osztály\r\n1091 Budapest, Lechner Ödön fasor 23.");
            Strings.Add("Címzett", ss);

            FileStream file = new FileStream("strings.xml", FileMode.Create);
            XmlSerializer xs = new XmlSerializer(typeof(SerializableDictionary<string, List<string>>));
            xs.Serialize(file, Strings);
            file.Close();
            file.Dispose(); //*/


        }

        private void DocsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DocumentDescription selDoc = DocsList.SelectedItem as DocumentDescription;
            FileStream fs = new FileStream(@"Xml\" + selDoc.XmlFileName, FileMode.Open);
            XmlSerializer xs = new XmlSerializer(typeof(List<DataGroup>));
            List<DataGroup> dataGroups = xs.Deserialize(fs) as List<DataGroup>;
            fs.Close();
            fs.Dispose();
            DataGroupList.ItemsSource = dataGroups;
            TextBox tb = new TextBox();
            DataGroups = new Dictionary<string, DataGroup>();
            foreach (DataGroup dg in dataGroups)
            {
                DataGroups.Add(dg.Name, dg);
            }
            DisplayDoc.DataGroups = DataGroups;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            
            //MessageBox.Show("!"+b.Tag.ToString()+"!");
            StringDataGroup sdg = DataGroups[b.Tag.ToString()] as StringDataGroup;
            StringSelectorWindow ssw = new StringSelectorWindow();
            try
            {
                ssw.StringListView.ItemsSource = Strings[b.Tag.ToString()];
            }
            catch
            {
                return;
            }
            if (ssw.ShowDialog() ?? false)
            {
                string s = ssw.StringListView.SelectedItem as string;
                if (s != null)
                {
                    sdg.TbString = s;
                }
            }
             
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            string key = b.Tag.ToString();
            StringDataGroup sdg = DataGroups[key] as StringDataGroup;
            List<string> ss = Strings[key];
            if (ss == null)
            {
                Strings.Add(key, new string[] { sdg.TbString }.ToList<string>() );
            }
            else
            {
                if (!ss.Exists(x => x == sdg.TbString))
                {
                    ss.Add(sdg.TbString);
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DocumentDescription doc = DocsList.SelectedItem as DocumentDescription;
            if (doc == null)
            {
                return;
            }
            if (DataGroups == null)
            {
                return;
            }
            
            
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() ?? false)
            {
                PrintQueue pq = pd.PrintQueue;
                PrintCapabilities pc = pq.GetPrintCapabilities();
                DrawingVisual vis = new DrawingVisual();
                DrawingContext dc = vis.RenderOpen();

                double stx = (pc.OrientedPageMediaWidth ?? 870) / 2;
                double dsty = 0;// pc.PageImageableArea.OriginHeight;

                double defaultFontSize = 14;
                double mmphi = 0.254; 

                foreach (KeyValuePair<string, DataGroup> kvp in DataGroups)
                {
                    StringDataGroup sdg = kvp.Value as StringDataGroup;
                    if (sdg != null)
                    {
                        string s = sdg.TbString ?? "";
                        FormattedText ft = new FormattedText(s, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(sdg.FontName), defaultFontSize * sdg.FontScale, new SolidColorBrush(Colors.Black));
                        double sty = dsty - ft.Baseline;
                        dc.DrawText(ft, new Point(stx + sdg.PrintPosition.X / mmphi, sty + sdg.PrintPosition.Y / mmphi));//*/
                    }
                    CheckDataGroup cdg = kvp.Value as CheckDataGroup;
                    if (cdg != null)
                    {
                        if (cdg.Checked)
                        {
                            string s = cdg.CheckString ?? "X";
                            FormattedText ft = new FormattedText(s, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(cdg.FontName), defaultFontSize * cdg.FontScale, new SolidColorBrush(Colors.Black));
                            double sty = dsty - ft.Baseline;
                            dc.DrawText(ft, new Point(stx + cdg.PrintPosition.X / mmphi, sty + cdg.PrintPosition.Y / mmphi));//*/
                        }
                    }
                }


                dc.Close();

                pd.PrintVisual(vis, doc.Name);
            }
        }
    }
}

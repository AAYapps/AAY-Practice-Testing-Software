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

namespace AAY_Transdumper_v2
{
    /// <summary>
    /// Interaction logic for testFileItem.xaml
    /// </summary>
    public partial class TestFileItem : UserControl
    {
        public TestFileItem()
        {
            InitializeComponent();
        }

        string filePath = "";
        public void InitIcon(string name, string path)
        {
            fileName.Text = name;
            filePath = path;
            string[] file = System.IO.File.ReadAllLines(path);
            for (int i = 0; i < file.Length; i++)
            {
                if (file[i].StartsWith("Rank: "))
                {
                    if (file[i].Replace("Rank: ", "").ToUpper().Equals("A"))
                        testIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/ARank.png"));
                    else if (file[i].Replace("Rank: ", "").ToUpper().Equals("B"))
                        testIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/BRank.png"));
                    else if (file[i].Replace("Rank: ", "").ToUpper().Equals("C"))
                        testIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/CRank.png"));
                    else if (file[i].Replace("Rank: ", "").ToUpper().Equals("D"))
                        testIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/DRank.png"));
                    else if (file[i].Replace("Rank: ", "").ToUpper().Equals("F"))
                        testIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/FRank.png"));
                    break;
                }
            }
        }

        public void Reload(Object sender, EventArgs e)
        {
            string test = filePath.Substring(filePath.LastIndexOf('\\') + 1);
            InitIcon(test.Replace(".txt", ""), filePath);
        }

        private void StartTest()
        {
            Test test = (Test)WindowLoader.createMainWindow(typeof(Test));
            AppConstants.TESTLOCATION = fileName.Text.Replace(".txt", "");
            test.testClose += Reload;
            test.SettupTest(filePath);
        }

        private void Grid_TouchUp(object sender, TouchEventArgs e)
        {
            StartTest();
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StartTest();
        }
    }
}

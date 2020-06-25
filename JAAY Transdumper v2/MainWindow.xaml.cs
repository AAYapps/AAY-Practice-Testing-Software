using System;
using System.Collections.Generic;
using System.IO;
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
using TestLogic;

namespace JAAY_Transdumper_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Sounds_Checked(object sender, RoutedEventArgs e)
        {
            Sounds.soundSwitchCheck.Play();
            Properties.Settings.Default.Sounds = true;
            Properties.Settings.Default.Save();
        }

        private void Sounds_Unchecked(object sender, RoutedEventArgs e)
        {
            Sounds.soundSwitchUncheck.Play();
            Properties.Settings.Default.Sounds = false;
            Properties.Settings.Default.Save();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] tests = Directory.GetFiles(AppConstants.TESTSLOCATION);
            foreach (string file in tests)
            {
                TestFileItem temp = new TestFileItem();
                string test = file.Substring(file.LastIndexOf('\\') + 1);
                temp.InitIcon(test.Replace(".txt", ""), file);
                testBrowser.Items.Add(temp);
            }
            sounds.IsChecked = Properties.Settings.Default.Sounds;
            Sounds.Volume(Properties.Settings.Default.Volume);
        }
    }
}

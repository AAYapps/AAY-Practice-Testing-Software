using System.Windows;
using System.Windows.Controls;

namespace AAY_Transdumper_v2
{
    /// <summary>
    /// Interaction logic for soundSettings.xaml
    /// </summary>
    public partial class soundSettings : Window
    {
        public soundSettings()
        {
            InitializeComponent();
        }

        void selectsound(TextBox sound)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.DefaultExt = ".wav";
            openFileDialog.Filter = "Wave Files (*.wav)|*.wav";
            bool result = false;
            result = (bool)openFileDialog.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = openFileDialog.FileName;
                sound.Text = filename;
            }
        }

        private void startsound_Click(object sender, RoutedEventArgs e)
        {
            selectsound(startsoundtxt);
        }

        private void testdone_Click(object sender, RoutedEventArgs e)
        {
            selectsound(testdonetxt);
        }

        private void resultpass_Click(object sender, RoutedEventArgs e)
        {
            selectsound(resultpasstxt);
        }

        private void resultfail_Click(object sender, RoutedEventArgs e)
        {
            selectsound(resultfailtxt);
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            selectsound(nexttxt);
        }

        private void next2_Click(object sender, RoutedEventArgs e)
        {
            selectsound(next2txt);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            selectsound(backtxt);
        }

        private void backtomain_Click(object sender, RoutedEventArgs e)
        {
            selectsound(backtomaintxt);
        }

        private void explain_Click(object sender, RoutedEventArgs e)
        {
            selectsound(explaintxt);
        }

        private void testselect_Click(object sender, RoutedEventArgs e)
        {
            selectsound(testselecttxt);
        }

        private void soundchecked_Click(object sender, RoutedEventArgs e)
        {
            selectsound(soundcheckedtxt);
        }

        private void soundunchecked_Click(object sender, RoutedEventArgs e)
        {
            selectsound(sounduncheckedtxt);
        }

        private void anwser_Click(object sender, RoutedEventArgs e)
        {
            selectsound(anwsertxt);
        }

        private void showreview_Click(object sender, RoutedEventArgs e)
        {
            selectsound(showreviewtxt);
        }

        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Sounds.Volume((float)Volume.Value);
        }
    }
}

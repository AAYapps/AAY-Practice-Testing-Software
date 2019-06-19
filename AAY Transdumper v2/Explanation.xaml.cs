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
using System.Windows.Shapes;

namespace AAY_Transdumper_v2
{
    /// <summary>
    /// Interaction logic for Explanation.xaml
    /// </summary>
    public partial class Explanation : Window
    {
        private LibVLCSharp.Shared.Media video;
        private readonly LibVLCSharp.Shared.LibVLC core = new LibVLCSharp.Shared.LibVLC();

        public Explanation()
        {
            InitializeComponent();
        }

        public void SetMediaPlayerVideo(string path)
        {
            ExplanationViewTab.Visibility = Visibility.Visible;
            Video.Visibility = Visibility.Visible;
            video = new LibVLCSharp.Shared.Media(core, path, LibVLCSharp.Shared.FromType.FromPath);
        }

        public void SetExplanation(string explanation)
        {
            ExplanationView.Text = explanation;
        }

        public void SetImage(BitmapImage img)
        {
            ImageViewTab.Visibility = Visibility.Visible;
            ImageView.Source = img;
        }

        private void MediaPlayer_Loaded(object sender, RoutedEventArgs e)
        {
            if (MediaPlayer.MediaPlayer == null)
                MediaPlayer.MediaPlayer = new LibVLCSharp.Shared.MediaPlayer(core);
            MediaPlayer.MediaPlayer.Play(video);
            play.Visibility = Visibility.Collapsed;
            pause.Visibility = Visibility.Visible;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MediaPlayer.MediaPlayer.Dispose();
            if (video != null)
                video.Dispose();
        }

        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MediaPlayer.MediaPlayer != null)
                MediaPlayer.MediaPlayer.Volume = (int)volume.Value;
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.MediaPlayer.Pause();
            play.Visibility = Visibility.Visible;
            pause.Visibility = Visibility.Collapsed;
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.MediaPlayer.Play();
            play.Visibility = Visibility.Collapsed;
            pause.Visibility = Visibility.Visible;
        }

        private void ExplanationTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (explanationTabs.TabIndex != 2)
            {
                if (MediaPlayer.MediaPlayer != null)
                    MediaPlayer.MediaPlayer.Stop();
            }
            else
            {
                MediaPlayer.MediaPlayer.Volume = (int)volume.Value;
                MediaPlayer.MediaPlayer.Pause();
                play.Visibility = Visibility.Visible;
                pause.Visibility = Visibility.Collapsed;
            }
        }
    }
}

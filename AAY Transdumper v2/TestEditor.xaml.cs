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
using System.Windows.Shapes;

namespace AAY_Transdumper_v2
{
    /// <summary>
    /// Interaction logic for TestEditor.xaml
    /// </summary>
    public partial class TestEditor : Window
    {
        private string testName = "untitled";
        private int qIndex = 0;
        private string qImage = "";
        private string expImage = "";
        private string expVideo = "";

        public TestEditor()
        {
            InitializeComponent();
            RefreshAnswerEditList(int.Parse((string)((ComboBoxItem)answerAmountSelector.SelectedItem).Content));
            this.Title = testName;
        }

        private void RefreshAnswerEditList(int limit)
        {
            if (answerList != null)
            {
                answerList.Children.Clear();
                for (int i = 0; i < limit; i++)
                {
                    CheckBox isAnswer = new CheckBox
                    {
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    TextBox newChoice = new TextBox
                    {
                        Margin = new Thickness(4, 4, 10, 4),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        HorizontalContentAlignment = HorizontalAlignment.Stretch,
                        FontSize = 20
                    };
                    Label label = new Label
                    {
                        Content = (char)('A' + i) + ": ",
                        FontSize = 20
                    };
                    DockPanel item = new DockPanel
                    {
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                    };
                    DockPanel.SetDock(isAnswer, Dock.Left);
                    item.Children.Add(isAnswer);
                    DockPanel.SetDock(label, Dock.Left);
                    item.Children.Add(label);
                    DockPanel.SetDock(newChoice, Dock.Left);
                    item.Children.Add(newChoice);
                    answerList.Children.Add(item);
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshAnswerEditList(int.Parse((string)((ComboBoxItem)answerAmountSelector.SelectedItem).Content));
        }

        private void SetQuestion(int index)
        {
            AppConstants.QUESTIONS[index].SetQuestion(question.Text);
            if (!qImage.Equals(""))
            {
                System.Drawing.Bitmap img =
                    (System.Drawing.Bitmap)System.Drawing.Image.
                        FromFile(qImage);
                AppConstants.QUESTIONS[index].SetQImage(img);
                AppConstants.QUESTIONS[index].SetQImageName(AppConstants.TESTSLOCATION + testName +
                    "\\IMG\\" + qImage.Substring(qImage.LastIndexOf('\\')));
                AppConstants.QUESTIONS[index].AppendQuestion("<img src=\"" + testName + "\\IMG\\" +
                    qImage.Substring(qImage.LastIndexOf('\\')) + "\">");
            }
            foreach (DockPanel item in answerList.Children)
            {
                bool answer = (bool)((CheckBox)item.Children[0]).IsChecked;
                char key = ((string)((Label)item.Children[1]).Content)[0];
                CheckBox choice = new CheckBox { Content = ((TextBox)item.Children[2]).Text };
                AppConstants.QUESTIONS[index].AddChoice(key, choice);
                AppConstants.QUESTIONS[index].SetChoiceTrue(key);
            }
            AppConstants.QUESTIONS[index].SetExplanation(explanation.Text + "\n");
            if (!expImage.Equals(""))
            {
                System.Drawing.Bitmap img =
                    (System.Drawing.Bitmap)System.Drawing.Image.
                        FromFile(expImage);
                AppConstants.QUESTIONS[index].SetAImage(img);
                AppConstants.QUESTIONS[index].SetAImageName(AppConstants.TESTSLOCATION + testName +
                    "\\IMG\\" + qImage.Substring(qImage.LastIndexOf('\\')));
                AppConstants.QUESTIONS[index].AppendExplanation("<img src=\"" + testName + "\\IMG\\" +
                    expImage.Substring(expImage.LastIndexOf('\\')) + "\">");
            }
            if (!expVideo.Equals(""))
            {
                AppConstants.QUESTIONS[index].SetAVideoURI(AppConstants.TESTSLOCATION + testName +
                    "\\IMG\\" + expVideo.Substring(qImage.LastIndexOf('\\')));
                AppConstants.QUESTIONS[index].AppendExplanation("<Embed src=\"" + testName + "\\IMG\\" +
                    expImage.Substring(expImage.LastIndexOf('\\')) + "\">");
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (qIndex > 0)
                Previous.Visibility = Visibility.Visible;
            if (!question.Text.Equals(""))
            {
                foreach (DockPanel item in answerList.Children)
                {
                    if (((TextBox)item.Children[2]).Text.Equals(""))
                    {
                        MessageBox.Show("There are some choices that are still empty. ");
                        return;
                    }
                }

                if (qIndex == AppConstants.QUESTIONS.Count)
                {
                    Previous.Visibility = Visibility.Visible;
                    AppConstants.QUESTIONS.Add(new Question());
                }
                SetQuestion(qIndex++);
            }
        }
 
        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog image = new Microsoft.Win32.OpenFileDialog();
            image.Filter = "JPG Image|*.jpg|PNG Image|*.png|GIF Image|*.gif";
            bool? result = image.ShowDialog();
            if (result != null && result == true)
            {
                expImage = image.FileName;
                mediaFile.Content = expImage.Split('\\').Last();
            }
            else
                mediaFile.Content = "No media";
        }

        private void AddVideo_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog video = new Microsoft.Win32.OpenFileDialog();
            video.Filter = "MPEG Video|*.mpg|AVI video|*.avi|WMV video|*.wmv|MP4 video|*.mp4|OGG video|*.ogg|FLV video|*.flv|MOV MP4 video|*.mov";
            bool? result = video.ShowDialog();
            if (result != null && result == true)
            {
                expVideo = video.FileName;
                mediaFile.Content = expVideo.Split('\\').Last();
            }
            else
                mediaFile.Content = "No media";
        }

        private void RemoveMedia_Click(object sender, RoutedEventArgs e)
        {
            expImage = "";
            expVideo = "";
            mediaFile.Content = "No media";
        }

        private void AddQImage_Click(object sender, RoutedEventArgs e)
        {
            if (qImage != "")
            {
                addQImage.Content = "Add Image";
                qImage = "";
            }
            else
            {
                Microsoft.Win32.OpenFileDialog image = new Microsoft.Win32.OpenFileDialog();
                image.Filter = "JPG Image|*.jpg|PNG Image|*.png|GIF Image|*.gif";
                bool? result = image.ShowDialog();
                if (result != null && result == true)
                {
                    qImage = image.FileName;
                    addQImage.Content = "Remove";
                }
                else
                    addQImage.Content = "Add Image";
            }

        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (qIndex <= 0)
                Previous.Visibility = Visibility.Collapsed;
            if (!question.Text.Equals(""))
            {
                foreach (DockPanel item in answerList.Children)
                {
                    if (((TextBox)item.Children[2]).Text.Equals(""))
                    {
                        MessageBox.Show("There are some choices that are still empty. ");
                        return;
                    }
                }
                SetQuestion(qIndex--);
            }
        }

        private void Save_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (System.IO.File.Exists(AppConstants.TESTSLOCATION + testName + ".txt"))
            {

            }
            else
            {
                SaveAs_MouseUp(sender, e);
            }
        }

        private void SaveAs_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog save = new Microsoft.Win32.SaveFileDialog();
            save.Filter = "Text File|*.txt";
            save.InitialDirectory = AppConstants.TESTSLOCATION;
            bool? result = save.ShowDialog();
            if (result != null && result == true)
            {
                StreamWriter file = File.CreateText(save.FileName);
                file.WriteLine("Exam Name: " + testName.Split('\\').Last());
                foreach (Question q in AppConstants.QUESTIONS)
                {
                    file.WriteLine();
                    file.WriteLine("");
                }
                testName = save.FileName;
                this.Title = testName.Split('\\').Last();
            }
        }
    }
}

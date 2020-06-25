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
using TestLogic;

namespace JAAY_Transdumper_v2
{
    /// <summary>
    /// Interaction logic for TestEditor.xaml
    /// </summary>
    public partial class TestEditor : Window
    {
        private string testName = "untitled";
        private EditorQuestion current;
        private int qIndex = 0;

        public TestEditor()
        {
            InitializeComponent();
            RefreshAnswerEditList(int.Parse((string)((ComboBoxItem)answerAmountSelector.SelectedItem).Content));
            Title = testName;
            current = new EditorQuestion();
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

                if (index == AppConstants.QUESTIONS.Count)
                {
                    AppConstants.QUESTIONS.Add(current);
                }
            }

            if (qIndex > 0)
                Previous.Visibility = Visibility.Visible;

            AppConstants.QUESTIONS[index].SetQuestion(question.Text);
            
            AppConstants.QUESTIONS[index].removeAllChoices();
            foreach (DockPanel item in answerList.Children)
            {
                // TODO make sure this works
                bool answer = (bool)((CheckBox)item.Children[0]).IsChecked;
                char key = ((string)((Label)item.Children[1]).Content)[0];
                AppConstants.QUESTIONS[index].AddChoice(key, ((TextBox)item.Children[2]).Text);
                if (answer)
                    AppConstants.QUESTIONS[index].SetChoiceTrue(key);
            }
            AppConstants.QUESTIONS[index].SetExplanation(explanation.Text + "\n");
            current = new EditorQuestion();
            question.Text = "";
            explanation.Text = "";
            addQImage.Content = "Add Image";
            addQVideo.Content = "Add Video";
            addImage.Content = "Add Image";
            addVideo.Content = "Add Video";

            if (!current.CheckQImageFile() && !current.CheckQVideoFile())
                qMediaFile.Text = "No media";
            else
                qMediaFile.Text = "";

            if (!current.CheckAImageFile() && !current.CheckAVideoFile())
                mediaFile.Content = "No media";
            else
                mediaFile.Content = "";

            if (current.CheckQImageFile())
            {
                qMediaFile.Text += current.GetAImageFile().Split('\\').Last();
                addQImage.Content = "Remove Image";
            }
            if (current.CheckAImageFile() && current.CheckAVideoFile())
                qMediaFile.Text += ", ";
            if (current.CheckAVideoFile())
            {
                qMediaFile.Text += current.GetAVideoFile().Split('\\').Last();
                addQVideo.Content = "Remove Video";
            }

            if (current.CheckAImageFile())
            {
                mediaFile.Content += current.GetAVideoFile().Split('\\').Last();
                addImage.Content = "Remove Image";
            }
            if (current.CheckAImageFile() && current.CheckAVideoFile())
                mediaFile.Content += ", ";
            if (current.CheckAVideoFile())
            {
                mediaFile.Content += current.GetAVideoFile().Split('\\').Last();
                addVideo.Content = "Remove Video";
            }
            RefreshAnswerEditList(int.Parse((string)((ComboBoxItem)answerAmountSelector.SelectedItem).Content));
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (!question.Text.Equals(""))
            {
                SetQuestion(qIndex++);
            }
            else
                MessageBox.Show("The question is not allowed to be empty. ");
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (qIndex - 1 <= 0)
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
            }
            SetQuestion(qIndex--);
        }

        private void AddQImage_Click(object sender, RoutedEventArgs e)
        {
            if (current.CheckQImageFile())
            {
                addQImage.Content = "Add Image";
                current.SetQImageFile("");
            }
            else
            {
                Microsoft.Win32.OpenFileDialog image = new Microsoft.Win32.OpenFileDialog
                {
                    Filter = "JPG Image|*.jpg|PNG Image|*.png|GIF Image|*.gif",
                    InitialDirectory = AppConstants.TESTSLOCATION
                };
                bool? result = image.ShowDialog();
                if (result != null && result == true)
                {
                    current.SetQImageFile(image.FileName);
                    addQImage.Content = "Remove Image";
                }
                else
                    addQImage.Content = "Add Image";
            }
            qMediaFile.Text = "";
            if (current.CheckQImageFile())
                qMediaFile.Text += current.GetQImageFile().Split('\\').Last();
            if (current.CheckQImageFile() && current.CheckQVideoFile())
                qMediaFile.Text += ", ";
            if (current.CheckQVideoFile())
                qMediaFile.Text += current.GetQVideoFile().Split('\\').Last();
        }

        private void AddQVideo_Click(object sender, RoutedEventArgs e)
        {
            if (current.CheckQVideoFile())
            {
                addQVideo.Content = "Add Video";
                current.SetQVideoFile("");
            }
            else
            {
                Microsoft.Win32.OpenFileDialog video = new Microsoft.Win32.OpenFileDialog
                {
                    Filter = "MPEG Video|*.mpg|AVI video|*.avi|WMV video|*.wmv|MP4 video|*.mp4|OGG video|*.ogg|FLV video|*.flv|MOV MP4 video|*.mov",
                    InitialDirectory = AppConstants.TESTSLOCATION
                };
                bool? result = video.ShowDialog();
                if (result != null && result == true)
                {
                    current.SetQVideoFile(video.FileName);
                    addQVideo.Content = "Remove Video";
                }
                else
                    addQVideo.Content = "Add Video";
            }
            qMediaFile.Text = "";
            if (current.CheckQImageFile())
                qMediaFile.Text += current.GetQImageFile().Split('\\').Last();
            if (current.CheckQImageFile() && current.CheckQVideoFile())
                qMediaFile.Text += ", ";
            if (current.CheckQVideoFile())
                qMediaFile.Text += current.GetQVideoFile().Split('\\').Last();
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            if (current.CheckAVideoFile())
            {
                addImage.Content = "Add Image";
                current.SetAVideoFile("");
            }
            else
            {
                Microsoft.Win32.OpenFileDialog image = new Microsoft.Win32.OpenFileDialog
                {
                    Filter = "JPG Image|*.jpg|PNG Image|*.png|GIF Image|*.gif",
                    InitialDirectory = AppConstants.TESTSLOCATION
            };
                bool? result = image.ShowDialog();
                if (result != null && result == true)
                {
                    current.SetAImageFile(image.FileName);
                    addImage.Content = "Remove Image";
                }
                else
                    addImage.Content = "Add Image";
            }
            if (!current.CheckAImageFile() && !current.CheckAVideoFile())
                mediaFile.Content = "No media";
            else
                mediaFile.Content = "";
            if (current.CheckAImageFile())
                mediaFile.Content += current.GetAImageFile().Split('\\').Last();
            if (current.CheckAImageFile() && current.CheckAVideoFile())
                mediaFile.Content += ", ";
            if (current.CheckAVideoFile())
                mediaFile.Content += current.GetAVideoFile().Split('\\').Last();
        }

        private void AddVideo_Click(object sender, RoutedEventArgs e)
        {
            if (current.CheckAVideoFile())
            {
                addVideo.Content = "Add Video";
                current.SetAVideoFile("");
            }
            else
            {
                Microsoft.Win32.OpenFileDialog video = new Microsoft.Win32.OpenFileDialog
                {
                    Filter = "MPEG Video|*.mpg|AVI video|*.avi|WMV video|*.wmv|MP4 video|*.mp4|OGG video|*.ogg|FLV video|*.flv|MOV MP4 video|*.mov",
                    InitialDirectory = AppConstants.TESTSLOCATION
                };
                bool? result = video.ShowDialog();
                if (result != null && result == true)
                {
                    current.SetAVideoFile(video.FileName);
                    addVideo.Content = "Remove Video";
                }
                else
                    addVideo.Content = "Add Video";
            }
            if (!current.CheckAVideoFile() && !current.CheckAVideoFile())
                mediaFile.Content = "No media";
            else
                mediaFile.Content = "";
            if (current.CheckAImageFile())
                mediaFile.Content += current.GetAImageFile().Split('\\').Last();
            if (current.CheckAImageFile() && current.CheckAVideoFile())
                mediaFile.Content += ", ";
            if (current.CheckAVideoFile())
                mediaFile.Content += current.GetAVideoFile().Split('\\').Last();
        }

        private void RemoveMedia_Click(object sender, RoutedEventArgs e)
        {
            current.SetAImageFile("");
            current.SetAVideoFile("");
            addImage.Content = "Add Image";
            addVideo.Content = "Add Video";
            mediaFile.Content = "No media";
        }

        private void Save(StreamWriter file)
        {
            SetQuestion(qIndex);
            file.WriteLine("Exam Name: " + testName.Split('\\').Last());
            int questionIndex = 0;
            foreach (EditorQuestion q in AppConstants.QUESTIONS)
            {
                file.WriteLine(++questionIndex + ". " + q.GetQuestion());
                file.WriteLine();
                if (!q.CheckQImageFile())
                {
                    // TODO make sure this works
                    file.WriteLine("<img src=\"" + testName + "\\IMG\\" +
                        q.GetQImageFile().Substring(q.GetQImageFile().LastIndexOf('\\')) + "\">");
                }
                file.WriteLine();
                if (!q.CheckQVideoFile())
                {
                    // TODO make sure this works
                    file.WriteLine("<Embed src=\"" + testName + "\\IMG\\" +
                        q.GetQVideoFile().Substring(q.GetQVideoFile().LastIndexOf('\\')) + "\">");
                }
                file.WriteLine();
                int count = 0;
                foreach (KeyValuePair<Question.Choice, bool> item in q.GetChoiceList())
                    file.WriteLine(((char)('A' + count++)) + ". " + item.Key.getText());
                file.WriteLine();
                if (!explanation.Text.Equals("") || !q.CheckAImageFile() || !q.CheckAVideoFile())
                    file.WriteLine("Explanation: ");
                if (!explanation.Text.Equals(""))
                {
                    file.WriteLine(q.GetExplanation());
                }
                file.WriteLine();
                if (!q.CheckAImageFile())
                {
                    // TODO make sure this works
                    file.WriteLine("<img src=\"" + testName + "\\IMG\\" +
                        q.GetAImageFile().Substring(q.GetAImageFile().LastIndexOf('\\')) + "\">");
                }
                file.WriteLine();
                if (!q.CheckAVideoFile())
                {
                    // TODO make sure this works
                    file.WriteLine("<Embed src=\"" + testName + "\\VIDEO\\" +
                        q.GetAVideoFile().Substring(q.GetAVideoFile().LastIndexOf('\\')) + "\">");
                }
                file.WriteLine();
            }
            file.Close();
        }

        private void Save_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (System.IO.File.Exists(AppConstants.TESTSLOCATION + testName + ".txt"))
            {
                File.Delete(AppConstants.TESTSLOCATION + testName + ".txt");
                Save(File.CreateText(AppConstants.TESTSLOCATION + testName + ".txt"));
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
                testName = save.FileName.Split('\\').Last().Replace(".txt", "");
                Save(file);
                this.Title = testName;
            }
        }
    }
}

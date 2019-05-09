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
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        private int totalQuestions = 0;
        private readonly static char[] ValidAnswerLetters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
        
        private int currentQuestion = 0;
        private string testName = "";

        public EventHandler testClose = new EventHandler((Object sender, EventArgs e) => { });

        public Test()
        {
            InitializeComponent();
        }

        public int GetLeadingNumber(string input)
        {
            int lastValid = 0;
            foreach (char i in input)
            {
                if (Char.IsDigit(i))
                {
                    lastValid++;
                }
                else
                {
                    break;
                }
            }
            if (lastValid > 0)
            {
                return int.Parse(input.Substring(0, lastValid));
            }
            else
            {
                return 0;
            }
        }

        private void DisplayQuestion(int index)
        {
            this.Title = testName + " " + (index + 1) + "/" + totalQuestions;
            currentQuestion = index;
            questionText.Text = AppConstants.QUESTIONS[index].GetQuestion();
            if (AppConstants.QUESTIONS[index].CheckQImage())
            {
                qimg.Source = AppConstants.QUESTIONS[index].GetQImage();
                qimg.Visibility = Visibility.Visible;
            }
            else
                qimg.Visibility = Visibility.Collapsed;

            foreach (Viewbox item in answerList.Children)
            {
                ((DockPanel)item.Child).Children.Clear();
                item.Child = null;
            }
            answerList.Children.Clear();
            foreach (KeyValuePair<CheckBox, bool> choice in AppConstants.QUESTIONS[index].GetChoiceList())
            {
                DockPanel answerPanel = new DockPanel
                {
                    Margin = new Thickness(0, 5, 20, 5),
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                Viewbox view = new Viewbox
                {
                    Stretch = Stretch.Uniform,
                    Height = 100,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Child = answerPanel
                };

                Image answerImg = new Image();
                BitmapSource right = new BitmapImage(new Uri("pack://application:,,,/Resources/right.gif"));
                BitmapSource wrong = new BitmapImage(new Uri("pack://application:,,,/Resources/Wrong.gif"));
                answerImg.Source = choice.Value ? right : wrong;
                answerImg.Height = 16;
                answerImg.Width = 16;
                answerImg.VerticalAlignment = VerticalAlignment.Center;
                answerImg.Name = choice.Value ? "correct" : "incorrect";
                if ((bool)choice.Key.IsChecked || choice.Value)
                    answerImg.Visibility = AppConstants.QUESTIONS[index].GetAnswered() ? Visibility.Visible : Visibility.Hidden;
                else
                    answerImg.Visibility = Visibility.Hidden;
                DockPanel.SetDock(answerImg, Dock.Left);                        
                answerPanel.Children.Add(answerImg);
                DockPanel.SetDock(choice.Key, Dock.Left);
                answerPanel.Children.Add(choice.Key);
                answerList.Children.Add(view);
            }
            Explanation.Visibility = Visibility.Collapsed;
            if (currentQuestion >= totalQuestions - 1)
                Next.Content = "Done";
            else
                Next.Content = "Next";
            if (currentQuestion <= 0)
                Previous.Visibility = Visibility.Collapsed;
            else
                Previous.Visibility = Visibility.Visible;
            if (Properties.Settings.Default.correctionOnAnswer && !AppConstants.QUESTIONS[index].GetAnswered())
            {
                Next.Visibility = Visibility.Collapsed;
                Answer.Visibility = Visibility.Visible;
            }
            else
            {
                Next.Visibility = Visibility.Visible;
                Answer.Visibility = Visibility.Collapsed;
            }
            if (AppConstants.QUESTIONS[index].GetAnswered())
                Explanation.Visibility = Visibility.Visible;
        }

        public void SettupTest(string file)
        {
            this.Title = "";
            string testPath = file;
            if (File.Exists(testPath))
            {
                string[] test = File.ReadAllLines(testPath);
                bool explanation = false;
                bool questionInsert = false;
                bool answerInsert = false;
                char tempLetter = ' ';
                int count = 0;
                foreach (string line in test)
                {
                    count++;
                    if (line.StartsWith("Exam Name:"))
                        testName = line.Replace("Exam Name:", "").TrimStart();
                    else if (!questionInsert && line.StartsWith(GetLeadingNumber(line) + "."))
                    {
                        AppConstants.QUESTIONS.Add(new Question());
                        AppConstants.QUESTIONS[AppConstants.QUESTIONS.Count - 1].SetQuestion(line.Replace(GetLeadingNumber(line) + ".", "").TrimStart());
                        totalQuestions++;
                        explanation = false;
                        questionInsert = true;
                        answerInsert = false;
                    }
                    else
                    {
                        try
                        {
                            if (AppConstants.QUESTIONS.Count > 0 && line.Trim().Length > 0)
                            {
                                if (line.StartsWith("<img src=\""))
                                {
                                    try
                                    {
                                        System.Drawing.Bitmap img =
                                            (System.Drawing.Bitmap)System.Drawing.Image.
                                                FromFile(AppConstants.IMGLOCATION +
                                                    line.Substring(line.LastIndexOf('/') + 1,
                                                        line.LastIndexOf('"') -
                                                        line.LastIndexOf('/') - 1));
                                        if (!questionInsert)
                                        {
                                            AppConstants.QUESTIONS[AppConstants.QUESTIONS.Count - 1].SetAImage(img);
                                        }
                                        else
                                        {
                                            AppConstants.QUESTIONS[AppConstants.QUESTIONS.Count - 1].SetQImage(img);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        MessageBox.Show("Warning, \"" + line + "\" is not a valid image path. \nCheck to make sure your image is in the right location.\n" + e.Message);
                                    }
                                }
                                else if (line.StartsWith("<Embed src=\""))
                                {
                                    string videoPath = AppConstants.VIDEOLOCATION +
                                        line.Substring(line.LastIndexOf('/') + 1,
                                            line.LastIndexOf('"') -
                                            line.LastIndexOf('/') - 1);
                                    if (!questionInsert)
                                    {
                                        AppConstants.QUESTIONS[AppConstants.QUESTIONS.Count - 1].SetAVideoURI(videoPath);
                                    }
                                    else
                                    {
                                        AppConstants.QUESTIONS[AppConstants.QUESTIONS.Count - 1].SetQVideoURI(videoPath);
                                    }
                                }
                                else if (line.ToLower().StartsWith("answer:"))
                                {
                                    string[] answers = line.Substring(7).Trim().Split(',');
                                    foreach (string chars in answers)
                                    {
                                        try
                                        {
                                            AppConstants.QUESTIONS[AppConstants.QUESTIONS.Count - 1].SetChoiceTrue(chars.Trim()[0]);
                                        }
                                        catch (Exception)
                                        {
                                            MessageBox.Show("Answer: " + 
                                                chars + " on " + (AppConstants.QUESTIONS.Count - 1) + 
                                                " does not exist.");
                                        }
                                    }
                                }
                                else if (!explanation && 
                                    line.StartsWith(ValidAnswerLetters.Where(c => c == line.ToUpper()[0]).Count() > 0 ?
                                    ValidAnswerLetters.Where(c => c == line.ToUpper()[0]).First() + "." : line + "undefined"))
                                {
                                    questionInsert = false;
                                    answerInsert = true;
                                    tempLetter = ValidAnswerLetters.Where(c => c == line.ToUpper()[0]).First();
                                    TextBlock text = new TextBlock
                                    {
                                        Text = line,
                                        TextWrapping = TextWrapping.Wrap
                                    };
                                    CheckBox choice = new CheckBox
                                    {
                                        Content = text
                                    };
                                    AppConstants.QUESTIONS[AppConstants.QUESTIONS.Count - 1].AddChoice(line.ToUpper()[0], choice);
                                }
                                else
                                {
                                    if (explanation)
                                    {
                                        AppConstants.QUESTIONS[AppConstants.QUESTIONS.Count - 1].AppendExplanation(line);
                                    }
                                    else
                                    {
                                        if (line.ToLower()
                                            .StartsWith("explanation:"))
                                        {
                                            explanation = true;
                                            AppConstants.QUESTIONS[AppConstants.QUESTIONS.Count - 1].AppendExplanation(line.Replace("explanation:", ""));
                                        }
                                        else
                                        {
                                            if (questionInsert)
                                                AppConstants.QUESTIONS[AppConstants.QUESTIONS.Count - 1].AppendQuestion(line);
                                            else
                                            {
                                                if (answerInsert)
                                                    AppConstants.QUESTIONS[AppConstants.QUESTIONS.Count - 1].AppendChoice(tempLetter,line);
                                                else
                                                    throw new Exception("Does not match any corresponding functions with the test");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error \"" + line + "\" is not valid \"" + 
                                e.Message + "\" at line: " + count);
                        }
                    }
                }
                DisplayQuestion(0);
            }
        }

        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            foreach (Viewbox item in answerList.Children)
            {
                CheckBox answerCheck = (CheckBox)((DockPanel)item.Child).Children[1];
                Image img = (Image)((DockPanel)item.Child).Children[0];
                if ((bool)answerCheck.IsChecked || img.Name.Equals("correct"))
                {
                    ((DockPanel)item.Child).Children[0].Visibility = Visibility.Visible;
                }
                answerCheck.IsEnabled = false;
            }
            AppConstants.QUESTIONS[currentQuestion].SetAnswered();
            Explanation.Visibility = Visibility.Visible;
            Next.Visibility = Visibility.Visible;
            Answer.Visibility = Visibility.Collapsed;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuestion < totalQuestions - 1)
            {
                DisplayQuestion(currentQuestion + 1);
            }
            else
            {
                WindowLoader.createMainWindow(typeof(ResultScreen), true);
                AppConstants.QUESTIONS.Clear();
                Close();
            }
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuestion > 0)
            {
                DisplayQuestion(currentQuestion - 1);
            }
        }

        private void Explanation_Click(object sender, RoutedEventArgs e)
        {
            Explanation explanation = (Explanation)WindowLoader.createMainWindow(typeof(Explanation));
            if (!AppConstants.QUESTIONS[currentQuestion].GetAVideoURI().Equals(""))
                explanation.SetMediaPlayerVideo(AppConstants.QUESTIONS[currentQuestion].GetAVideoURI());
            try
            {
                if (AppConstants.QUESTIONS[currentQuestion].GetAimage() != null)
                    explanation.SetImage(AppConstants.QUESTIONS[currentQuestion].GetAimage());
            }
            catch (Exception f)
            {
                MessageBox.Show("There was a problem with the image. " + f.Message);
            }
            if (!AppConstants.QUESTIONS[currentQuestion].GetExplanation().Equals(""))
                explanation.SetExplanation(AppConstants.QUESTIONS[currentQuestion].GetExplanation());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (Viewbox item in answerList.Children)
            {
                ((DockPanel)item.Child).Children.Clear();
                item.Child = null;
            }
            answerList.Children.Clear();
            testClose.Invoke(this, null);
        }
    }
}

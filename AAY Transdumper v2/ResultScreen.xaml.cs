using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestLogic;

namespace AAY_Transdumper_v2
{
    /// <summary>
    /// Interaction logic for ResultScreen.xaml
    /// </summary>
    public partial class ResultScreen : Window
    {
        private int total = 0;
        private int correct = 0;
        private int incorrect = 0;
        private int countCorrect = 0;
        private int countIncorrect = 0;
        private char grade = ' ';
        private Timer timer = new Timer(100);

        public ResultScreen()
        {
            foreach (Question item in AppConstants.QUESTIONS)
            {
                total++;
                bool right = true;
                foreach (KeyValuePair<Question.Choice, bool> choice in item.GetChoiceList())
                {
                    if (right)
                    {
                        if (choice.Key.getChecked() != choice.Value)
                            right = false;
                    }
                }
                if (right)
                    correct++;
                else
                    incorrect++;
            }
            InitializeComponent();
            totalText.Content = total;
            timer.Elapsed += resultCount;
            timer.Start();
        }

        private bool GradeRange(double lower, double higher)
        {
            return (correct / total < higher && correct / total >= lower);
        }

        private void resultCount(object sender, ElapsedEventArgs e)
        {
            if (countCorrect == correct && countIncorrect == incorrect)
            {
                Dispatcher.Invoke((Action)delegate () {
                    if (GradeRange(0.9, 1.01))
                    {
                        grade = 'A';
                        rank.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/ARank.png"));
                    }
                    else if (GradeRange(0.8, 0.9))
                    {
                        grade = 'B';
                        rank.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/BRank.png"));
                    }
                    else if (GradeRange(0.7, 0.8))
                    {
                        grade = 'C';
                        rank.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/CRank.png"));
                    }
                    else if (GradeRange(0.6, 0.7))
                    {
                        grade = 'D';
                        rank.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/DRank.png"));
                    }
                    else if (GradeRange(0, 0.6))
                    {
                        grade = 'F';
                        rank.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/FRank.png"));
                    }
                });
                timer.Stop();
                if (GradeRange(0.7, 1.01))
                {
                    Sounds.PlayAudio(Sounds.Passed);
                }
                else
                {
                    Sounds.PlayAudio(Sounds.Failed);
                }
            }
            else
            {
                if (countCorrect < correct)
                    countCorrect++;
                if (countIncorrect < incorrect)
                    countIncorrect++;
                Dispatcher.Invoke((Action)delegate () {
                    correctText.Content = countCorrect;
                    incorrectText.Content = countIncorrect;
                });
            }
        }

        private void done_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool changeRank = false;
            string[] file = System.IO.File.ReadAllLines(AppConstants.TESTLOCATION.Substring(0, AppConstants.TESTLOCATION.Length - 1) + ".txt");
            for (int i = 0; i < file.Length; i++)
            {
                if (file[i].StartsWith("Rank: "))
                {
                    changeRank = true;
                    file[i] = "Rank: " + grade;
                    break;
                }
            }
            if (changeRank)
                System.IO.File.WriteAllLines(AppConstants.TESTLOCATION.Substring(0, AppConstants.TESTLOCATION.Length - 1) + ".txt", file);
            else
            {
                string[] newFile = new string[file.Length + 1];
                newFile[0] = "Rank: " + grade;
                for (int i = 1; i < newFile.Length; i++)
                    newFile[i] = file[i - 1];
                System.IO.File.WriteAllLines(AppConstants.TESTLOCATION.Substring(0, AppConstants.TESTLOCATION.Length - 1) + ".txt", newFile);
            }

            Sounds.PlayAudio(Sounds.closeResult);
        }
    }
}

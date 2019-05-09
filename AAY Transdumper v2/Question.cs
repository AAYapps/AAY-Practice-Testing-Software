using System.Collections.Generic;
using System.Windows.Controls;

namespace AAY_Transdumper_v2
{
    class Question
    {
        private bool answered = false;
        private string question = "";
        private System.Drawing.Bitmap qImage = null;
        private string qImageName = "";
        private string qVideoURI = "";
        private readonly Dictionary<char, KeyValuePair<CheckBox, bool>> answers = new Dictionary<char, KeyValuePair<CheckBox, bool>>();
        private System.Drawing.Bitmap aImage = null;
        private string aImageName = "";
        private string aVideoURI = "";
        private string explanation = "";

        public Question() { }

        public void SetAnswered()
        {
            answered = true;
        }

        public bool GetAnswered()
        {
            return answered;
        }

        public bool CheckIfCorrect()
        {
            return true;
        }

        public string GetQuestion()
        {
            return question;
        }

        public void SetQuestion(string question)
        {
            this.question = question;
        }

        public void AppendQuestion(string question)
        {
            this.question += "\n" + question;
        }

        public System.Windows.Media.Imaging.BitmapImage GetQImage()
        {
            return Converter.BitmapToImageSource(qImage);
        }

        public bool CheckQImage()
        {
            return qImage != null;
        }

        public void SetQImage(System.Drawing.Bitmap qImage)
        {
            this.qImage = qImage;
        }

        public string GetQImageName()
        {
            return qImageName;
        }

        public bool CheckQImageName()
        {
            return !qImageName.Equals("");
        }

        public void SetQImageName(string qImageName)
        {
            this.qImageName = qImageName;
        }

        public void AddChoice(char letter, CheckBox choice)
        {
            //choice.RenderTransform = new System.Windows.Media.ScaleTransform(2.0, 2.0);
            //choice.RenderTransformOrigin = new System.Windows.Point(1,1);
            //choice.Margin = new System.Windows.Thickness(0, 0, 200, 0);
            choice.FontSize = 20;
            choice.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            choice.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            //choice.Resources = new BigCheckBox();
            answers.Add(letter, new KeyValuePair<CheckBox, bool>(choice, false));
        }

        public void SetChoiceTrue(char letter)
        {
            answers[letter] = new KeyValuePair<CheckBox, bool>(answers[letter].Key, true);
        }

        public void AppendChoice(char letter, string text)
        {
            TextBlock temp = (TextBlock)answers[letter].Key.Content;
            temp.Text += " " + text.TrimStart();
            answers[letter].Key.Content = temp;
        }

        public List<KeyValuePair<CheckBox, bool>> GetChoiceList()
        {
            List<KeyValuePair<CheckBox, bool>> list = new List<KeyValuePair<CheckBox, bool>>();
            foreach (KeyValuePair<CheckBox, bool> item in answers.Values)
            {
                list.Add(item);
            }
            return list;
        }

        public System.Windows.Media.Imaging.BitmapImage GetAimage()
        {
            if (aImage != null)
                return Converter.BitmapToImageSource(aImage);
            else
                return null;
        }

        public bool CheckAImage()
        {
            return aImage != null;
        }

        public void SetAImage(System.Drawing.Bitmap aImage)
        {
            this.aImage = aImage;
        }

        public string GetAImageName()
        {
            return aImageName;
        }

        public bool CheckAImageName()
        {
            return !aImageName.Equals("");
        }

        public void SetAImageName(string aImageName)
        {
            this.aImageName = aImageName;
        }

        public string GetExplanation()
        {
            return explanation;
        }

        public void SetExplanation(string explanation)
        {
            this.explanation = explanation + "\n";
        }

        public void AppendExplanation(string explanation)
        {
            this.explanation += explanation + "\n";
        }

        public string GetQVideoURI()
        {
            return qVideoURI;
        }

        public void SetQVideoURI(string qVideoURI)
        {
            this.qVideoURI = qVideoURI;
        }

        public string GetAVideoURI()
        {
            return aVideoURI;
        }

        public void SetAVideoURI(string aVideoURI)
        {
            this.aVideoURI = aVideoURI;
        }
    }
}

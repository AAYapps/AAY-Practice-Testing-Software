using System.Collections.Generic;

namespace TestLogic
{
    class Question
    {
        public class Choice
        {
            private string text = "";
            private bool Checked = false;

            public void setChecked(bool Checked)
            {
                this.Checked = Checked;
            }

            public void setText(string text)
            {
                this.text = text;
            }

            public void appendText(string text)
            {
                this.text += "\n" + text;
            }

            public string getText()
            {
                return text;
            }

            public bool getChecked()
            {
                return Checked;
            }
        }

        private bool answered = false;
        private string question = "";
        private string qImage = "";
        private string qVideoURI = "";
        private readonly Dictionary<char, KeyValuePair<Choice, bool>> answers = new Dictionary<char, KeyValuePair<Choice, bool>>();
        private string aImage = "";
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

        public string GetQImage()
        {
            return qImage;
        }

        public bool CheckQImage()
        {
            return !qImage.Equals("");
        }

        public void SetQImage(string qImage)
        {
            this.qImage = qImage;
        }

        public void AddChoice(char letter, string choice)
        {
            Choice item = new Choice();
            item.setText(choice);
            answers.Add(letter, new KeyValuePair<Choice, bool>(item, false));
        }

        public void SetChoiceTrue(char letter)
        {
            answers[letter] = new KeyValuePair<Choice, bool>(answers[letter].Key, true);
        }

        public void SetChoiceChecked(char letter, bool check)
        {
            answers[letter].Key.setChecked(check);
        }

        public void AppendChoice(char letter, string text)
        {
            string temp = answers[letter].Key.getText();
            temp += " " + text.TrimStart();
            answers[letter].Key.appendText(temp);
        }

        public void removeAllChoices()
        {
            answers.Clear();
        }

        public List<KeyValuePair<Choice, bool>> GetChoiceList()
        {
            List<KeyValuePair<Choice, bool>> list = new List<KeyValuePair<Choice, bool>>();
            foreach (KeyValuePair<Choice, bool> item in answers.Values)
            {
                list.Add(item);
            }
            return list;
        }

        public string GetAimage()
        {
            return aImage;
        }

        public bool CheckAImage()
        {
            return !aImage.Equals("");
        }

        public void SetAImage(string aImage)
        {
            this.aImage = aImage;
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

        public bool CheckQVideoURI()
        {
            return !qVideoURI.Equals("");
        }

        public void SetQVideoURI(string qVideoURI)
        {
            this.qVideoURI = qVideoURI;
        }

        public string GetAVideoURI()
        {
            return aVideoURI;
        }

        public bool CheckAVideoURI()
        {
            return !aVideoURI.Equals("");
        }

        public void SetAVideoURI(string aVideoURI)
        {
            this.aVideoURI = aVideoURI;
        }
    }
}

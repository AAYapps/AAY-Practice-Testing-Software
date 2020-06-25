using TestLogic;

namespace JAAY_Transdumper_v2
{
    class EditorQuestion : Question
    {
        private string qImageFile = "";
        private string aImageFile = "";
        private string qVideoFile = "";
        private string aVideoFile = "";
        public EditorQuestion() : base()
        {

        }

        public string GetQImageFile()
        {
            return qImageFile;
        }

        public bool CheckQImageFile()
        {
            return !qImageFile.Equals("");
        }

        public void SetQImageFile(string qImageFile)
        {
            this.qImageFile = qImageFile;
        }

        public string GetAImageFile()
        {
            return aImageFile;
        }

        public bool CheckAImageFile()
        {
            return !aImageFile.Equals("");
        }

        public void SetAImageFile(string aImageFile)
        {
            this.aImageFile = aImageFile;
        }

        public string GetQVideoFile()
        {
            return qVideoFile;
        }

        public bool CheckQVideoFile()
        {
            return !qVideoFile.Equals("");
        }

        public void SetQVideoFile(string aVideoFile)
        {
            this.qVideoFile = aVideoFile;
        }

        public string GetAVideoFile()
        {
            return aVideoFile;
        }

        public bool CheckAVideoFile()
        {
            return !aVideoFile.Equals("");
        }

        public void SetAVideoFile(string aVideoFile)
        {
            this.aVideoFile = aVideoFile;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAAY_Transdumper_v2
{
    static class WindowLoader
    {
        private static readonly List<System.Windows.Window> views = new List<System.Windows.Window>();
        public static void setMainWindow(System.Windows.Window window)
        {
            if (views.Find(name => name.Equals(window)) == null)
                views.Add(window);
        }

        public static System.Windows.Window createMainWindow(Type window, bool dialogBox = false)
        {
            System.Windows.Window temp = views.Find(name => name.GetType().Equals(window));
            if (temp == null)
            {
                temp = (System.Windows.Window)Activator.CreateInstance(window);
                views.Add(temp);
            }
            temp.Closed += onClose;
            if (dialogBox)
                temp.ShowDialog();
            else
                temp.Show();
            temp.Focus();
            return temp;
        }

        private static void onClose(object sender, EventArgs e)
        {
            views.Remove((System.Windows.Window)sender);
        }
    }
}

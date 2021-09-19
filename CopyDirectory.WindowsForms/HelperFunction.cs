using System.Windows.Forms;

namespace CopyDirectory.WindowsForms
{
    /// <summary>
    /// Helper function class which follows singleton pattern.
    /// </summary>
    internal sealed class HelperFunction
    {
        private static HelperFunction instance = null;
        public static HelperFunction GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new HelperFunction();
                return instance;
            }
        }
        private HelperFunction()
        {

        }
        delegate void SetTextCallback(Form f, ListView ctrl, string text);
        /// <summary>
        /// Set text property of controls by invoking which compares the thread ID of the calling thread to the thread ID of the creating thread. 
        /// If these threads are different, it returns true.
        /// </summary>
        /// <param name="form">The calling form</param>
        /// <param name="listView"></param>
        /// <param name="text"></param>
        internal void AddViewItem(Form form, ListView listView, string text)
        {
            if (listView.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(AddViewItem);
                form.Invoke(d, new object[] { form, listView, text });
            }
            else
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Text = text;
                listView.Items.Add(listViewItem);
            }
        }
    }
}

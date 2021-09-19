using CopyDirectory.Copier;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyDirectory.WindowsForms
{
    public partial class CopyForm : Form
    {
        private ICopy _copyDirectory;
        public CopyForm()
        {
            _copyDirectory = (ICopy)Program.ServiceProvider.GetService(typeof(ICopy)); /// ICopy dependency injected
            InitializeComponent();
        }
        #region Forms onclick Events
        private void Source_Click(object sender, EventArgs e)
        {
            SelectFolder(Source);
        }

        private void Target_Click(object sender, EventArgs e)
        {
            SelectFolder(Target);
        }

        private async void CopyButton_ClickAsync(object sender, EventArgs e)
        {
            await CopyFolderAsync();
        }
        #endregion
        #region Local methods
        /// <summary>
        /// Copy files and folder
        /// </summary>
        /// <returns></returns>
        private async Task CopyFolderAsync()
        {
            ListViewItem listViewItem = new ListViewItem();

            if (!string.IsNullOrEmpty(Source.Text) && !string.IsNullOrEmpty(Target.Text))
            {
                CopyButton.Enabled = false;

                ResponseMessage response = await Task.Run(() => CopyTask(Source.Text, Target.Text));

                if (response == ResponseMessage.SUCCESS)
                {
                    listViewItem.Text = ReturnMessage.SUCCESS;
                    OutputList.Items.Add(listViewItem);
                }
                else if (response == ResponseMessage.SAME)
                {
                    listViewItem.Text = ReturnMessage.SAME;
                    OutputList.Items.Add(listViewItem);
                }
                else if (response == ResponseMessage.ERROR)
                {
                    listViewItem.Text = ReturnMessage.ERROR;
                    OutputList.Items.Add(listViewItem);
                }
            }
            else
            {
                listViewItem.Text = ReturnMessage.AGAIN;
                OutputList.Items.Add(listViewItem);
            }
            CopyButton.Enabled = true;
        }
        /// <summary>
        /// Calling CopyDirectory from dependency
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private ResponseMessage CopyTask(string source, string target)
        {
            _copyDirectory.Process += AddProgressToListView;
            return _copyDirectory.CopyDirectory(source, target);
        }
        /// <summary>
        /// Adding items in list view by using function pointer
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public void AddProgressToListView(string message)
        {
            HelperFunction helperFunction = HelperFunction.GetInstance;
            helperFunction.AddViewItem(this, OutputList, message);
        }
        /// <summary>
        /// Select folder using FolderBrowserDialog
        /// </summary>
        /// <param name="textBox"></param>
        private void SelectFolder(TextBox textBox)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    textBox.Text = fbd.SelectedPath;

                }
            }
        }
        #endregion
    }
}

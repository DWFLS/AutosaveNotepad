namespace AutosaveNotepad
{
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private void SaveButtonCheck()
        {
            if (currentFileName == "")
            {
                saveToolStripMenuItem.Enabled = false;
            }

            else
            {
                saveToolStripMenuItem.Enabled = true;
            }
        }
    }
}

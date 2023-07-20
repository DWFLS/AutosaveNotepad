namespace AutosaveNotepad
{
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private void SaveButtonCheck()
        {
            if (currentFileName != "" && File.Exists(currentFileName))
            {
                saveToolStripMenuItem.Enabled = true;
            }

            else
            {
                AutosaveActive(false);
                autosaveCheckBox.Enabled = false;
                saveToolStripMenuItem.Enabled = false;
            }
        }
    }
}

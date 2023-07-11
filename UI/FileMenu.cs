namespace AutosaveNotepad
{
    using System;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        //
        // FILE menu
        //

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New("New...");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void saveAsCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFileName))
                SaveAs();

            else
                SaveAsCopy();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Application.Exit();
        }

        private void selectDefaultSaveDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectDefaultFolder();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print();
        }
    }
}

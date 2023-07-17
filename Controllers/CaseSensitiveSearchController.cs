namespace AutosaveNotepad
{
    using System;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        // Controlling buttons related to Case Sensitive Search

        private void caseSensitiveSearchToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            CaseSenstiveSearch();
        }

        private void CaseSenstiveSearch()
        {
            NextPrevGlobalController(false);
            findButton.Enabled = false;
        }

        private void caseSensitiveSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

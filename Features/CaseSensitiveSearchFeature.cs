namespace AutosaveNotepad
{
    using System;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private void CaseSenstiveSearch()
        {
            NextPrevGlobalController(false);
            findButton.Enabled = false;
        }
        private void caseSensitiveSearchToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            CaseSenstiveSearch();
        }

        private void caseSensitiveSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

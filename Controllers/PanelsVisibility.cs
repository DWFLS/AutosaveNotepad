namespace AutosaveNotepad
{
    public partial class formMain
    {
        // Quicksave Panel

        private void quicksavePanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quicksavePanelToolStripMenuItem.Checked = !quicksavePanelToolStripMenuItem.Checked;
        }

        private void quicksavePanelToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            panel1.Visible = quicksavePanelToolStripMenuItem.Checked;
            if (panel1.Visible == false)
            {
                StripStatusConstructor("", "", " ", "");
            }

            else
            {
                StripStatusConstructor("", "", "", "");
            }
        }

        // Search Panel

        private void searchPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchPanelToolStripMenuItem.Checked = !searchPanelToolStripMenuItem.Checked;
        }

        private void searchPanelToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            searchPanel.Visible = searchPanelToolStripMenuItem.Checked;
        }
    }
}
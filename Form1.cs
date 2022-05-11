namespace AutosaveNotepad
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }

        // FILE menu

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEditorBox.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveAsCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // FILE functions

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }


        // EDIT menu

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // DARK MODE Menu

        private void darkModeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // ABOUT Menu

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
}
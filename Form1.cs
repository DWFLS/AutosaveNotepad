namespace AutosaveNotepad
{
    public partial class formMain : Form
    {
        string currentFileName = "";
        string originalFileName = "";

        public formMain()
        {
            InitializeComponent();
        }

        private void formMain_Load(object sender, EventArgs e)
        {

        }

        // FILE menu

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs("New...", true);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open";
            openFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
                this.Text = "AutosaveNotepad - " + openFileDialog.FileName;
                currentFileName = openFileDialog.FileName;
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs("Save as...", false);
        }

        private void saveAsCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFileName == "") // quick save and promptless load of currentFileName
            {
                SaveAs("Save as...", false);
            }
            else
            {
                SaveAsCopy("Save a backup copy...", false);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // FILE functions

        private void SaveAs(string title, bool clearTextBox)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = title;
            saveFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (clearTextBox)
                {
                    richTextBox.Clear();
                }
                richTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                this.Text = "AutosaveNotepad - " + saveFileDialog.FileName;
                currentFileName = saveFileDialog.FileName;
                originalFileName = currentFileName;
            }
        }

        private void SaveAsCopy(string title, bool clearTextBox)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = title;
            saveFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (clearTextBox)
                {
                    richTextBox.Clear();
                }
                richTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                richTextBox.SaveFile(originalFileName, RichTextBoxStreamType.PlainText);
                richTextBox.LoadFile(originalFileName, RichTextBoxStreamType.PlainText);

                this.Text = "AutosaveNotepad - " + originalFileName;
                currentFileName = originalFileName;
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        // EDIT menu

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Clear();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.SelectAll();
        }

        // DARK MODE Menu

        private void darkModeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // ABOUT Menu

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }


    }
}
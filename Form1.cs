namespace AutosaveNotepad
{
    public partial class formMain : Form
    {
        string currentFileName = "";
        bool autosaveActive = false;
        bool textBoxActive = false;

        public formMain()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Autosave is not active - Create or open a document.";
            if (currentFileName == "")
            {
                editToolStripMenuItem.Enabled = false;
                displaySettingToolStripMenuItem.Enabled = false;
            }

            else
            {
                editToolStripMenuItem.Enabled = true;
            }

            if (textBoxActive == false)
            {
                richTextBox.Enabled = false;

            }
        }

        public void formMain_Load(object sender, EventArgs e)
        {

        }

        //
        // textBox input
        //

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            Autosave();
        }

        private void Autosave()
        {
            if (autosaveActive)
            {
                richTextBox.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);
                toolStripStatusLabel1.Text = "Autosave is now active";
            }
        }

        private void EnableFeatures()
        {
            CheckColors();
            richTextBox.Enabled = true;
            editToolStripMenuItem.Enabled = true;
            displaySettingToolStripMenuItem.Enabled = true;
        }

        private void CheckColors()
        {
            if (darkModeToolStripMenuItem1.Checked)
            {
                richTextBox.BackColor = Color.FromArgb(8, 17, 19);
                richTextBox.ForeColor = Color.White;
            }
            else
            {
                richTextBox.BackColor = Color.White;
                richTextBox.ForeColor = Color.Black;
            }
        }


        // FILE menu

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
            if (currentFileName == "") // quick save and promptless load of currentFileName
            {
                SaveAs();
            }
            else
            {
                SaveAsCopy();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // FILE functions

        private void New(string title)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = title;
            saveFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                richTextBox.Clear();
                currentFileName = saveFileDialog.FileName;
                richTextBox.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);
                this.Text = "AutosaveNotepad - " + saveFileDialog.FileName;
                autosaveActive = true;
                toolStripStatusLabel1.Text = "Autosave is now active";
                EnableFeatures();
            }
        }

        private void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open";
            openFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
                this.Text = "AutosaveNotepad - " + openFileDialog.FileName;
                currentFileName = openFileDialog.FileName;
                autosaveActive = true;
                toolStripStatusLabel1.Text = "Autosave is now active";
                EnableFeatures();
            }
        }

        private void SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save as...";
            saveFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                this.Text = "AutosaveNotepad - " + saveFileDialog.FileName;
                currentFileName = saveFileDialog.FileName;
                toolStripStatusLabel1.Text = "Autosave is now active";
                EnableFeatures();
            }
        }

        private void SaveAsCopy()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save a backup copy...";
            saveFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                this.Text = "AutosaveNotepad - " + currentFileName;
                toolStripStatusLabel1.Text = "Created a backup copy. Autosave is active the original file";
                EnableFeatures();
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
            Autosave();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Redo();
            Autosave();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
            Autosave();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
            Autosave();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
            Autosave();
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Clear();
            Autosave();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.SelectAll();
            Autosave();
        }

        // DARK MODE Menu

        private void darkModeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CheckColors();
        }

        // ABOUT Menu

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void displaySettingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
}
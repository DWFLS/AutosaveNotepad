namespace AutosaveNotepad
{
    public partial class formMain : Form
    {
        private string currentFileName = string.Empty;
        private bool autosaveActive = false;

        public formMain() //this code block is executed after the main form is instantiated.
        {
            InitializeComponent();

            statusStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            toolStripStatusLabel2.Alignment = ToolStripItemAlignment.Right;

            //
            // AutosaveNotepad initial settings:
            //

            toolStripStatusLabel1.Text = "Autosave is NOT active - Create or open a document.";
            editToolStripMenuItem.Enabled = false;
            displaySettingToolStripMenuItem.Enabled = false;
            richTextBox.Enabled = false;
        }

        public void formMain_Load(object sender, EventArgs e)
        {

        }

        //
        // RICH TEXT BOX input
        //

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            Autosave();
        }

        //
        // FEATURE functions
        //


        private void Autosave()
        {
            if (autosaveActive)
            {
                richTextBox.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);
                toolStripStatusLabel1.Text = "Autosave is active";
            }

            else
                toolStripStatusLabel1.Text = "Autosave is NOT active";
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
            if (string.IsNullOrEmpty(currentFileName))
                SaveAs();

            else
                SaveAsCopy();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // FILE functions

        private void New(string title)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = title;
            saveFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                currentFileName = saveFileDialog.FileName;
                richTextBox.Clear();
                richTextBox.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);
                this.Text = "AutosaveNotepad - " + saveFileDialog.FileName;
                autosaveActive = true;
                toolStripStatusLabel1.Text = "Autosave is now active, take care while editing.";
                EnableFeatures();

            }
        }

        private void Open()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open...";
            openFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
                this.Text = "AutosaveNotepad - " + openFileDialog.FileName;
                currentFileName = openFileDialog.FileName;
                autosaveActive = true;
                toolStripStatusLabel1.Text = "Autosave is now active, take care while editing.";
                EnableFeatures();
            }
        }

        private void SaveAs()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save as...";
            saveFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                this.Text = "AutosaveNotepad - " + saveFileDialog.FileName;
                currentFileName = saveFileDialog.FileName;
                autosaveActive = true;
                toolStripStatusLabel1.Text = "Autosave is now active, take care while editing.";
                EnableFeatures();
            }
        }

        private void SaveAsCopy()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save a backup copy...";
            saveFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                this.Text = "AutosaveNotepad - " + currentFileName;
                autosaveActive = true;
                toolStripStatusLabel1.Text = "Created a backup copy. Autosave is active on the original file";
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

        //
        // DISPLAY Menu
        //
        private void displaySettingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void darkModeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CheckColors();
        }

        //
        // TOOL STRIP bar
        //

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
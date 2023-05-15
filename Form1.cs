namespace AutosaveNotepad
{
    using System.IO;
    public partial class formMain : Form
    {


        private string currentFileName = string.Empty;
        private bool autosaveActive = false;
        string defaultFolderPath = string.Empty;
        string defaultFolderLogFilePath = string.Empty;
        string rootFolder = AppDomain.CurrentDomain.BaseDirectory;
        private bool validDefaultFolderChosen = false;
        string autosaveStatus = "";
        string miscInfo = "";
        string defaultFolderStatus = "";


        public formMain() //this code block is executed after the main form is instantiated.
        {
            InitializeComponent();

            //
            // AutosaveNotepad initial settings:
            //

            EnableFeatures(false);
            CheckForDefaultFolder();
            StripStatusConstructor("Autosave is NOT active - Create or open a document.", "", "");

            statusStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            toolStripStatusLabel2.Alignment = ToolStripItemAlignment.Right;
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

        private void QuickSaveBarControl(bool validDefPath)
        {
            quicksaveLabel.Enabled = validDefPath;
            quicksaveTextBox.Enabled = validDefPath;
            quicksaveButton.Enabled = validDefPath;

        }

        private void StripStatusConstructor(string a, string b, string c)
        {
            if (a != "") autosaveStatus = a + " ";
            if (b != "") miscInfo = b + " ";
            if (c != "") defaultFolderStatus = c;

            toolStripStatusLabel1.Text = autosaveStatus + miscInfo + defaultFolderStatus;
        }


        private void CheckForDefaultFolder()
        {
            if (File.Exists(rootFolder + @"\defaultFolderPath.log"))
            {
                defaultFolderLogFilePath = rootFolder + @"\defaultFolderPath.log";
                {
                    if (Directory.Exists(File.ReadLines(defaultFolderLogFilePath).ElementAtOrDefault(0)))
                    {
                        defaultFolderPath = File.ReadLines(defaultFolderLogFilePath).ElementAtOrDefault(0);
                        QuickSaveBarControl(true);
                        StripStatusConstructor("", "", "Saving files in: " + defaultFolderPath);

                    }

                    else
                    {
                        StripStatusConstructor("", "", "Default save folder not found.");
                        QuickSaveBarControl(false);
                    }
                }
            }
            else
            {
                StripStatusConstructor("", "", "Default folder not selected.");
                QuickSaveBarControl(false);
            }
        }

        private void Autosave()
        {
            if (autosaveActive)
            {
                richTextBox.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);
                StripStatusConstructor("Autosave is active.", "", "");
            }

            else
                StripStatusConstructor("Autosave is NOT active.", "", "");
        }

        private void EnableFeatures(bool b)
        {
            CheckColors();
            richTextBox.Enabled = b;
            editToolStripMenuItem.Enabled = b;
            displaySettingToolStripMenuItem.Enabled = b;
            saveAsToolStripMenuItem.Enabled = b;
            saveAsCopyToolStripMenuItem.Enabled = b;
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

        private object FileNameOnly(string fileName)
        {
            int lastSlashIndex = 0;
            for (int i = fileName.Length - 1; i != 0; i--)
            {
                if (fileName[i] == '\\')
                {
                    lastSlashIndex = fileName.Length - 1 - i;
                    break;
                }
            }

            return fileName.Remove(0, fileName.Length - lastSlashIndex);
        }

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
            Application.Exit();
        }

        private void selectDefaultSaveDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectDefaultFolder();
        }

        //
        // FILE functions
        //

        private void SelectDefaultFolder()
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                // Set the initial directory of the folder browser dialog
                folderBrowserDialog.SelectedPath = "C:\\";

                // Show the folder browser dialog
                DialogResult result = folderBrowserDialog.ShowDialog();

                // Process the result of the dialog box
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    // The user has selected a folder
                    defaultFolderPath = folderBrowserDialog.SelectedPath;
                    // Do something with the folder path
                    TextWriter tw = new StreamWriter(rootFolder + "defaultFolderPath.log");
                    tw.WriteLine(defaultFolderPath);
                    tw.Close();
                }
            }
            CheckForDefaultFolder();


        }

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
                var fileNameOnly = FileNameOnly(saveFileDialog.FileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + saveFileDialog.FileName;
                autosaveActive = true;
                StripStatusConstructor("Autosave is now active, take care while editing.", "", "");
                EnableFeatures(true);
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
                var fileNameOnly = FileNameOnly(openFileDialog.FileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + openFileDialog.FileName;
                currentFileName = openFileDialog.FileName;
                autosaveActive = true;
                StripStatusConstructor("Autosave is now active, take care while editing.", "", "");
                EnableFeatures(true);
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
                var fileNameOnly = FileNameOnly(saveFileDialog.FileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + saveFileDialog.FileName;
                currentFileName = saveFileDialog.FileName;
                autosaveActive = true;
                StripStatusConstructor("Autosave is now active, take care while editing.", "", "");
                EnableFeatures(true);
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
                var fileNameOnly = FileNameOnly(saveFileDialog.FileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + currentFileName;
                autosaveActive = true;
                StripStatusConstructor("Created a backup copy. Autosave is active on the original file", "", "");
                EnableFeatures(true);
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        //
        // EDIT menu
        //

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

        private void quicksaveLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
namespace AutosaveNotepad
{
    using System;
    using System.IO;
    using System.Media;
    using System.Text.RegularExpressions;
    public partial class formMain : Form
    {


        private string currentFileName = string.Empty;
        private bool autosaveActive = false;
        string defaultFolderPath = string.Empty;
        string defaultFolderLogFilePath = string.Empty;
        string rootFolder = AppDomain.CurrentDomain.BaseDirectory;
        string autosaveStatus = "";
        string miscInfo = "";
        string defaultFolderStatus = "";
        string quickSaveInput = "";

        public formMain() //this code block is executed after the main form is instantiated.
        {
            InitializeComponent();

            //
            // AutosaveNotepad initial settings:
            //

            EnableFeatures(true);
            CheckForDefaultFolder();
            StripStatusConstructor("Autosave is NOT active - Create or open a document.", "", "");
            WordWrapActive(true);

            statusStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            toolStripStatusLabel2.Alignment = ToolStripItemAlignment.Right;
            quicksaveButton.Enabled = false;
            autosaveCheckBox.Enabled = false;

            undoToolStripMenuItem.Enabled = false;
            undoToolStripMenuItem1.Enabled = false;
            redoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem1.Enabled = false;

            debug.Enabled = false;
            debug.Visible = false;
        }

        public void formMain_Load(object sender, EventArgs e)
        {

        }

        #region TEXT BOXES input
        //
        // TEXT BOXES input
        //

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            Autosave();
            StripStatusConstructor("", " ", "");
            if (richTextBox.CanUndo)
            {
                undoToolStripMenuItem.Enabled = true;
                undoToolStripMenuItem1.Enabled = true;
            }

            else
            {
                undoToolStripMenuItem.Enabled = false;
                undoToolStripMenuItem1.Enabled = false;
            }

            if (richTextBox.CanRedo)
            {
                redoToolStripMenuItem.Enabled = true;
                redoToolStripMenuItem1.Enabled = true;
            }

            else
            {
                redoToolStripMenuItem.Enabled = false;
                redoToolStripMenuItem1.Enabled = false;
            }

        }

        private void quicksaveTextBox_TextChanged(object sender, EventArgs e)
        {
            int error1Count = 0;
            int error2Count = 0;
            int error3Count = 0;
            bool warning = false;
            quickSaveInput = quicksaveTextBox.Text;

            if (quickSaveInput.Length == 0 || quickSaveInput == "")
            {
                error1Count++;
                StripStatusConstructor("", "File name cannot be empty", "");

            }
            else
            {
                error1Count = 0;
            }


            if (Regex.IsMatch(quickSaveInput, "[<>:\"/|?*]") || Regex.IsMatch(quickSaveInput, @"[\\]"))
            {
                error2Count++;
                StripStatusConstructor("", "Remove the following characters:" + @"<>:""/\|?*", "");
            }
            else
            {
                error2Count = 0;

            }

            if (CheckForDefaultFolder() == false)
            {
                error3Count++;
            }

            else
            {
                error3Count = 0;
                if (File.Exists(defaultFolderPath + "\\" + quickSaveInput + ".txt") == true)
                {
                    warning = true;

                }
            }

            if (error1Count + error2Count + error3Count > 0)
            {
                quicksaveButton.Enabled = false;
            }

            else
            {
                quicksaveButton.Enabled = true;
                if (!warning)
                {
                    StripStatusConstructor("", "Quicksave filename valid.", "");
                }

                else
                {
                    StripStatusConstructor("", "File already exists. Quicksave at own discretion.", "");
                    SystemSounds.Exclamation.Play();
                }

            }
        }

        #endregion

        #region QUICK SAVE BUTTON
        //
        // QUICK SAVE BUTTON
        //

        private void quicksaveButton_Click_1(object sender, EventArgs e)
        {
            bool validFolderCheck = CheckForDefaultFolder();
            if (validFolderCheck)
            {
                currentFileName = defaultFolderPath + "\\" + quicksaveTextBox.Text + ".txt";
                richTextBox.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);
                StripStatusConstructor("Autosave is now active.", "Quicksave successful.", "");

                var fileNameOnly = FilenameTrimmer(currentFileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + currentFileName;

                AutosaveActive(true);

            }

            else
            {
                QuickSaveBarControl(false);
                StripStatusConstructor("", "Quicksave failed", "Please check if default save folder exists.");
                AutosaveActive(false);
            }
        }

        #endregion

        #region FEATURE functions

        //
        // FEATURE functions
        //



        private bool AutosaveActive(bool status)
        {
            autosaveCheckBox.Enabled = true;
            if (status == false)
            {
                autosaveActive = false;
                autosaveCheckBox.Checked = false;
                StripStatusConstructor("Autosave is deactivated.", "", "");
                return false;
            }

            else
            {
                autosaveActive = true;
                autosaveCheckBox.Checked = true;
                return true;
            }

        }

        private void autosaveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (autosaveCheckBox.Checked == true)
            {
                StripStatusConstructor("Autosave is active.", " ", "");
                AutosaveActive(true);
            }

            else
            {
                StripStatusConstructor("Autosave is deactivated.", " ", "");
                AutosaveActive(false);
            }
        }

        private void WordWrapActive(bool status)
        {
            if (status == false)
            {
                wordWrapToolStripMenuItem.Checked = false;
                richTextBox.WordWrap = false;
                StripStatusConstructor("", "Word Wrap disabled.", "");

            }

            else
            {
                wordWrapToolStripMenuItem.Checked = true;
                richTextBox.WordWrap = true;
                StripStatusConstructor("", "Word Wrap enabled.", "");

            }
        }

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

        private bool CheckForDefaultFolder()
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
                        return true;
                    }

                    else
                    {
                        StripStatusConstructor("", "", "Default save folder not found.");
                        QuickSaveBarControl(false);
                        return false;
                    }
                }
            }
            else
            {
                StripStatusConstructor("", "", "Default folder not selected.");
                QuickSaveBarControl(false);
                return false;
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

        private object FilenameTrimmer(string fileName)
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


        #endregion 

        #region FILE menu
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

        #endregion

        #region FILE functions

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
                var fileNameOnly = FilenameTrimmer(saveFileDialog.FileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + saveFileDialog.FileName;
                AutosaveActive(true);
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
                var fileNameOnly = FilenameTrimmer(openFileDialog.FileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + openFileDialog.FileName;
                currentFileName = openFileDialog.FileName;
                AutosaveActive(true);
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
                var fileNameOnly = FilenameTrimmer(saveFileDialog.FileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + saveFileDialog.FileName;
                currentFileName = saveFileDialog.FileName;
                AutosaveActive(true);
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
                var fileNameOnly = FilenameTrimmer(saveFileDialog.FileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + currentFileName;
                AutosaveActive(true);
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

        #endregion

        #region EDIT menu

        //
        // EDIT menu
        //

        // Undo

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox.CanUndo)
            {
                richTextBox.Undo();
                undoToolStripMenuItem.Enabled = true;

            }

            else
            {
                undoToolStripMenuItem.Enabled = false;
            }

            Autosave();
        }

        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (richTextBox.CanUndo)
            {
                richTextBox.Undo();
                undoToolStripMenuItem1.Enabled = true;

            }

            else
            {
                undoToolStripMenuItem1.Enabled = false;
            }

            Autosave();
        }

        // Redo

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox.CanRedo)
            {
                richTextBox.Redo();
                redoToolStripMenuItem.Enabled = true;

            }

            else
            {
                richTextBox.Redo();
                redoToolStripMenuItem.Enabled = false;
            }

            Autosave();
        }

        private void redoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox.CanRedo)
            {
                richTextBox.Redo();
                redoToolStripMenuItem1.Enabled = true;

            }

            else
            {
                richTextBox.Redo();
                redoToolStripMenuItem1.Enabled = false;
            }
            Autosave();
        }

        // Cut

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
            Autosave();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
            Autosave();
        }

        // Copy

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
            Autosave();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
            Autosave();
        }

        // Paste

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
            Autosave();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
            Autosave();
        }

        // Clear

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Clear();
            Autosave();
        }

        private void clearAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox.SelectAll();
            Autosave();
        }

        // Select All
        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.SelectAll();
            Autosave();
        }

        // Word Wrap

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.Checked)
            {
                WordWrapActive(true);
            }

            else
            {
                WordWrapActive(false);
            }
        }

        private void wordWrapToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.Checked)
            {
                WordWrapActive(true);
            }

            else
            {
                WordWrapActive(false);
            }
        }

        #endregion

        #region DISPLAY Menu

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

        #endregion

        #region TOOL STRIP bar

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

        #endregion



        //
        // NEW ACTIONS
        //

        private void quicksaveLabel_Click(object sender, EventArgs e)
        {

        }

        private void quicksaveButton_Click(object sender, EventArgs e)
        {
            //dont use
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
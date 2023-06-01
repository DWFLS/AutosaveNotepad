namespace AutosaveNotepad
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.IO;
    using System.Media;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
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

        string textBackup = "";
        bool textEditingLocked = false;
        bool cutCopyAvailable = false;

        int currentFindIndex = 0;
        List<int> allFinds = new List<int>();
        int findLength = 0;

        bool searchResultOK = false;





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

            ResetFind();

            statusStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            toolStripStatusLabel2.Alignment = ToolStripItemAlignment.Right;

            quicksaveButton.Enabled = false;
            autosaveCheckBox.Enabled = false;
            undoToolStripMenuItem.Enabled = false;
            undoToolStripMenuItem1.Enabled = false;
            redoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem1.Enabled = false;
            cutToolStripMenuItem.Enabled = false;
            cutToolStripMenuItem1.Enabled = false;
            copyToolStripMenuItem.Enabled = false;
            copyToolStripMenuItem1.Enabled = false;
            searchPanel.Visible = true;
            foundCounter.Visible = false;
            findButton.Enabled = false;

            findNextButtonReal.Enabled = false;
            findPrevButton.Enabled = false;

            debug.Enabled = false;
            debug.Visible = true;
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
            StripStatusConstructor("", " ", "");
            CheckForUndoRedo();
            Autosave();
        }

        private void findTextBox_TextChanged(object sender, EventArgs e)
        {
            foundCounter.Text = "";
            findNextButtonReal.Enabled = false;
            findPrevButton.Enabled = false;
        }

        private void richTextBox_SelectionChanged(object sender, EventArgs e)
        {
            CheckForCutCopyPaste();
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
                richTextBox.SaveFile(defaultFolderPath + "\\" + quicksaveTextBox.Text + ".txt", RichTextBoxStreamType.PlainText);
                var fileNameOnly = FilenameTrimmer(currentFileName);
                currentFileName = defaultFolderPath + "\\" + quicksaveTextBox.Text + ".txt";
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + currentFileName;
                AutosaveActive(true);
                StripStatusConstructor("Autosave is now active.", "Quicksave successful.", "");
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

        private string FilenameTrimmer(string fileName)
        {
            return Path.GetFileName(fileName);
        }

        private void CheckForUndoRedo()
        {
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

        private void ProcessPaste()
        {
            if (Clipboard.ContainsText())
            {
                string clipboardText = Clipboard.GetText();
                // Process the clipboard text as plain text
                string plainText = HandlePlainText(clipboardText);

                Clipboard.SetText(plainText);
                // Use the plain text as needed
                richTextBox.Paste();
            }

        }

        private string HandlePlainText(string clipboardText)
        {
            string plainText = clipboardText.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
            return plainText;
        }

        private void CheckForCutCopyPaste()
        {
            if (!string.IsNullOrEmpty(richTextBox.SelectedText))
            {
                cutCopyAvailable = true;
            }
            else
            {
                cutCopyAvailable = false;
            }

            if (cutCopyAvailable)
            {
                cutToolStripMenuItem.Enabled = true;
                cutToolStripMenuItem1.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem1.Enabled = true;
            }

            else
            {
                cutToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem1.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem1.Enabled = false;
            }

            if (Clipboard.ContainsText())
            {
                if (Clipboard.GetText() != "")
                {
                    pasteToolStripMenuItem.Enabled = true;
                    pasteToolStripMenuItem1.Enabled = true;
                }

                else
                {
                    pasteToolStripMenuItem.Enabled = false;
                    pasteToolStripMenuItem1.Enabled = false;
                }
            }

            else
            {
                pasteToolStripMenuItem.Enabled = false;
                pasteToolStripMenuItem1.Enabled = false;
            }

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

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create an instance of the PrintDialog
            PrintDialog printDialog = new PrintDialog();

            // Set optional print dialog properties
            printDialog.AllowSomePages = true;
            printDialog.ShowHelp = true;

            // Display the print dialog
            DialogResult result = printDialog.ShowDialog();

            // Check if the user clicked the Print button in the dialog
            if (result == DialogResult.OK)
            {
                // Retrieve the selected printer and print settings
                PrinterSettings printerSettings = printDialog.PrinterSettings;
                PageSettings pageSettings = printDialog.PrinterSettings.DefaultPageSettings;

                // Perform the printing using the selected printer and settings
                PrintDocument document = new PrintDocument();
                document.PrinterSettings = printerSettings;
                document.DefaultPageSettings = pageSettings;
                document.PrintPage += Document_PrintPage; // Hook up the PrintPage event handler
                document.Print();
            }
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



        private void Document_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Retrieve the content from the RichTextBox control
            string content = richTextBox.Text;

            // Set up the font and other formatting for the printed content
            Font font = new Font("Arial", 12);
            Brush brush = Brushes.Black;

            // Set up the printing area
            RectangleF rect = e.MarginBounds;

            // Calculate the number of lines per page
            int linesPerPage = (int)(rect.Height / font.GetHeight(e.Graphics));

            // Calculate the number of lines to print
            int lineCount = content.Length / linesPerPage;

            // Print the content line by line
            for (int line = 0; line <= lineCount; line++)
            {
                int start = line * linesPerPage;
                int end = start + linesPerPage;

                // Check if we have reached the end of the content
                if (end > content.Length)
                {
                    end = content.Length;
                }

                // Retrieve the line of text to print
                string lineText = content.Substring(start, end - start);

                // Print the line of text
                e.Graphics.DrawString(lineText, font, brush, rect);

                // Move the printing area down to the next line
                rect.Y += font.GetHeight(e.Graphics);
            }
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
            ProcessPaste();
            Autosave();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ProcessPaste();
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
            richTextBox.Clear();
            Autosave();
        }

        // Select All
        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox.SelectAll();
            Autosave();
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
            string url = "https://www." + toolStripStatusLabel2.Text;
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void toolStripStatusLabel1_Click_1(object sender, EventArgs e)
        {

        }

        #endregion

        // SEARCH BUTTON

        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchInRichTextBox();
        }

        private void SearchInRichTextBox()
        {
            ResetFind();

            if (findTextBox.Text != "")
            {
                Search(richTextBox.Text, findTextBox.Text, false, out searchResultOK);

                if (searchResultOK)
                {
                    findButton.Enabled = true;

                    SelectText(allFinds[0], findLength);
                }

                else
                {
                    findButton.Enabled = false;
                    findNextButtonReal.Enabled = false;
                    findPrevButton.Enabled = false;
                    debug.Text = ":(";
                }

            }

            else
            {
                findButton.Enabled = false;
                richTextBox.Enabled = true;
                findNextButtonReal.Enabled = false;
                findPrevButton.Enabled = false;
                richTextBox.Text = textBackup;
                textEditingLocked = false;
            }
            FoundCounterController(currentFindIndex, allFinds.Count, "find");
        }

        /*
         "Highlight" Button.
         */

        private void findNextButton_Click(object sender, EventArgs e) // "Highlight" button
        {
            string findQuery = findTextBox.Text;
            string richText = richTextBox.Text;
            richTextBox.Text = richText;
            bool caseSensitive = false;

            searchResultOK = false;
            FindBoxAndControlsGlobalController(searchResultOK);
            if (textEditingLocked)
            {
                EditingRichTextBoxFeaturesEnabled(true);
                textEditingLocked = false;
                findButton.Text = "Highlight!";
                richTextBox.Text = textBackup;
                FoundCounterController(currentFindIndex, allFinds.Count, "find");
                SearchInRichTextBox();
            }
            else
            {
                ResetFind();
                Search(richText, findQuery, caseSensitive, out searchResultOK);

                if (searchResultOK)
                {
                    Highlight(allFinds, findQuery.Length);
                    NextPrevFindController();
                    FoundCounterController(currentFindIndex, allFinds.Count, "highlight");
                }


            }
        }

        private void Search(string aTextbox, string aQuery, bool aCasing, out bool result)
        {
            string text = aTextbox;
            if (!aCasing) text = text.ToLower();
            List<int> foundIndexes = new List<int>();
            bool found = false;

            for (int i = 0; i < text.Length; i++)
            {
                int occurrenceStreak = 0;
                if (text[i] == aQuery[0]
                    && text.Length >= aQuery.Length + i)
                {
                    for (int j = 0; j < aQuery.Length; j++)
                    {
                        if (text[i + j] == aQuery[j])
                        {
                            occurrenceStreak++;
                        }
                    }

                    if (occurrenceStreak == aQuery.Length)
                    {
                        foundIndexes.Add(i);
                        //i += query.Length;
                        found = true;
                    }
                }
            }
            if (found)
            {
                result = found;
                allFinds = foundIndexes;
                currentFindIndex = 0;
                findLength = aQuery.Length;
                richTextBox.Enabled = true;
            }

            else
            {
                result = false;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (textEditingLocked && findTextBox.Focused != true)
            {
                // Disable all key input and editing actions
                return true;
            }

            else if (textEditingLocked && richTextBox.Focused)
            {
                return true;
            }

            else return base.ProcessCmdKey(ref msg, keyData);
        }

        private void NextPrevFindController()
        {

        }

        private void EditingRichTextBoxFeaturesEnabled(bool state)
        {
            richTextBox.Enabled = state;
            richTextBox.ShortcutsEnabled = state;
        }

        private void ResetFind()
        {
            currentFindIndex = 0;
            allFinds.Clear();
            findLength = 0;
            searchResultOK = false;
        }


        private void Highlight(List<int> indexes, int queryLength)
        {
            textBackup = richTextBox.Text;
            findButton.Text = "Done";
            EditingRichTextBoxFeaturesEnabled(false);
            textEditingLocked = true;


            for (int i = 0; i < indexes.Count; i++)
            {
                richTextBox.Select(indexes[i], queryLength);
                richTextBox.SelectionBackColor = Color.Yellow;
            }

            FindBoxAndControlsGlobalController(searchResultOK);
            //richTextBox.Focus();
            //richTextBox.Refresh();
        }

        private void FindBoxAndControlsGlobalController(bool status)
        {
            findTextBox.Enabled = !status;
            searchButton.Enabled = !status;
            findNextButtonReal.Enabled = !status;
            findPrevButton.Enabled = !status;
        }

        private void SelectText(int selectedIndex, int selectionLength)
        {
            richTextBox.SelectionStart = selectedIndex;
            richTextBox.SelectionLength = selectionLength;
            richTextBox.Focus();
            richTextBox.Refresh();
        }

        private void FoundCounterController(int current, int total, string mode)
        {
            int index = current + 1;
            if (total > 0)
            {
                foundCounter.Visible = true;
                //findQuerySuccess = true;
                if (total < 11) foundCounter.Text = index.ToString() + "/" + total.ToString();
                else foundCounter.Text = total.ToString() + " found.";

                if (mode != "highlight")
                {
                    if (index == total)
                    {
                        findPrevButton.Enabled = false;
                        findNextButtonReal.Enabled = false;

                    }

                    else if (index == allFinds[0] + 1)
                    {
                        findPrevButton.Enabled = false;
                        findNextButtonReal.Enabled = true;
                    }

                    else if (index == allFinds.Count + 1)
                    {
                        findPrevButton.Enabled = true;
                        findNextButtonReal.Enabled = false;
                    }

                    else
                    {
                        findPrevButton.Enabled = true;
                        findNextButtonReal.Enabled = true;
                    }
                }


            }
            else
            {
                //findQuerySuccess = false;
                foundCounter.Visible = false;
            }
        }

        private void findTextBox_Enter(object sender, EventArgs e)
        {
            //OnTheFlySearch();
        }

        private void findTextBox_Leave(object sender, EventArgs e)
        {

        }

        private void findNextButtonReal_Click(object sender, EventArgs e)
        {

        }

        private void findPrevButton_Click(object sender, EventArgs e)
        {

        }

        private void replaceButton_Click(object sender, EventArgs e)
        {

        }

        private void replaceAllButton_Click(object sender, EventArgs e)
        {

        }


        //
        // NEW ACTIONS
        //

        private void quicksaveLabel_Click(object sender, EventArgs e)
        {

        }

        private void quicksaveButton_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


    }
}
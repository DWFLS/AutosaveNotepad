namespace AutosaveNotepad
{
    using System;
    using System.Drawing;
    using System.IO;
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
        int savedFindIndex = 0;
        List<int> allFinds = new List<int>();
        int findLength = 0;
        string foundQuery = "";
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
            searchButton.Enabled = false;

            findNextButtonReal.Enabled = false;
            findPrevButton.Enabled = false;

            debug.Enabled = false;
            debug.Visible = true;
        }

        public void formMain_Load(object sender, EventArgs e)
        {

        }


        #region FEATURE functions

        //
        // FEATURE functions
        //

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

        private void StripStatusConstructor(string a, string b, string c)
        {
            if (a != "") autosaveStatus = a + " ";
            if (b != "") miscInfo = b + " ";
            if (c != "") defaultFolderStatus = c;

            toolStripStatusLabel1.Text = autosaveStatus + miscInfo + defaultFolderStatus;
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
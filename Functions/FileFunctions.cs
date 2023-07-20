namespace AutosaveNotepad
{
    using System.IO;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
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

        private void Save()
        {
            richTextBox.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);
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
                fileNameOnly = FilenameTrimmer(saveFileDialog.FileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + saveFileDialog.FileName;
                AutosaveActive(true);
                StripStatusConstructor("Autosave is now active, take care while editing.", "", "", "");
                EnableFeatures(true);
            }
            SaveButtonCheck();
            CheckForDefaultFolder();
        }

        public void Open()
        {

            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open...";
            openFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileName != currentFileName) //this check is a safe guard against opening same file that is currently opened
                {
                    richTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
                    fileNameOnly = FilenameTrimmer(openFileDialog.FileName);
                    this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + openFileDialog.FileName;
                    currentFileName = openFileDialog.FileName;
                    AutosaveActive(true);
                    StripStatusConstructor("Autosave is now active, take care while editing.", "", "", "");
                    EnableFeatures(true);
                }
            }
            SaveButtonCheck();
            CheckForDefaultFolder();
        }

        private void SaveAs()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save as...";
            saveFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                fileNameOnly = FilenameTrimmer(saveFileDialog.FileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + saveFileDialog.FileName;
                currentFileName = saveFileDialog.FileName; //this line is ommited when using save as copy option
                AutosaveActive(true);
                StripStatusConstructor("Autosave is now active, take care while editing.", "", "", "");
                EnableFeatures(true);
            }
            SaveButtonCheck();
            CheckForDefaultFolder();
        }

        private void SaveAsCopy() //this creates a copy that will not be currently edited
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save a backup copy...";
            saveFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                fileNameOnly = FilenameTrimmer(saveFileDialog.FileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + currentFileName;
                AutosaveActive(true);
                StripStatusConstructor("Created a backup copy. Autosave is active on the original file", "", "", "");
                EnableFeatures(true);
            }
            SaveButtonCheck();
            CheckForDefaultFolder();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}

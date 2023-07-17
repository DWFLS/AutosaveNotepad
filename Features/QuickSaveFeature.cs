namespace AutosaveNotepad
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private void quicksaveButton_Click_1(object sender, EventArgs e)
        {
            bool validFolderCheck = CheckForDefaultFolder();
            if (validFolderCheck) // if the test is positive, then save current file
            {
                currentFileName = defaultFolderPath + "\\" + quicksaveTextBox.Text + ".txt";
                richTextBox.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);
                var fileNameOnly = FilenameTrimmer(currentFileName);
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

        /*
         
         Checks

         */


        private void QuickSaveBarControl(bool validDefPath)
        {
            quicksaveLabel.Enabled = validDefPath;
            quicksaveTextBox.Enabled = validDefPath;
            quicksaveButton.Enabled = validDefPath;

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
    }
}

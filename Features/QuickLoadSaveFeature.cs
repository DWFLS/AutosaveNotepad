namespace AutosaveNotepad
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        string selectedFileName = "";

        private void quicksaveButton_Click_1(object sender, EventArgs e)
        {
            bool validFolderCheck = CheckForDefaultFolder();
            if (validFolderCheck) // if the test is positive, then save current file
            {
                currentFileName = defaultFolderPath + "\\" + quicksaveTextBox.Text + ".txt";
                richTextBox.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);
                fileNameOnly = FilenameTrimmer(currentFileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + currentFileName;
                AutosaveActive(true);
                StripStatusConstructor("Autosave is now active.", "Quicksave successful.", "", "");
            }

            else
            {
                QuickLoadSaveBarControl(false);
                StripStatusConstructor("", "Quicksave failed", "Please check if default save folder exists.", "");
                AutosaveActive(false);
            }
        }

        private void quickLoadComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selectedFileName = quickLoadComboBox.SelectedItem.ToString();
            AutosaveActive(false); // disable autosave temporarily to prevent crashing caused by double loading

            bool validFolderCheck = CheckForDefaultFolder();
            if (validFolderCheck && selectedFileName != "") // if the test is positive, then load current file
            {
                currentFileName = defaultFolderPath + "\\" + selectedFileName;
                richTextBox.LoadFile(currentFileName, RichTextBoxStreamType.PlainText);
                fileNameOnly = FilenameTrimmer(currentFileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + currentFileName;
                AutosaveActive(true);
                StripStatusConstructor("Autosave is now active, take care while editing.", "", "", "");
                EnableFeatures(true);
            }

            else
            {
                QuickLoadSaveBarControl(false);
                StripStatusConstructor("", "Quickload failed", "Please check if default save folder exists.", "");
            }
            SaveButtonCheck();
        }

        private void quickLoadComboBox_Click(object sender, EventArgs e) //rescan when clicked on combobox
        {
            QuickLoadFileScan();
        }

        private void quickLoadComboBox_SelectedIndexChanged(object sender, EventArgs e) //action when selection changed in combobox (clicked on item)
        {

        }

        /*
         
         Checks

         */

        private void QuickLoadFileScan() //populating the combobox by rescanning everytime method loads
        {
            defaultFolderTxtFiles = Directory.GetFiles(defaultFolderPath, "*.txt");
            quickLoadComboBox.Items.Clear();
            foreach (string file in defaultFolderTxtFiles)
            {
                string fileName = Path.GetFileName(file);
                quickLoadComboBox.Items.Add(fileName);
            }
        }

        private void QuickLoadSaveBarControl(bool validDefPath)
        {
            // quickload items
            quickLoadLabel.Enabled = validDefPath;
            quickLoadComboBox.Enabled = validDefPath;

            // quicksave items
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
                        QuickLoadSaveBarControl(true);
                        StripStatusConstructor("", "", "Quicksaving files in: " + defaultFolderPath, "");
                        QuickLoadFileScan();
                        return true;
                    }

                    else
                    {
                        StripStatusConstructor("", "", "Default quicksave folder not selected or found.", "");
                        QuickLoadSaveBarControl(false);
                        return false;
                    }
                }
            }
            else
            {
                StripStatusConstructor("", "", "Default quicksave folder not selected.", "");
                QuickLoadSaveBarControl(false);
                return false;
            }
        }
    }
}

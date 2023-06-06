namespace AutosaveNotepad
{
    using System;
    using System.IO;
    using System.Media;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        //
        // TEXT BOXES input
        //

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            StripStatusConstructor("", " ", "");
            CheckForUndoRedo();
            Autosave();
            WordCount();
            ReplaceController(false);
        }

        private void findTextBox_TextChanged(object sender, EventArgs e)
        {
            foundCounter.Text = "";
            findNextButtonReal.Enabled = false;
            findPrevButton.Enabled = false;
            searchResultOK = false;
            ReplaceController(false);
            findButton.Enabled = false;
            if (findTextBox.Text == "")
            {
                searchButton.Enabled = false;
            }

            else
            {
                searchButton.Enabled = true;
            }
        }

        private void replaceTextBox_TextChanged(object sender, EventArgs e)
        {

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

    }
}

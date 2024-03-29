﻿namespace AutosaveNotepad
{
    using System;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private void replaceAllButton_Click(object sender, EventArgs e)
        {
            string searchText = findTextBox.Text;
            string replaceText = replaceTextBox.Text;

            string richTextBoxText = richTextBox.Text;
            string modifiedText = richTextBoxText.Replace(searchText, replaceText);

            richTextBox.Text = modifiedText;
        }

        private void ReplaceController(bool searchStatus) //called by find related methods
        {
            replaceLabel.Enabled = searchStatus;
            replaceTextBox.Enabled = searchStatus;
            replaceButton.Enabled = searchStatus;
            replaceAllButton.Enabled = searchStatus;
        }
    }
}
namespace AutosaveNotepad
{
    using System;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private void replaceButton_Click(object sender, EventArgs e)
        {
            richTextBox.Visible = false;
            int forwards = currentFindIndexDisplayed;
            richTextBox.SelectedText = replaceTextBox.Text;

            SearchInRichTextBox(); //search needs to called after every singular replace

            for (int i = 0; i < forwards; i++) // and the loop forwards to next possible search, so it's not restarting at the first one
            {
                Next();
            }
            richTextBox.Visible = true;
            richTextBox.Focus();
        }
    }
}

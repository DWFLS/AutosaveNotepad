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

            SearchInRichTextBox();

            for (int i = 0; i < forwards; i++)
            {
                Next();
            }
            richTextBox.Visible = true;
            richTextBox.Focus();
        }
    }
}

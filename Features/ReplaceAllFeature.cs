namespace AutosaveNotepad
{
    using System;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private void replaceTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ReplaceController(bool searchStatus)
        {
            replaceLabel.Enabled = searchStatus;
            replaceTextBox.Enabled = searchStatus;
            replaceButton.Enabled = searchStatus;
            replaceAllButton.Enabled = searchStatus;
        }

        private void replaceAllButton_Click(object sender, EventArgs e)
        {
            string searchText = findTextBox.Text;
            string replaceText = replaceTextBox.Text;

            string richTextBoxText = richTextBox.Text;
            string modifiedText = richTextBoxText.Replace(searchText, replaceText);

            richTextBox.Text = modifiedText;
        }
    }
}
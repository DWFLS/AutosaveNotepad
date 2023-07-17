namespace AutosaveNotepad
{
    using System;
    public partial class formMain
    {
        private void wordCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wordCountToolStripMenuItem.Checked = !wordCountToolStripMenuItem.Checked;
            WordCountCheck();
        }

        private void WordCountCheck()
        {
            if (wordCountToolStripMenuItem.Checked)
            {
                WordCount();
            }

            else
            {
                wordCountLabel.Visible = false;
            }
        }

        private void WordCount() //updating the count every time the function is called
        {
            if (richTextBox.Text != "")
            {
                if (wordCountToolStripMenuItem.Checked)
                {
                    wordCountLabel.Visible = true;
                    debug.Text = "Word Count should work";
                    string text = richTextBox.Text;
                    string[] words = text.Split(new char[] { ' ', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
                    wordCountLabel.Text = "Word Count: " + words.Length.ToString();
                }

                else
                {
                    wordCountLabel.Visible = false;
                }
            }

            else
            {
                wordCountLabel.Visible = false;
            }
        }

        /*
         Not in use
         */

        private void wordCountToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {

        }
    }
}

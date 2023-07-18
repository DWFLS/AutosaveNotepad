namespace AutosaveNotepad
{
    using System;
    public partial class formMain
    {
        string wordCount = "";

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
                StripStatusConstructor("", "", "", " ");
                //wordCountLabel.Visible = false;
            }
        }

        private void WordCount() //updating the count every time the function is called
        {
            if (richTextBox.Text != "")
            {
                if (wordCountToolStripMenuItem.Checked)
                {
                    //wordCountLabel.Visible = true;
                    debug.Text = "Word Count should work";
                    string text = richTextBox.Text;
                    string[] words = text.Split(new char[] { ' ', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
                    wordCount = "Word Count: " + words.Length.ToString();
                    StripStatusConstructor("", "", "", wordCount);
                }

                else
                {
                    StripStatusConstructor("", "", "", " ");
                    //wordCountLabel.Visible = false;
                }
            }

            else
            {
                StripStatusConstructor("", "", "", " ");
                //wordCountLabel.Visible = false;
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

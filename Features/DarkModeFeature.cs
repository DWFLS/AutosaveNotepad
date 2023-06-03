namespace AutosaveNotepad
{
    using System.Drawing;
    public partial class formMain
    {
        private void CheckColors()
        {
            if (darkModeToolStripMenuItem1.Checked)
            {
                richTextBox.BackColor = Color.FromArgb(8, 17, 19);
                richTextBox.ForeColor = Color.White;
                if (textEditingLocked)
                {
                    richTextBox.ForeColor = Color.Black;
                }

                else
                {
                    richTextBox.ForeColor = Color.White;
                }
            }
            else
            {
                richTextBox.BackColor = Color.White;
                richTextBox.ForeColor = Color.Black;
            }
        }
    }
}


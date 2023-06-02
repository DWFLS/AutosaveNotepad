namespace AutosaveNotepad
{
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private void WordWrapActive(bool status)
        {
            if (status == false)
            {
                wordWrapToolStripMenuItem.Checked = false;
                richTextBox.WordWrap = false;
                StripStatusConstructor("", "Word Wrap disabled.", "");
            }

            else
            {
                wordWrapToolStripMenuItem.Checked = true;
                richTextBox.WordWrap = true;
                StripStatusConstructor("", "Word Wrap enabled.", "");
            }
        }
    }
}

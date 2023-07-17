namespace AutosaveNotepad
{
    public partial class formMain
    {
        private void CheckForCutCopyPaste() //enabling features based on circumstances
        {
            if (!string.IsNullOrEmpty(richTextBox.SelectedText))
            {
                cutCopyAvailable = true;
            }
            else
            {
                cutCopyAvailable = false;
            }

            if (cutCopyAvailable)
            {
                cutToolStripMenuItem.Enabled = true;
                cutToolStripMenuItem1.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem1.Enabled = true;
            }

            else
            {
                cutToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem1.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem1.Enabled = false;
            }

            if (Clipboard.ContainsText())
            {
                if (Clipboard.GetText() != "")
                {
                    pasteToolStripMenuItem.Enabled = true;
                    pasteToolStripMenuItem1.Enabled = true;
                }

                else
                {
                    pasteToolStripMenuItem.Enabled = false;
                    pasteToolStripMenuItem1.Enabled = false;
                }
            }

            else
            {
                pasteToolStripMenuItem.Enabled = false;
                pasteToolStripMenuItem1.Enabled = false;
            }

        }
    }
}

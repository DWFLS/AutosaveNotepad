namespace AutosaveNotepad
{
    public partial class formMain
    {
        private void ProcessPaste()
        {
            if (Clipboard.ContainsText())
            {
                string clipboardText = Clipboard.GetText();
                // Process the clipboard text as plain text
                string plainText = HandlePlainText(clipboardText);

                Clipboard.SetText(plainText);
                // Use the plain text as needed
                richTextBox.Paste();
            }

        }

        private string HandlePlainText(string clipboardText)
        {
            string plainText = clipboardText.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
            return plainText;
        }

        private void CheckForCutCopyPaste()
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

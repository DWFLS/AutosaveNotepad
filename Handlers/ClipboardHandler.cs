namespace AutosaveNotepad
{
    public partial class formMain
    {
        private void ProcessPaste() //handle clipboard to be just plain text, no tables, different fonts and so on.
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
    }
}

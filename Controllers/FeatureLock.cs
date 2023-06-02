namespace AutosaveNotepad
{
    public partial class formMain
    {
        private void EnableFeatures(bool b)
        {
            CheckColors();
            richTextBox.Enabled = b;
            editToolStripMenuItem.Enabled = b;
            displaySettingToolStripMenuItem.Enabled = b;
            saveAsToolStripMenuItem.Enabled = b;
            saveAsCopyToolStripMenuItem.Enabled = b;
        }
    }
}

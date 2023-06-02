namespace AutosaveNotepad
{
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private void EditingRichTextBoxFeaturesEnabled(bool state)
        {
            richTextBox.Enabled = state;
            richTextBox.ShortcutsEnabled = state;
        }
    }
}
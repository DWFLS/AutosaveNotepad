namespace AutosaveNotepad
{
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Disable all key input and editing actions to prevent ctrl c/v etc

            if (textEditingLocked && findTextBox.Focused != true)
            {
                return true;
            }

            else if (textEditingLocked && richTextBox.Focused)
            {
                return true;
            }

            else return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
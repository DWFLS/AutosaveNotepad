namespace AutosaveNotepad
{
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (textEditingLocked && findTextBox.Focused != true)
            {
                // Disable all key input and editing actions
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
namespace AutosaveNotepad
{
    using System;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private void debugConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            debugConsoleToolStripMenuItem.Checked = !debugConsoleToolStripMenuItem.Checked;
            CheckDebugConsole();
        }

        private void CheckDebugConsole()
        {
            if (debugConsoleToolStripMenuItem.Checked)
            {
                debug.Visible = true;
                debug.Enabled = true;
            }

            else
            {
                debug.Visible = false;
                debug.Enabled = false;
            }
        }

        private void Debug(string input)
        {
            debug.Text += input;
            debug.Text += "\r\n";
        }
    }
}
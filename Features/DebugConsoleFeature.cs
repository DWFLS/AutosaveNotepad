namespace AutosaveNotepad
{
    using System;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private void debugConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            debugConsoleToolStripMenuItem.Checked = !debugConsoleToolStripMenuItem.Checked;
            if (debugConsoleToolStripMenuItem.Checked)
            {
                debug.Visible = true;
            }

            else
            {
                debug.Visible = false;
            }

        }

        private void Debug(string input)
        {
            debug.Text += input;
            debug.Text += "\r\n";
        }
    }
}
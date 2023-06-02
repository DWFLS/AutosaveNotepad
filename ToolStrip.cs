namespace AutosaveNotepad
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        //
        // TOOL STRIP bar
        //

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            string url = "https://www." + toolStripStatusLabel2.Text;
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void toolStripStatusLabel1_Click_1(object sender, EventArgs e)
        {

        }
    }
}

namespace AutosaveNotepad
{
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private void StripStatusConstructor(string a, string b, string c)
        {
            if (a != "") autosaveStatus = a + " ";
            if (b != "") miscInfo = b + " ";
            if (c != "") defaultFolderStatus = c;

            toolStripStatusLabel1.Text = autosaveStatus + miscInfo + defaultFolderStatus;
        }
    }
}

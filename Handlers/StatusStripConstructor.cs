namespace AutosaveNotepad
{
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private void StripStatusConstructor(string a, string b, string c, string d)
        {
            if (a != "") autosaveStatus = a;
            if (b != "") miscInfo = " " + b;
            else
            {
                miscInfo = "";
            }
            if (c != "") defaultFolderStatus = " " + c;
            if (d != "") wordCountStatus = " " + d;

            toolStripStatusLabel1.Text = autosaveStatus + miscInfo + defaultFolderStatus + wordCountStatus;
        }
    }
}

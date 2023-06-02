namespace AutosaveNotepad
{
    using System.IO;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private string FilenameTrimmer(string fileName)
        {
            return Path.GetFileName(fileName);
        }
    }
}

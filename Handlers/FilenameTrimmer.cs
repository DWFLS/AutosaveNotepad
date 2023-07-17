namespace AutosaveNotepad
{
    using System.IO;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private string FilenameTrimmer(string fileName) //converts path+filename just to filename
        {
            return Path.GetFileName(fileName);
        }
    }
}

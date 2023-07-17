namespace AutosaveNotepad
{
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private void OpenWithHandler() // this gets called only when openWithFilePath is not blank,
                                       // and this variable is not blank only when it's not assigned in form1.cs,
                                       // which is passed from program.cs command-line arguements
        {
            if (openWithFilePath != "")
            {
                richTextBox.Text = openWithFilePath;

                richTextBox.LoadFile(openWithFilePath, RichTextBoxStreamType.PlainText);
                var fileNameOnly = FilenameTrimmer(openWithFilePath);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + openWithFilePath;
                currentFileName = openWithFilePath;
                AutosaveActive(true);
                StripStatusConstructor("Autosave is now active, take care while editing.", "", "");
                EnableFeatures(true);
            }
        }
    }
}

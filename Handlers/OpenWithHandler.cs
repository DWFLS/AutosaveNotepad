namespace AutosaveNotepad
{
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private void OpenWithHandler()
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

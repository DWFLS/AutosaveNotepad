namespace AutosaveNotepad
{
    using System;
    using System.Windows.Forms;
    public partial class formMain
    {
        private void Autosave()
        {
            if (autosaveActive)
            {
                richTextBox.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);
                StripStatusConstructor("Autosave is active.", "", "", "");
            }

            else StripStatusConstructor("Autosave is NOT active.", "", "", "");
        }
        private bool AutosaveActive(bool status)
        {
            autosaveCheckBox.Enabled = true;
            if (status == false)
            {
                autosaveActive = false;
                autosaveCheckBox.Checked = false;
                StripStatusConstructor("Autosave is deactivated.", "", "", "");
                return false;
            }

            else
            {
                autosaveActive = true;
                autosaveCheckBox.Checked = true;
                return true;
            }

        }

        private void autosaveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (autosaveCheckBox.Checked == true)
            {
                StripStatusConstructor("Autosave is active.", " ", "", "");
                AutosaveActive(true);
            }

            else
            {
                StripStatusConstructor("Autosave is deactivated.", " ", "", "");
                AutosaveActive(false);
            }
        }
    }
}

namespace AutosaveNotepad
{
    using Microsoft.Win32;
    using System;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        private void makeDefaultTxtAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                // Set the file association in the registry
                RegistryKey key = Registry.ClassesRoot.OpenSubKey(".txt", true) ?? Registry.ClassesRoot.CreateSubKey(".txt");
                key.SetValue("", "MyTextFile");
                key.Close();

                RegistryKey fileKey = Registry.ClassesRoot.OpenSubKey("MyTextFile", true) ?? Registry.ClassesRoot.CreateSubKey("MyTextFile");
                fileKey.SetValue("", "My Text File");
                fileKey.Close();

                RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey("MyTextFile\\shell\\open\\command", true);
                shellKey?.SetValue("", "\"" + appPath + "\" \"%1\"");
                shellKey?.Close();

                MessageBox.Show("Default program set successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while setting the default program: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
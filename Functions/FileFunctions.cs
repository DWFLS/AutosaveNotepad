namespace AutosaveNotepad
{
    using System.Drawing;
    using System.Drawing.Printing;
    using System.IO;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        //
        // FILE functions
        //

        private void SelectDefaultFolder()
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                // Set the initial directory of the folder browser dialog
                folderBrowserDialog.SelectedPath = "C:\\";

                // Show the folder browser dialog
                DialogResult result = folderBrowserDialog.ShowDialog();

                // Process the result of the dialog box
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    // The user has selected a folder
                    defaultFolderPath = folderBrowserDialog.SelectedPath;
                    // Do something with the folder path
                    TextWriter tw = new StreamWriter(rootFolder + "defaultFolderPath.log");
                    tw.WriteLine(defaultFolderPath);
                    tw.Close();
                }
            }
            CheckForDefaultFolder();


        }

        private void New(string title)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = title;
            saveFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                currentFileName = saveFileDialog.FileName;
                richTextBox.Clear();
                richTextBox.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);
                var fileNameOnly = FilenameTrimmer(saveFileDialog.FileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + saveFileDialog.FileName;
                AutosaveActive(true);
                StripStatusConstructor("Autosave is now active, take care while editing.", "", "");
                EnableFeatures(true);
            }
        }

        private void Open()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open...";
            openFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
                var fileNameOnly = FilenameTrimmer(openFileDialog.FileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + openFileDialog.FileName;
                currentFileName = openFileDialog.FileName;
                AutosaveActive(true);
                StripStatusConstructor("Autosave is now active, take care while editing.", "", "");
                EnableFeatures(true);
            }
        }

        private void SaveAs()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save as...";
            saveFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                var fileNameOnly = FilenameTrimmer(saveFileDialog.FileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + saveFileDialog.FileName;
                currentFileName = saveFileDialog.FileName;
                AutosaveActive(true);
                StripStatusConstructor("Autosave is now active, take care while editing.", "", "");
                EnableFeatures(true);
            }
        }

        private void SaveAsCopy()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save a backup copy...";
            saveFileDialog.Filter = "Text Document|*.txt|All Files|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                var fileNameOnly = FilenameTrimmer(saveFileDialog.FileName);
                this.Text = "AutosaveNotepad - " + fileNameOnly + " - " + currentFileName;
                AutosaveActive(true);
                StripStatusConstructor("Created a backup copy. Autosave is active on the original file", "", "");
                EnableFeatures(true);
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }



        private void Document_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Retrieve the content from the RichTextBox control
            string content = richTextBox.Text;

            // Set up the font and other formatting for the printed content
            Font font = new Font("Arial", 12);
            Brush brush = Brushes.Black;

            // Set up the printing area
            RectangleF rect = e.MarginBounds;

            // Calculate the number of lines per page
            int linesPerPage = (int)(rect.Height / font.GetHeight(e.Graphics));

            // Calculate the number of lines to print
            int lineCount = content.Length / linesPerPage;

            // Print the content line by line
            for (int line = 0; line <= lineCount; line++)
            {
                int start = line * linesPerPage;
                int end = start + linesPerPage;

                // Check if we have reached the end of the content
                if (end > content.Length)
                {
                    end = content.Length;
                }

                // Retrieve the line of text to print
                string lineText = content.Substring(start, end - start);

                // Print the line of text
                e.Graphics.DrawString(lineText, font, brush, rect);

                // Move the printing area down to the next line
                rect.Y += font.GetHeight(e.Graphics);
            }
        }
    }
}

namespace AutosaveNotepad
{
    using System;
    using System.Drawing.Printing;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        //
        // FILE menu
        //

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New("New...");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void saveAsCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFileName))
                SaveAs();

            else
                SaveAsCopy();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void selectDefaultSaveDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectDefaultFolder();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create an instance of the PrintDialog
            PrintDialog printDialog = new PrintDialog();

            // Set optional print dialog properties
            printDialog.AllowSomePages = true;
            printDialog.ShowHelp = true;

            // Display the print dialog
            DialogResult result = printDialog.ShowDialog();

            // Check if the user clicked the Print button in the dialog
            if (result == DialogResult.OK)
            {
                // Retrieve the selected printer and print settings
                PrinterSettings printerSettings = printDialog.PrinterSettings;
                PageSettings pageSettings = printDialog.PrinterSettings.DefaultPageSettings;

                // Perform the printing using the selected printer and settings
                PrintDocument document = new PrintDocument();
                document.PrinterSettings = printerSettings;
                document.DefaultPageSettings = pageSettings;
                document.PrintPage += Document_PrintPage; // Hook up the PrintPage event handler
                document.Print();
            }
        }
    }
}

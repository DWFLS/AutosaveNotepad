namespace AutosaveNotepad
{
    using System.Drawing;
    using System.Drawing.Printing;
    public partial class formMain
    {
        private void Print()
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

namespace AutosaveNotepad
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
            if (args.Length > 0)
            {
                string filePath = args[0];
                Application.Run(new formMain(filePath));
            }
            else
            {
                // Handle when the application is launched without command-line arguments
                Application.Run(new formMain(""));
            }
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
        }
    }
}
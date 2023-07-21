namespace AutosaveNotepad
{
    using System;
    using System.Windows.Forms;

    public partial class formMain : Form
    {
        //variables
        private string openWithFilePath = "";
        private string currentFileName = "";
        string fileNameOnly = "";
        private bool autosaveActive = false;
        string defaultFolderPath = string.Empty;
        string[] defaultFolderTxtFiles = new string[0];
        string defaultFolderLogFilePath = string.Empty;
        string rootFolder = AppDomain.CurrentDomain.BaseDirectory;
        string autosaveStatus = "";
        string miscInfo = "";
        string defaultFolderStatus = "";
        string wordCountStatus = "";
        string quickSaveInput = "";
        string textBackup = "";
        string wordCount = "";
        bool textEditingLocked = false;
        bool cutCopyAvailable = false;
        int currentFindIndex = 0;
        List<int> allFinds = new List<int>();
        int findLength = 0;
        string foundQuery = "";
        bool searchResultOK = false;
        int currentFindIndexDisplayed = 0;
        int totalFindResultsDisplayed = 0;

        public formMain(string filePath) //this code block is executed after the main form is instantiated.
        {
            openWithFilePath = filePath;
            InitializeComponent();

            //
            // AutosaveNotepad initial settings:
            //

            statusStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            toolStripStatusLabel2.Alignment = ToolStripItemAlignment.Right;

            EnableFeatures(true);
            CheckForDefaultFolder();
            StripStatusConstructor("Autosave is NOT active - Create or open a document.", "", "", "");
            ResetFind();
            ReplaceController(searchResultOK);
            SaveButtonCheck();
            OpenWithHandler(); //handle "open with" event
            SetupSettings(); // Initial, default and user Settings handler

            // Save on "x"
            this.FormClosing += formMain_FormClosing;

            //regain focus
            this.Activated += Form1_Activated;
        }

        public void formMain_Load(object sender, EventArgs e)
        {

        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                SaveSettings();
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            // Call the method you want to execute when the form regains focus.
            CheckForDefaultFolder();
            SaveButtonCheck();
        }

        //
        // NEW ACTIONS
        //

        private void quicksaveLabel_Click(object sender, EventArgs e)
        {

        }

        private void quicksaveButton_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
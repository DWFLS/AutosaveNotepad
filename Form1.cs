namespace AutosaveNotepad
{
    using System;
    using System.Windows.Forms;

    public partial class formMain : Form
    {
        private string currentFileName = string.Empty;
        private bool autosaveActive = false;
        string defaultFolderPath = string.Empty;
        string defaultFolderLogFilePath = string.Empty;
        string rootFolder = AppDomain.CurrentDomain.BaseDirectory;
        string autosaveStatus = "";
        string miscInfo = "";
        string defaultFolderStatus = "";
        string quickSaveInput = "";
        string textBackup = "";
        bool textEditingLocked = false;
        bool cutCopyAvailable = false;
        int currentFindIndex = 0;
        List<int> allFinds = new List<int>();
        int findLength = 0;
        string foundQuery = "";
        bool searchResultOK = false;

        public formMain() //this code block is executed after the main form is instantiated.
        {
            InitializeComponent();

            //
            // AutosaveNotepad initial settings:
            //

            EnableFeatures(true);
            CheckForDefaultFolder();
            StripStatusConstructor("Autosave is NOT active - Create or open a document.", "", "");
            WordWrapActive(true);
            ResetFind();
            CheckColors();
            ReplaceController(searchResultOK);

            searchPanel.Visible = true;
            panel1.Visible = true;

            statusStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            toolStripStatusLabel2.Alignment = ToolStripItemAlignment.Right;

            quicksaveButton.Enabled = false;
            autosaveCheckBox.Enabled = false;
            undoToolStripMenuItem.Enabled = false;
            undoToolStripMenuItem1.Enabled = false;
            redoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem1.Enabled = false;
            cutToolStripMenuItem.Enabled = false;
            cutToolStripMenuItem1.Enabled = false;
            copyToolStripMenuItem.Enabled = false;
            copyToolStripMenuItem1.Enabled = false;
            searchPanel.Visible = true;
            foundCounter.Visible = false;
            findButton.Enabled = false;
            searchButton.Enabled = false;

            findNextButtonReal.Enabled = false;
            findPrevButton.Enabled = false;

            wordCountLabel.Visible = false;

            searchPanelToolStripMenuItem.Checked = true;
            quicksavePanelToolStripMenuItem.Checked = true;


            debug.Enabled = false;
            debug.Visible = false;
        }

        public void formMain_Load(object sender, EventArgs e)
        {

        }


        private void replaceButton_Click(object sender, EventArgs e)
        {

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
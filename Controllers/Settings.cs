namespace AutosaveNotepad
{
    /*
Settings file scheme:
(Name)
an.cfg

(File Menu)
[0] - Default save directory

(Edit Menu)
[1] - Case Sensitive Search
[2] - Word Wrap
[3] - Word Count

(View Menu)
[4] - Quicksave Panel
[5] - Search Panel
[6] - Dark Mode
[7] - Debug Console 
    */

    using System;
    using System.IO;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        string configFileName = "an.cfg";
        string configFileNamePath = "";
        string[] config = new string[8];
        string desktopPath = "";

        private void GetDesktopPath()
        {
            desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private void GenerateDefaultSettings()
        {
            GetDesktopPath();
            config[0] = desktopPath;
            config[1] = "Case Sensitive Search = false"; //Case Sensitive Search
            config[2] = "Word Wrap = true"; //Word Wrap
            config[3] = "Word Count = true"; //Word Count
            config[4] = "Quicksave Panel = true"; //Quicksave Panel
            config[5] = "Search Panel = true"; //Search Panel
            config[6] = "Dark Mode = false"; //Dark Mode
            config[7] = "Debug Console = false"; //Debug Console

            File.WriteAllLines(configFileNamePath, config);
        }

        private void CombinePath()
        {
            configFileNamePath = rootFolder + "\\" + configFileName;
        }

        private void SetupSettings()
        {
            InitalElementsState();
            CombinePath();

            if (File.Exists(configFileNamePath))
            {
                VerifySettings();
            }

            else

            {
                GenerateDefaultSettings();
                LoadSettings();
            }

        }

        private void InitalElementsState()
        {
            // initial settings
            // --- menu items
            undoToolStripMenuItem.Enabled = false;
            undoToolStripMenuItem1.Enabled = false;
            redoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem1.Enabled = false;
            cutToolStripMenuItem.Enabled = false;
            cutToolStripMenuItem1.Enabled = false;
            copyToolStripMenuItem.Enabled = false;
            copyToolStripMenuItem1.Enabled = false;

            // --- buttons
            quicksaveButton.Enabled = false;
            autosaveCheckBox.Enabled = false;
            foundCounter.Visible = false;
            findButton.Enabled = false;
            searchButton.Enabled = false;
            findNextButtonReal.Enabled = false;
            findPrevButton.Enabled = false;
            debug.Enabled = false;
            debug.Visible = false;
            searchPanel.Visible = false;
            panel1.Visible = false;
        }

        private void VerifySettings()
        {
            int lineCount = File.ReadLines(configFileNamePath).Count();

            if (lineCount == config.Length)
            {
                LoadSettings();
            }

            else
            {
                File.Delete(configFileNamePath);
                GenerateDefaultSettings();
                LoadSettings();
            }
        }

        private void LoadSettings()
        {
            // Reading settings from the config file

            for (int i = 0; i < config.Length; i++)
            {
                config[i] = File.ReadLines(configFileNamePath).ElementAtOrDefault(i);
            };

            // verifying if loaded config[0] still exists
            if (Directory.Exists(config[0]))
            {
                defaultFolderPath = config[0];
            }

            else
            {
                GetDesktopPath();
                defaultFolderPath = desktopPath;
            }

            //
            // --- Case Sensitive Search ---
            // 
            if (config[1] == "Case Sensitive Search = true")
            {
                caseSensitiveSearchToolStripMenuItem.Checked = true;
                CaseSenstiveSearch();
            }
            else
            {
                caseSensitiveSearchToolStripMenuItem.Checked = false;
                CaseSenstiveSearch();
            }

            //
            // --- Word Wrap ---
            // 

            if (config[2] == "Word Wrap = true")
            {
                wordWrapToolStripMenuItem.Checked = true;
                WordWrapActive(true);
            }
            else
            {
                wordWrapToolStripMenuItem.Checked = false;
                WordWrapActive(false);
            }

            //
            // --- Word Count ---
            //

            if (config[3] == "Word Count = true")
            {
                wordCountToolStripMenuItem.Checked = true;
                WordCountCheck();
            }
            else
            {
                wordCountToolStripMenuItem.Checked = false;
                WordCountCheck();
            }

            //
            // --- Quicksave Panel ---
            // 

            if (config[4] == "Quicksave Panel = true")
            {
                quicksavePanelToolStripMenuItem.Checked = true;
            }
            else
            {
                quicksavePanelToolStripMenuItem.Checked = false;
            }

            //
            // --- Search Panel ---
            // 

            if (config[5] == "Search Panel = true")
            {
                searchPanelToolStripMenuItem.Checked = true;
            }
            else
            {
                searchPanelToolStripMenuItem.Checked = false;
            }

            //
            // --- Dark Mode ---
            // 

            if (config[6] == "Dark Mode = true")
            {
                darkModeToolStripMenuItem1.Checked = true;
                CheckColors();
            }

            else
            {
                darkModeToolStripMenuItem1.Checked = false;
                CheckColors();
            }

            //
            // --- Debug Console ---
            //

            if (config[7] == "Debug Console = true")
            {
                debugConsoleToolStripMenuItem.Checked = true;
                CheckDebugConsole();
            }
            else
            {
                debugConsoleToolStripMenuItem.Checked = false;
                CheckDebugConsole();
            }
        }

        private void SaveSettings()
        {
            if (Directory.Exists(defaultFolderPath))
            {
                config[0] = defaultFolderPath;
            }

            else
            {
                config[0] = desktopPath;
            }

            //
            // --- Case Sensitive Search ---
            // 

            if (caseSensitiveSearchToolStripMenuItem.Checked == true)
            {
                config[1] = "Case Sensitive Search = true";
            }
            else
            {
                config[1] = "Case Sensitive Search = false";
            }


            //
            // --- Word Wrap ---
            // 

            if (wordWrapToolStripMenuItem.Checked == true)
            {
                config[2] = "Word Wrap = true";
            }
            else
            {
                config[2] = "Word Wrap = false";
            }

            //
            // --- Word Count ---
            //

            if (wordCountToolStripMenuItem.Checked == true)
            {
                config[3] = "Word Count = true";
            }
            else
            {
                config[3] = "Word Count = false";
            }

            //
            // --- Quicksave Panel ---
            // 

            if (quicksavePanelToolStripMenuItem.Checked == true)
            {
                config[4] = "Quicksave Panel = true";
            }
            else
            {
                config[4] = "Quicksave Panel = false";
            }

            //
            // --- Search Panel ---
            // 

            if (searchPanelToolStripMenuItem.Checked == true)
            {
                config[5] = "Search Panel = true";
            }
            else
            {
                config[5] = "Search Panel = false";
            }

            //
            // --- Dark Mode ---
            // 

            if (darkModeToolStripMenuItem1.Checked == true)
            {
                config[6] = "Dark Mode = true";
            }
            else
            {
                config[6] = "Dark Mode = false";
            }

            //
            // --- Debug Console ---
            //

            if (debugConsoleToolStripMenuItem.Checked == true)
            {
                config[7] = "Debug Console = true";
            }
            else
            {
                config[7] = "Debug Console = false";
            }

            File.WriteAllLines(configFileNamePath, config);
        }
    }
}


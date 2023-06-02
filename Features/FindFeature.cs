namespace AutosaveNotepad
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    public partial class formMain : Form
    {
        // FIND FEATURE

        private void searchButton_Click(object sender, EventArgs e)
        {
            NextPrevGlobalController(false);
            SearchInRichTextBox();
        }

        private void SearchInRichTextBox()
        {
            ResetFind();

            if (findTextBox.Text != "")
            {
                Search(richTextBox.Text, findTextBox.Text, out searchResultOK);

                if (searchResultOK)
                {
                    findButton.Enabled = true;

                    SelectText(allFinds[0], findLength);
                }

                else
                {
                    findButton.Enabled = false;
                    findNextButtonReal.Enabled = false;
                    findPrevButton.Enabled = false;
                    debug.Text = ":(";
                }

            }

            else
            {
                findButton.Enabled = false;
                richTextBox.Enabled = true;
                findNextButtonReal.Enabled = false;
                findPrevButton.Enabled = false;
                richTextBox.Text = textBackup;
                textEditingLocked = false;
            }
            FoundCounterController(currentFindIndex, allFinds.Count, "find");
        }

        /*
         "Highlight" Button.
         */

        private void findNextButton_Click(object sender, EventArgs e) // "Highlight" button
        {
            string findQuery = findTextBox.Text;
            string richText = richTextBox.Text;
            richTextBox.Text = richText;

            searchResultOK = false;

            FindBoxAndControlsGlobalController(searchResultOK);

            if (textEditingLocked)
            {
                EditingRichTextBoxFeaturesEnabled(true);
                textEditingLocked = false;
                findButton.Text = "Highlight!";
                richTextBox.Text = textBackup;
                FoundCounterController(currentFindIndex, allFinds.Count, "find");
                SearchInRichTextBox();
            }

            else
            {
                ResetFind();
                Search(richText, findQuery, out searchResultOK);

                if (searchResultOK)
                {
                    Highlight(allFinds, findQuery.Length);
                    FoundCounterController(currentFindIndex, allFinds.Count, "highlight");
                }
                else
                {
                    FoundCounterController(currentFindIndex, allFinds.Count, "find");
                }
            }
        }

        private void Search(string aTextbox, string aQuery, out bool result)
        {
            string text = aTextbox;
            if (caseSensitiveSearchToolStripMenuItem.Checked != true)
            {
                text = text.ToLower();
                aQuery.ToLower();
            }
            List<int> foundIndexes = new List<int>();
            bool found = false;

            for (int i = 0; i < text.Length; i++)
            {
                int occurrenceStreak = 0;

                if (text[i] == aQuery[0]
                    && text.Length >= aQuery.Length + i)
                {
                    for (int j = 0; j < aQuery.Length; j++)
                    {
                        if (text[i + j] == aQuery[j])
                        {
                            occurrenceStreak++;
                        }
                    }

                    if (occurrenceStreak == aQuery.Length)
                    {
                        foundIndexes.Add(i);
                        found = true;
                    }
                }
            }
            if (found)
            {
                result = found;
                allFinds = foundIndexes;
                currentFindIndex = 0;
                findLength = aQuery.Length;
                richTextBox.Enabled = true;
                foundQuery = aQuery;
            }

            else
            {
                result = false;
                foundQuery = "";
            }
        }

        private void Highlight(List<int> indexes, int queryLength)
        {
            textBackup = richTextBox.Text;
            findButton.Text = "Done";
            EditingRichTextBoxFeaturesEnabled(false);
            textEditingLocked = true;


            for (int i = 0; i < indexes.Count; i++)
            {
                richTextBox.Select(indexes[i], queryLength);
                richTextBox.SelectionBackColor = Color.Yellow;
            }

            FindBoxAndControlsGlobalController(searchResultOK);
        }

        private void SelectText(int selectedIndex, int selectionLength)
        {
            richTextBox.SelectionStart = selectedIndex;
            richTextBox.SelectionLength = selectionLength;
            richTextBox.Focus();
            richTextBox.Refresh();
        }

        private void findNextButtonReal_Click(object sender, EventArgs e)
        {
            currentFindIndex++;
            SelectText(allFinds[currentFindIndex], foundQuery.Length);
            FoundCounterController(currentFindIndex, allFinds.Count, "find");
        }

        private void findPrevButton_Click(object sender, EventArgs e)
        {
            currentFindIndex--;
            SelectText(allFinds[currentFindIndex], foundQuery.Length);
            FoundCounterController(currentFindIndex, allFinds.Count, "find");
        }

        private void findTextBox_Enter(object sender, EventArgs e)
        {

        }

        private void findTextBox_Leave(object sender, EventArgs e)
        {

        }
    }
}
